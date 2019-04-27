using System;
using System.Threading;
using System.Threading.Tasks;
using System.Threading.Channels;

namespace SessionTypes.Binary.Threading
{
	public static class BinarySessionChannel<P> where P : SessionType
	{
		private static (Client<P, P> client, Server<P, P> server) NewChannel()
		{
			var up = Channel.CreateUnbounded<object>();
			var down = Channel.CreateUnbounded<object>();
			return (NewClient(up, down), NewServer(up, down));
		}

		private static Client<P, P> NewClient(Channel<object> up, Channel<object> down)
		{
			var c = new BinaryChannelCommunication(down.Reader, up.Writer);
			return new Client<P, P>(c);
		}

		private static Server<P, P> NewServer(Channel<object> up, Channel<object> down)
		{
			var c = new BinaryChannelCommunication(up.Reader, down.Writer);
			return new Server<P, P>(c);
		}

		public static Client<P, P> Fork(Action<Server<P, P>> threadFunction)
		{
			var (client, server) = NewChannel();
			var threadStart = new ThreadStart(() => threadFunction(server));
			var serverThread = new Thread(threadStart);
			serverThread.Start();
			return client;
		}

		public static (Client<P, P>, Server<P, P>) Pipeline<A>(Action<Server<P, P>, Client<P, P>, A> threadFunction, A[] args)
		{
			int n = args.Length + 1;
			Client<P, P>[] clients = new Client<P, P>[n];
			Server<P, P>[] servers = new Server<P, P>[n];
			for (int i = 0; i < n; i++)
			{
				var (c, s) = NewChannel();
				clients[i] = c;
				servers[(i + 1) % n] = s;
			}
			for (int i = 1; i < n; i++)
			{
				var threadNumber = i;
				var threadStart = new ThreadStart(() => threadFunction(servers[threadNumber], clients[threadNumber], args[threadNumber - 1]));
				var thread = new Thread(threadStart);
				thread.Start();
			}
			return (clients[0], servers[0]);
		}
	}

	internal class BinaryChannelCommunication : BinaryCommunicator
	{
		private ChannelReader<object> reader;
		private ChannelWriter<object> writer;

		public BinaryChannelCommunication(ChannelReader<object> reader, ChannelWriter<object> writer)
		{
			this.reader = reader;
			this.writer = writer;
		}

		public override void Send<T>(T value)
		{
			Task.Run(async () => await writer.WriteAsync(value)).Wait();
		}

		public override Task SendAsync<T>(T value)
		{
			return writer.WriteAsync(value).AsTask();
		}

		public override T Receive<T>()
		{
			return (T)Task.Run(async () => await reader.ReadAsync()).Result;
		}

		public override async Task<T> ReceiveAsync<T>()
		{
			return (T)await reader.ReadAsync();
		}

		public override void Close()
		{
			writer.Complete();
		}
	}
}
