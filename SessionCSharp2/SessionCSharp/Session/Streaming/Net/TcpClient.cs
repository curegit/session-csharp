using System;
using System.Threading;
using System.Threading.Tasks;
using System.Threading.Channels;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;

namespace Session.Streaming.Net
{
	public sealed class TcpClient<S> where S : Session, new()
	{
		private readonly TcpClient tcpClient;

		private readonly ISerializer serializer;

		internal TcpClient(ISerializer serializer)
		{
			this.serializer = serializer;
			tcpClient = new TcpClient();
		}

		internal TcpClient(ISerializer serializer, AddressFamily family)
		{
			this.serializer = serializer;
			tcpClient = new TcpClient(family);
			tcpClient.NoDelay = true;
		}

		public S Connect(IPEndPoint endPoint)
		{
			tcpClient.Connect(endPoint);
			var com = new TcpCommunicator(tcpClient, serializer);
			return Session.Create<S>(com);
		}

		public S Connect(IPAddress address, int port)
		{
			tcpClient.Connect(address, port);
			var com = new TcpCommunicator(tcpClient, serializer);
			return Session.Create<S>(com);
		}
	}
}
