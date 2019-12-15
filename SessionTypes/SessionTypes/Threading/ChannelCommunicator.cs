using System;
using System.Threading;
using System.Threading.Tasks;
using System.Threading.Channels;

using System.Collections.Generic;

namespace SessionTypes.Threading
{
	internal class ChannelCommunicator : ICommunicator
	{
		private ChannelReader<object> reader;
		private ChannelWriter<object> writer;

		public ChannelCommunicator(ChannelReader<object> reader, ChannelWriter<object> writer)
		{
			this.reader = reader;
			this.writer = writer;
		}

		public void Send<T>(T value)
		{
			Task.Run(async () => await writer.WriteAsync(value)).Wait();
		}

		public Task SendAsync<T>(T value)
		{
			return writer.WriteAsync(value).AsTask();
		}

		public T Receive<T>()
		{
			return (T)Task.Run(async () => await reader.ReadAsync()).Result;
		}

		public async Task<T> ReceiveAsync<T>()
		{
			return (T)await reader.ReadAsync();
		}



		public Session<P, P> Cast<P, O>() where P : ProtocolType where O : ProtocolType
		{
			var (c, s) = Channel.NewChannel<P, O>();
			Send(s);
			return c;
		}

		public Task<Session<P, P>> CastAsync<P, O>() where P : ProtocolType where O : ProtocolType
		{
			throw new NotImplementedException();
		}

		public Session<P, P> Accept<P>() where P : ProtocolType
		{
			return Receive<Session<P, P>>();
		}

		public Task<Session<P, P>> AcceptAsync<P>() where P : ProtocolType
		{
			throw new NotImplementedException();
		}

		public void Select(Direction direction)
		{
			Send(direction);
		}

		public Task SelectAsync(Direction direction)
		{
			return SendAsync(direction);
		}

		public Direction Follow()
		{
			return Receive<Direction>();
		}

		public Task<Direction> FollowAsync()
		{
			return ReceiveAsync<Direction>();
		}

		public void Close()
		{
			writer.Complete();
		}
	}
}
