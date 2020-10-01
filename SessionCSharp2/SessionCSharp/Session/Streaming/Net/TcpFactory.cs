using System.Net.Sockets;

namespace Session.Streaming.Net
{
	public static class TcpFactory
	{
		public static TcpClient<S> CreateTcpClient<S, Z>(this StreamedProtocol<S, Z> protocol) where S : Session, new() where Z : Session, new()
		{
			return new TcpClient<S>(protocol.Serializer);
		}

		/*
		public static TcpClient<S, Cons<S, SS>> CreateTcpClient<S, SS, Z, ZZ>(this StreamedProtocol<Cons<S, SS>, Cons<Z, ZZ>> protocol) where S : Session where SS : SessionList where Z : SessionType where ZZ : SessionList
		{
			return new TcpClient<S, Cons<S, SS>>(protocol.Serializer);
		}*/
	}
}
