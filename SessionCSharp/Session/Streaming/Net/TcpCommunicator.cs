using System;
using System.IO;
using System.Net.Sockets;
using System.Threading.Tasks;

namespace Session.Streaming.Net
{
	internal class TcpCommunicator : ICommunicator
	{
		private readonly TcpClient tcpClient;

		private readonly Stream incomingStream;

		private readonly Stream outgoingStream;

		private readonly ISerializer serializer;

		private static readonly byte unit = 0x00;

		public TcpCommunicator(TcpClient tcpClient, ISerializer serializer, ITransform transform)
		{
			this.tcpClient = tcpClient;
			this.serializer = serializer;
			var networkStream = tcpClient.GetStream();
			incomingStream = transform.DecorateIncomingStream(networkStream);
			outgoingStream = transform.DecorateOutgoingStream(networkStream);
		}

		public void Send()
		{
			outgoingStream.WriteByte(unit);
		}

		public void Send<T>(T value)
		{
			serializer.Serialize(outgoingStream, value);
		}

		public Task SendAsync()
		{
			return outgoingStream.WriteAsync(new byte[] { unit }, 0, 1);
		}

		public Task SendAsync<T>(T value)
		{
			return Task.Run(() => serializer.Serialize(outgoingStream, value));
		}

		public void Receive()
		{
			incomingStream.ReadByte();
		}

		public T Receive<T>()
		{
			return serializer.Deserialize<T>(incomingStream);
		}

		public async Task ReceiveAsync()
		{
			await incomingStream.ReadAsync(new byte[1], 0, 1);
		}

		public async Task<T> ReceiveAsync<T>()
		{
			return await Task.Run(() => serializer.Deserialize<T>(incomingStream));
		}

		public void Select(Selection selection)
		{
			outgoingStream.WriteByte((byte)selection);
		}

		public Task SelectAsync(Selection selection)
		{
			return outgoingStream.WriteAsync(new byte[] { (byte)selection }, 0, 1);
		}

		public Selection Follow()
		{
			return (Selection)incomingStream.ReadByte();
		}

		public async Task<Selection> FollowAsync()
		{
			var buffer = new byte[1];
			await incomingStream.ReadAsync(buffer, 0, 1);
			return (Selection)buffer[0];
		}

		public Session<S, Empty, P> ThrowNewChannel<S, P>() where S : SessionType where P : ProtocolType
		{
			throw new NotImplementedException();
		}

		public Task<Session<S, Empty, P>> ThrowNewChannelAsync<S, P>() where S : SessionType where P : ProtocolType
		{
			throw new NotImplementedException();
		}

		public Session<S, Empty, P> CatchNewChannel<S, P>() where S : SessionType where P : ProtocolType
		{
			throw new NotImplementedException();
		}

		public Task<Session<S, Empty, P>> CatchNewChannelAsync<S, P>() where S : SessionType where P : ProtocolType
		{
			throw new NotImplementedException();
		}

		public void Close()
		{
			incomingStream.Dispose();
			outgoingStream.Dispose();
			tcpClient.Close();
		}

		public void Die()
		{
			incomingStream.Dispose();
			outgoingStream.Dispose();
			tcpClient.Dispose();
		}
	}
}
