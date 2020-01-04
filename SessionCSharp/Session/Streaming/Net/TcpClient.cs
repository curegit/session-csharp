using System;
using System.Threading;
using System.Threading.Tasks;
using System.Threading.Channels;

using System.Collections.Generic;

using System.Net;
using System.Net.Sockets;

namespace Session.Streaming.Net
{
	public static class TcpClientA
	{
		public static TcpClient<S, P> ToTcpClient<S, P, Z, Q>(this StreamedProtocol<S, P, Z, Q> protocol, IPAddress address, int port) where S : SessionType where P : ProtocolType where Z : SessionType where Q : ProtocolType
		{
			return new TcpClient<S, P>(address, port, protocol.Serializer, protocol.Transform);
		}
	}

	public sealed class TcpClient<S, P> where S : SessionType where P : ProtocolType
	{
		private readonly IPAddress address;

		private readonly int port;

		private readonly ISerializer serializer;

		private readonly ITransform transform;

		internal TcpClient(IPAddress address, int port, ISerializer serializer, ITransform transform)
		{
			this.address = address;
			this.port = port;
			this.serializer = serializer;
			this.transform = transform;
		}

		public Session<S, Empty, P> Connect()
		{
			var c = new TcpClient();
			c.Connect(new IPEndPoint(address, port));
			var com = new TcpCommunicator(c, serializer, transform);
			return new Session<S, Empty, P>(com);
		}
	}
}
