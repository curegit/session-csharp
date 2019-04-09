using System;
using System.Threading;
using System.Threading.Tasks;
using System.Threading.Channels;

namespace SessionTypes.Binary.Threading
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


	public class BinaryChannelCommunication : BinaryCommunication
	{
		public BinaryChannelCommunication()
		{

		}

		public override void Send<T>(T value)
		{
			throw new NotImplementedException();
		}

		public override Task<T> ReceiveAsync<T>()
		{
			throw new NotImplementedException();
		}
	}
}

