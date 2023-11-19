namespace Session.Streaming.Net
{
    public static class TcpFactory
    {
        public static TcpClient<S, Cons<S, Nil>> CreateTcpClient<S, Z>(this StreamedProtocol<S, Z> protocol) where S : SessionType where Z : SessionType
        {
            return new TcpClient<S, Cons<S, Nil>>(protocol.Serializer);
        }

        public static TcpClient<S, Cons<S, SS>> CreateTcpClient<S, SS, Z, ZZ>(this StreamedProtocol<Cons<S, SS>, Cons<Z, ZZ>> protocol) where S : SessionType where SS : SessionList where Z : SessionType where ZZ : SessionList
        {
            return new TcpClient<S, Cons<S, SS>>(protocol.Serializer);
        }
    }
}
