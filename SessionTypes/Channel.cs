using System;
using System.Threading;
using System.Threading.Tasks;
using System.Threading.Channels;

namespace SessionTypes.Threading
{
	public static class BinaryChannel
	{
		private static Session<C, C> NewClient<C>(Channel<object> up, Channel<object> down) where C : ProtocolType
		{
			var c = new BinaryChannelCommunicator(down.Reader, up.Writer);
			return new Session<C, C>(c);
		}

		private static Session<S, S> NewServer<S>(Channel<object> up, Channel<object> down) where S : ProtocolType
		{
			var c = new BinaryChannelCommunicator(up.Reader, down.Writer);
			return new Session<S, S>(c);
		}

		private static (Session<C, C> client, Session<S, S> server) NewChannel<C, S>() where C : ProtocolType where S : ProtocolType
		{
			var up = Channel.CreateUnbounded<object>();
			var down = Channel.CreateUnbounded<object>();
			return (NewClient<C>(up, down), NewServer<S>(up, down));
		}

		public static Session<C, C> Fork<C, S>(this Protocol<C, S> protocol, Action<Session<S, S>> threadFunction) where C : ProtocolType where S : ProtocolType
		{
			var (client, server) = NewChannel<C, S>();
			var threadStart = new ThreadStart(() => threadFunction(server));
			var serverThread = new Thread(threadStart);
			serverThread.Start();
			return client;
		}

		public static Session<C, C>[] Distribute<C, S, A>(this Protocol<C, S> protocol, Action<Session<S, S>, A> threadFunction, A[] args) where C : ProtocolType where S : ProtocolType
		{
			int n = args.Length;
			var clients = new Session<C, C>[n];
			var servers = new Session<S, S>[n];
			for (int i = 0; i < n; i++)
			{
				var threadNumber = i;
				var (c, s) = NewChannel<C, S>();
				clients[threadNumber] = c;
				servers[threadNumber] = s;
				var threadStart = new ThreadStart(() => threadFunction(servers[threadNumber], args[threadNumber]));
				var thread = new Thread(threadStart);
				thread.Start();
			}
			return clients;
		}

		public static (Session<C, C>, Session<S, S>) Pipeline<C, S, A>(this Protocol<C, S> protocol, Action<Session<S, S>, Session<C, C>, A> threadFunction, A[] args) where C : ProtocolType where S : ProtocolType
		{
			int n = args.Length + 1;
			Session<C, C>[] clients = new Session<C, C>[n];
			Session<S, S>[] servers = new Session<S, S>[n];
			for (int i = 0; i < n; i++)
			{
				var (c, s) = NewChannel<C, S>();
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

	internal class BinaryChannelCommunicator : Communicator
	{
		private ChannelReader<object> reader;
		private ChannelWriter<object> writer;

		public BinaryChannelCommunicator(ChannelReader<object> reader, ChannelWriter<object> writer)
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
