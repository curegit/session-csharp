using System;
using System.Threading.Tasks;
using System.Threading.Channels;

namespace Session.Threading
{
	internal class ChannelCommunicator : ICommunicator
	{
		private static readonly int unit = 0;

		private ChannelReader<object> reader;

		private ChannelWriter<object> writer;

		public ChannelCommunicator(ChannelReader<object> reader, ChannelWriter<object> writer)
		{
			this.reader = reader;
			this.writer = writer;
		}

		public void Send() => Send(unit);

		public void Send<T>(T value)
		{
			Task.Run(async () => await writer.WriteAsync(value)).Wait();
		}

		public Task SendAsync() => SendAsync(unit);

		public Task SendAsync<T>(T value)
		{
			return writer.WriteAsync(value).AsTask();
		}

		public void Receive() => Receive<int>();

		public T Receive<T>()
		{
			return (T)Task.Run(async () => await reader.ReadAsync()).Result;
		}

		public async Task ReceiveAsync()
		{
			await ReceiveAsync<int>();
		}

		public async Task<T> ReceiveAsync<T>()
		{
			return (T)await reader.ReadAsync();
		}



		public Session<S, Empty, P> ThrowNewChannel<S, P>() where S : SessionType where P : ProtocolType
		{
			var (c, s) = ChannelFactory.Create();
			Send(s);
			return new Session<S, Empty, P>(c);
		}

		public async Task<Session<S, Empty, P>> ThrowNewChannelAsync<S, P>() where S : SessionType where P : ProtocolType
		{
			var (c, s) = ChannelFactory.Create();
			await SendAsync(s);
			return new Session<S, Empty, P>(c);
		}

		public Session<S, Empty, P> CatchNewChannel<S, P>() where S : SessionType where P : ProtocolType
		{
			return new Session<S, Empty, P>(Receive<ChannelCommunicator>());
		}

		public async Task<Session<S, Empty, P>> CatchNewChannelAsync<S, P>() where S : SessionType where P : ProtocolType
		{
			return new Session<S, Empty, P>(await ReceiveAsync<ChannelCommunicator>());
		}

		public void Select(Selection direction)
		{
			Send(direction);
		}

		public Task SelectAsync(Selection direction)
		{
			return SendAsync(direction);
		}

		public Selection Follow()
		{
			return Receive<Selection>();
		}

		public Task<Selection> FollowAsync()
		{
			return ReceiveAsync<Selection>();
		}

		public void Close()
		{
			writer.Complete();
		}

		public void Cancel()
		{
			writer.Complete();
		}
	}
}
