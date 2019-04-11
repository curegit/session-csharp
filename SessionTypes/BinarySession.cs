using System;
using System.Threading.Tasks;

namespace SessionTypes.Binary
{
	public static class BinarySession
	{
		public static Server<S, FS> Send<T, S, FS>(this Server<Respond<T, S>, FS> respond, T value) where S : SessionType where FS : SessionType
		{
			var c = respond.GetInternalCommunication();
			c.Send(value);
			return new Server<S, FS>(c);
		}

		public static Client<S, FS> Send<T, S, FS>(this Client<Request<T, S>, FS> request, T value) where S : SessionType where FS : SessionType
		{
			var c = request.GetInternalCommunication();
			c.Send(value);
			return new Client<S, FS>(c);
		}

		public static async Task<(Server<S, FS>, T)> Receive<T, S, FS>(this Server<Request<T, S>, FS> request) where S : SessionType where FS : SessionType
		{
			var c = request.GetInternalCommunication();
			return await Task.Run(async () =>
			{
				var v = await c.ReceiveAsync<T>();
				return (new Server<S, FS>(c), v);
			});
		}

		public static async Task<(Client<S, FS>, T)> Receive<T, S, FS>(this Client<Respond<T, S>, FS> respond) where S : SessionType where FS : SessionType
		{
			var c = respond.GetInternalCommunication();
			return await Task.Run(async () =>
			{
				T v = await c.ReceiveAsync<T>();
				return (new Client<S, FS>(c), v);
			});
		}

		public static Server<S, FS> Enter<S, B, FS>(this Server<Block<S, B>, FS> label) where S : SessionType where B : IBlock where FS : SessionType
		{
			return new Server<S, FS>(label.GetInternalCommunication());
		}

		public static Server<S, Block<S, B>> Zero<S, B>(this Server<Jump<Zero>, Block<S, B>> jump) where S : SessionType where B : IBlock
		{
			return new Server<S, Block<S, B>>(jump.GetInternalCommunication());
		}

		public static Client<S, FS> Enter<S, B, FS>(this Client<Block<S, B>, FS> label) where S : SessionType where B : IBlock where FS : SessionType
		{
			return new Client<S, FS>(label.GetInternalCommunication());
		}

		public static Client<S, Block<S, B>> Zero<S, B>(this Client<Jump<Zero>, Block<S, B>> jump) where S : SessionType where B : IBlock
		{
			return new Client<S, Block<S, B>>(jump.GetInternalCommunication());
		}

		public static Server<SL, FS> ChooseLeft<SL, SR, FS>(this Server<RespondChoice<SL, SR>, FS> respondChoice) where SL : SessionType where SR : SessionType where FS : SessionType
		{
			var c = respondChoice.GetInternalCommunication();
			c.Send(1);
			return new Server<SL, FS>(c);
		}

		public static Server<SR, FS> ChooseRight<SL, SR, FS>(this Server<RespondChoice<SL, SR>, FS> respondChoice) where SL : SessionType where SR : SessionType where FS : SessionType
		{
			var c = respondChoice.GetInternalCommunication();
			c.Send(2);
			return new Server<SR, FS>(c);
		}

		public static Client<SL, FS> ChooseLeft<SL, SR, FS>(this Client<RequestChoice<SL, SR>, FS> requestChoice) where SL : SessionType where SR : SessionType where FS : SessionType
		{
			var c = requestChoice.GetInternalCommunication();
			c.Send(1);
			return new Client<SL, FS>(c);
		}

		public static Client<SR, FS> ChooseRight<SL, SR, FS>(this Client<RequestChoice<SL, SR>, FS> requestChoice) where SL : SessionType where SR : SessionType where FS : SessionType
		{
			var c = requestChoice.GetInternalCommunication();
			c.Send(2);
			return new Client<SR, FS>(c);
		}

		public static async Task Follow<SL, SR, FS>(this Server<RequestChoice<SL, SR>, FS> requestChoice, Func<Server<SL, FS>, Task> leftAction, Func<Server<SR, FS>, Task> rightAction) where SL : SessionType where SR : SessionType where FS : SessionType
		{
			var c = requestChoice.GetInternalCommunication();
			var v = await Task.Run(async () => await c.ReceiveAsync<int>());

			if (v == 1)
			{
				await leftAction(new Server<SL, FS>(c));
			}
			else if (v == 2)
			{
				await rightAction(new Server<SR, FS>(c));
			}
			else
			{

			}
		}

		public static async Task Follow<SL, SR, FS>(this Client<RespondChoice<SL, SR>, FS> respondChoice, Func<Client<SL, FS>, Task> leftAction, Func<Client<SR, FS>, Task> rightAction) where SL : SessionType where SR : SessionType where FS : SessionType
		{
			var c = respondChoice.GetInternalCommunication();
			var v = await Task.Run(async () => await c.ReceiveAsync<int>());

			if (v == 1)
			{
				await leftAction(new Client<SL, FS>(c));
			}
			else if (v == 2)
			{
				await rightAction(new Client<SR, FS>(c));
			}
			else
			{

			}
		}
	}

	public abstract class Communicator
	{
		private int used = 0;

		private BinaryCommunication binaryCommunication;

		public Communicator(BinaryCommunication communication)
		{
			binaryCommunication = communication;
		}

		public BinaryCommunication GetInternalCommunication()
		{
			if (used != 0)
			{
				throw new InvalidOperationException();
			}
			used++;
			return binaryCommunication;
		}
	}

	public sealed class Server<S, FS> : Communicator where S : SessionType where FS : SessionType
	{
		public Server(BinaryCommunication communication) : base(communication)
		{

		}
	}

	public sealed class Client<S, FS> : Communicator where S : SessionType where FS : SessionType
	{
		public Client(BinaryCommunication communication) : base(communication)
		{

		}
	}

	public abstract class IBlock : SessionType { }

	public sealed class EndBlock : IBlock { }

	public sealed class Block<S, B> : IBlock where S : SessionType where B : IBlock { }

	public abstract class SessionType { }

	public sealed class Request<T, S> : SessionType where S : SessionType { }

	public sealed class Respond<T, S> : SessionType where S : SessionType { }

	public sealed class RequestChoice<SL, SR> : SessionType where SL : SessionType where SR : SessionType { }

	public sealed class RespondChoice<SL, SR> : SessionType where SL : SessionType where SR : SessionType { }

	public sealed class Jump<N> : SessionType where N : TypeLevelNatural { }

	public sealed class Close : SessionType { }
}
