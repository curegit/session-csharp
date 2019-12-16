using System;
using System.Net.Sockets;
using System.Threading.Tasks;

namespace SessionTypes.Net
{
	internal class TcpCommunicator : ICommunicator
	{
		private readonly TcpClient tcpClient;

		private readonly NetworkStream networkStream;

		private readonly ISerializer serializer;

		private readonly IConverter streamLink;

		public TcpCommunicator(TcpClient tcpClient, ISerializer serializer)
		{
			this.tcpClient = tcpClient;
			this.serializer = serializer;
			networkStream = tcpClient.GetStream();
		}

		public void Send<T>(T value)
		{
			serializer.Serialize(value, networkStream);

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
			throw new NotImplementedException();
		}

		public Task<Session<P, P>> CastAsync<P, O>() where P : ProtocolType where O : ProtocolType
		{
			throw new NotImplementedException();
		}

		public Session<P, P> Accept<P>() where P : ProtocolType
		{
			throw new NotImplementedException();
		}

		public Task<Session<P, P>> AcceptAsync<P>() where P : ProtocolType
		{
			throw new NotImplementedException();
		}

		public void Select(Direction direction)
		{
			networkStream.WriteByte((byte)direction);
			networkStream.
		}

		public Task SelectAsync(Direction direction)
		{
			networkStream.WriteAsync(,)
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
			//networkStream.di
			tcpClient.Close();
		}
	}
}
