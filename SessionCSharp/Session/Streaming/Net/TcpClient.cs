using System.Net;
using System.Net.Sockets;

namespace Session.Streaming.Net
{
    public sealed class TcpClient<S, P> where S : SessionType where P : ProtocolType
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

        public Session<S, Empty, P> Connect(IPEndPoint endPoint)
        {
            tcpClient.Connect(endPoint);
            var com = new TcpCommunicator(tcpClient, serializer);
            return new Session<S, Empty, P>(com);
        }

        public Session<S, Empty, P> Connect(IPAddress address, int port)
        {
            tcpClient.Connect(address, port);
            var com = new TcpCommunicator(tcpClient, serializer);
            return new Session<S, Empty, P>(com);
        }
    }
}
