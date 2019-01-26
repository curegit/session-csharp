using System;
using System.Threading;

namespace SessionTypes.Binary
{
	public static class BinarySession<P> where P : SessionType
	{
		private static (Client<P> client, Server<P> server) New()
		{
			return (NewClient(), NewServer());
		}

		private static Client<P> NewClient()
		{
			return new Client<P>();
		}

		private static Server<P> NewServer()
		{
			return new Server<P>();
		}

		public static Client<P> Fork(Action<Server<P>> threadFunction)
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
		public static Server<S> Send<T, S>(this Server<Respond<T, S>> respond, T value) where S : SessionType
		{
			return new Server<S>();
		}

		public static Client<S> Send<T, S>(this Client<Request<T, S>> request, T value) where S : SessionType
		{
			return new Client<S>();
		}

		public static Server<S> Receive<T, S>(this Server<Request<T, S>> request) where S : SessionType
		{
			return new Server<S>();
		}

		public static Client<S> Receive<T, S>(this Client<Respond<T, S>> respond) where S : SessionType
		{
			return new Client<S>();
		}

		public static Server<SL> ChooseLeft<SL, SR>(this Server<RespondChoice<SL, SR>> respondChoice) where SL : SessionType where SR : SessionType
		{
			return new Server<SL>();
		}

		public static Server<SR> ChooseRight<SL, SR>(this Server<RespondChoice<SL, SR>> respondChoice) where SL : SessionType where SR : SessionType
		{
			return new Server<SR>();
		}

		public static Client<SL> ChooseLeft<SL, SR>(this Client<RequestChoice<SL, SR>> requestChoice) where SL : SessionType where SR : SessionType
		{
			return new Client<SL>();
		}

		public static Client<SR> ChooseRight<SL, SR>(this Client<RequestChoice<SL, SR>> requestChoice) where SL : SessionType where SR : SessionType
		{
			return new Client<SR>();
		}

		public static void Follow<SL, SR>(this Server<RequestChoice<SL, SR>> requestChoice, Action<Server<SL>> leftAction, Action<Server<SR>> rightAction) where SL : SessionType where SR : SessionType
		{

		}

		public static void Follow<SL, SR>(this Client<RespondChoice<SL, SR>> respondChoice, Action<Client<SL>> leftAction, Action<Client<SR>> rightAction) where SL : SessionType where SR : SessionType
		{

		}
	}

	public sealed class Server<S> where S : SessionType { }

	public sealed class Client<S> where S : SessionType { }

	public abstract class SessionType { }

	public sealed class Request<T, S> : SessionType where S : SessionType { }

	public sealed class Respond<T, S> : SessionType where S : SessionType { }

	public sealed class RequestChoice<SL, SR> : SessionType where SL : SessionType where SR : SessionType { }

	public sealed class RespondChoice<SL, SR> : SessionType where SL : SessionType where SR : SessionType { }

	public sealed class Close : SessionType { }
}
