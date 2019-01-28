using System;
using System.Threading;
using SessionTypes.Common;

namespace SessionTypes.Binary
{
	public static class BinarySession<P> where P : SessionType
	{
		private static (Client<P, P> client, Server<P, P> server) New()
		{
			return (NewClient(), NewServer());
		}

		private static Client<P, P> NewClient()
		{
			return new Client<P, P>();
		}

		private static Server<P, P> NewServer()
		{
			return new Server<P, P>();
		}

		public static Client<P, P> Fork(Action<Server<P, P>> threadFunction)
		{
			var (client, server) = New();
			var threadStart = new ThreadStart(() => threadFunction(server));
			var serverThread = new Thread(threadStart);
			serverThread.Start();
			return client;
		}
	}

	public static class BinarySession
	{
		public static Server<S, FS> Send<T, S, FS>(this Server<Respond<T, S>, FS> respond, T value) where S : SessionType where FS : SessionType
		{
			return new Server<S, FS>();
		}

		public static Client<S, FS> Send<T, S, FS>(this Client<Request<T, S>, FS> request, T value) where S : SessionType where FS : SessionType
		{
			return new Client<S, FS>();
		}

		public static Server<S, FS> Receive<T, S, FS>(this Server<Request<T, S>, FS> request) where S : SessionType where FS : SessionType
		{
			return new Server<S, FS>();
		}

		public static Client<S, FS> Receive<T, S, FS>(this Client<Respond<T, S>, FS> respond) where S : SessionType where FS : SessionType
		{
			return new Client<S, FS>();
		}

		public static Server<S, FS> Enter<S, B, FS>(this Server<Block<S, B>, FS> label) where S : SessionType where B : IBlock where FS : SessionType
		{
			return new Server<S, FS>();
		}

		public static Server<S, Block<S, B>> Zero<S, B>(this Server<Jump<Zero>, Block<S, B>> jump) where S : SessionType where B : IBlock
		{
			return new Server<S, Block<S, B>>();
		}
	}

	public sealed class Server<S, FS> where S : SessionType where FS : SessionType { }

	public sealed class Client<S, FS> where S : SessionType where FS : SessionType { }

	public abstract class IBlock : SessionType { }

	public sealed class EndBlock : IBlock { }

	public sealed class Block<S, B> : IBlock where S : SessionType where B : IBlock { }

	public abstract class SessionType { }

	public sealed class Request<T, S> : SessionType where S : SessionType { }

	public sealed class Respond<T, S> : SessionType where S : SessionType { }

	/*
	public sealed class Choose<SL, SR> : SessionType where SL : SessionType where SR : SessionType { }

	public sealed class Offer<SL, SR> : SessionType where SL : SessionType where SR : SessionType { }
	*/

	public sealed class Jump<N> : SessionType where N : TypeLevelNatural { }

	public sealed class Close : SessionType { }
}
