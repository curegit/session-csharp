using System.Net.Sockets;

namespace Session.Streaming.Net
{
	public static class TcpFactory
	{
		public static TcpClient<S, P> CreateTcpClient<S, P, Z, Q>(this StreamedProtocol<S, P, Z, Q> protocol) where S : SessionType where P : ProtocolType where Z : SessionType where Q : ProtocolType
		{
			return new TcpClient<S, P>(protocol.Serializer, protocol.Transform);
		}

		public static TcpClient<S, P> CreateTcpClient<S, P, Z, Q>(this StreamedProtocol<S, P, Z, Q> protocol, AddressFamily family) where S : SessionType where P : ProtocolType where Z : SessionType where Q : ProtocolType
		{
			return new TcpClient<S, P>(protocol.Serializer, protocol.Transform, family);
		}
	}
}
