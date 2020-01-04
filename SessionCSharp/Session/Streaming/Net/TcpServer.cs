using System;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;

namespace Session.Streaming.Net
{
	public static class TcpServer
	{
		public static TcpServer<Z, Q> ToTcpServer<S, P, Z, Q>(this StreamedProtocol<S, P, Z, Q> protocol, IPAddress address, int port) where S :SessionType where P : ProtocolType where Z : SessionType where Q : ProtocolType
		{
			return new TcpServer<Z, Q>(address, port, protocol.Serializer, protocol.Transform);
		}
	}

	public sealed class TcpServer<S, P> where S : SessionType where P : ProtocolType
	{
		private readonly IPAddress address;

		private readonly int port;

		private readonly ISerializer serializer;

		private readonly ITransform transform;

		internal TcpServer(IPAddress address, int port, ISerializer serializer, ITransform transform)
		{
			this.address = address;
			this.port = port;
			this.serializer = serializer;
			this.transform = transform;
		}

		public Task Listen(Action<Session<S, Empty, P>> action)
		{
			return Task.Run(async () =>
			{
				var l = new TcpListener(address, port);
				l.Start();
				while (true)
				{
					var c = await l.AcceptTcpClientAsync();
					
					var t = Task.Run(() =>
					{
						var com = new TcpCommunicator(c, serializer, transform);
						action(new Session<S, Empty, P>(com));
					});
				}
			});
		}
	}
}
