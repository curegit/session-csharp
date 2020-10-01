using System;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;

namespace Session.Streaming.Net
{
	public static class TcpServer
	{
		public static TcpServer<Z> ToTcpServer<S, Z>(this StreamedProtocol<S, Z> protocol, IPAddress address, int port) where S : Session where Z : Session, new()
		{
			return new TcpServer<Z>(address, port, protocol.Serializer);
		}

		/*
		public static TcpServer<Z, Cons<Z, ZZ>> ToTcpServer<S, SS, Z, ZZ>(this StreamedProtocol<Cons<S, SS>, Cons<Z, ZZ>> protocol, IPAddress address, int port) where S : SessionType where SS : SessionList where Z : SessionType where ZZ : SessionList
		{
			return new TcpServer<Z, Cons<Z, ZZ>>(address, port, protocol.Serializer);
		}*/
	}

	public sealed class TcpServer<S> where S : Session, new()
	{
		private readonly IPAddress address;

		private readonly int port;

		private readonly ISerializer serializer;

		internal TcpServer(IPAddress address, int port, ISerializer serializer)
		{
			this.address = address;
			this.port = port;
			this.serializer = serializer;
		}

		public Task Listen(Action<S> action)
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
						var com = new TcpCommunicator(c, serializer);
						action(Session.Create<S>(com));
					});
				}
			});
		}
	}
}
