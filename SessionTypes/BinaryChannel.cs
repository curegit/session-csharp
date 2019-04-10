using System;
using System.Threading;
using System.Threading.Tasks;
using System.Threading.Channels;

namespace SessionTypes.Binary.Threading
{
	public static class BinarySessionChannel<P> where P : SessionType
	{
		private static (Client<P, P> client, Server<P, P> server) New()
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
			var (client, server) = New();
			var threadStart = new ThreadStart(() => threadFunction(server));
			var serverThread = new Thread(threadStart);
			serverThread.Start();
			return client;
		}
	}

	public class BinaryChannelCommunication : BinaryCommunication
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
