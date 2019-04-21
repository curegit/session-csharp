using System;
using System.Threading;
using System.Threading.Tasks;
using System.Threading.Channels;

namespace SessionTypes.Binary.Threading
{
	public static class BinarySessionChannel<P> where P : SessionType
	{
		private static (Server<P, P> server, Client<P, P> client) NewChannel()
		{
			var up = Channel.CreateUnbounded<object>();
			var down = Channel.CreateUnbounded<object>();
			return (NewServer(up, down), NewClient(up, down));
		}

		private static Server<P, P> NewServer(Channel<object> up, Channel<object> down)
		{
			var c = new BinaryChannelCommunication(up.Reader, down.Writer);
			return new Server<P, P>(c);
		}

		private static Client<P, P> NewClient(Channel<object> up, Channel<object> down)
		{
			var c = new BinaryChannelCommunication(down.Reader, up.Writer);
			return new Client<P, P>(c);
		}

		public static Client<P, P> Fork(Action<Server<P, P>> threadFunction)
		{
			var (server, client) = NewChannel();
			var threadStart = new ThreadStart(() => threadFunction(server));
			var serverThread = new Thread(threadStart);
			serverThread.Start();
			return client;
		}

		public static (Server<P, P>, Client<P, P>) Pipeline<A>(Action<Server<P, P>, Client<P, P>, A> threadFunction, A[] args)
		{
			int n = args.Length + 1;
			Server<P, P>[] servers = new Server<P, P>[n];
			Client<P, P>[] clients = new Client<P, P>[n];
			for (int i = 0; i < n; i++)
			{
				var (s, c) = NewChannel();
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
			return (servers[0], clients[0]);
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

		public override async void Send<T>(T value)
		{
			await writer.WriteAsync(value);
		}

		public override async Task<T> ReceiveAsync<T>()
		{
			await reader.WaitToReadAsync();
			reader.TryRead(out var obj);
			return (T)obj;
		}
	}
}
