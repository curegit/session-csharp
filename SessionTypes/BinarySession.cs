using System.Threading.Tasks;

namespace SessionTypes.Binary
{
	public abstract class BinarySession
	{
		private bool used;

		private readonly BinaryCommunicator communicator;

		internal BinarySession(BinarySession session)
		{
			communicator = session.communicator;
		}

		private protected BinarySession(BinaryCommunicator communicator)
		{
			this.communicator = communicator;
		}

		internal void Send<T>(T value)
		{
			if (used)
			{
				throw new LinearityViolationException();
			}
			else
			{
				used = true;
				communicator.Send(value);
			}
		}

		internal Task SendAsync<T>(T value)
		{
			if (used)
			{
				throw new LinearityViolationException();
			}
			else
			{
				used = true;
				return communicator.SendAsync(value);
			}
		}

		internal T Receive<T>()
		{
			if (used)
			{
				throw new LinearityViolationException();
			}
			else
			{
				used = true;
				return communicator.Receive<T>();
			}
		}

		internal Task<T> ReceiveAsync<T>()
		{
			if (used)
			{
				throw new LinearityViolationException();
			}
			else
			{
				used = true;
				return communicator.ReceiveAsync<T>();
			}
		}

		internal void Choose(BinaryChoice choice)
		{
			if (used)
			{
				throw new LinearityViolationException();
			}
			else
			{
				used = true;
				communicator.Choose(choice);
			}
		}

		internal Task ChooseAsync(BinaryChoice choice)
		{
			if (used)
			{
				throw new LinearityViolationException();
			}
			else
			{
				used = true;
				return communicator.ChooseAsync(choice);
			}
		}

		internal BinaryChoice Follow()
		{
			if (used)
			{
				throw new LinearityViolationException();
			}
			else
			{
				used = true;
				return communicator.Follow();
			}
		}

		internal Task<BinaryChoice> FollowAsync()
		{
			if (used)
			{
				throw new LinearityViolationException();
			}
			else
			{
				used = true;
				return communicator.FollowAsync();
			}
		}

		internal void Close()
		{
			if (used)
			{
				throw new LinearityViolationException();
			}
			else
			{
				used = true;
				communicator.Close();
			}
		}
	}

	public sealed class Client<S, P> : BinarySession where S : SessionType where P : SessionType
	{
		internal Client(BinarySession session) : base(session) { }

		internal Client(BinaryCommunicator communicator) : base(communicator) { }
	}

	public sealed class Server<S, P> : BinarySession where S : SessionType where P : SessionType
	{
		internal Server(BinarySession session) : base(session) { }

		internal Server(BinaryCommunicator communicator) : base(communicator) { }
	}
}
