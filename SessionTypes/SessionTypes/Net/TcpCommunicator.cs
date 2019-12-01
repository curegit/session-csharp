using System;
using System.Threading;
using System.Threading.Tasks;
using System.Threading.Channels;

using System.Collections.Generic;

namespace SessionTypes.Threading
{
	internal class BinaryChannelCommunicator : Communicator
	{
		private ChannelReader<object> reader;
		private ChannelWriter<object> writer;

		public TcpCommunicator(ChannelReader<object> reader, ChannelWriter<object> writer)
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

		public override Session<P, P> AddSend<P, O>()
		{
			var (c, s) = BinaryChannel.NewChannel<P, O>();
			Send(s);
			return c;
		}

		public override Task<Session<P, P>> AddSendAsync<P, O>()
		{
			throw new NotImplementedException();
		}

		public override Session<P, P> AddReceive<P>()
		{
			return Receive<Session<P, P>>();
		}

		public override Task<Session<P, P>> AddReceiveAsync<P>()
		{
			throw new NotImplementedException();
		}

		public override void Close()
		{
			writer.Complete();
		}
	}
}
