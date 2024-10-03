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

        public TcpCommunicator(TcpClient tcpClient, ISerializer serializer)
        {
            this.tcpClient = tcpClient;
            this.serializer = serializer;
            var networkStream = tcpClient.GetStream();
            incomingStream = networkStream;
            outgoingStream = networkStream;
        }

        public void Send()
        {
            outgoingStream.WriteByte(unit);
            outgoingStream.Flush();
        }

        public void Send<T>(T value)
        {
            serializer.Serialize(outgoingStream, value);
            outgoingStream.Flush();
        }

        public Task SendAsync()
        {
            return outgoingStream.WriteAsync([unit], 0, 1).ContinueWith((x) => outgoingStream.FlushAsync());
        }

        public Task SendAsync<T>(T value)
        {
            return serializer.SerializeAsync(outgoingStream, value).ContinueWith((x) => outgoingStream.FlushAsync());
        }

        public void Receive()
        {
            incomingStream.ReadExactly(new byte[1], 0, 1);
        }

        public T Receive<T>()
        {
            return serializer.Deserialize<T>(incomingStream);
        }

        public Task ReceiveAsync()
        {
            return incomingStream.ReadExactlyAsync(new byte[1], 0, 1).AsTask();
        }

        public Task<T> ReceiveAsync<T>()
        {
            return serializer.DeserializeAsync<T>(incomingStream);
        }

        public void Select(Selection selection)
        {
            outgoingStream.WriteByte((byte)selection);
            outgoingStream.Flush();
        }

        public Task SelectAsync(Selection selection)
        {
            return outgoingStream.WriteAsync(new byte[] { selection.ToByte() }, 0, 1).ContinueWith(x => outgoingStream.FlushAsync());
        }

        public Selection Follow()
        {
            return (Selection)incomingStream.ReadByte();
        }

        public async Task<Selection> FollowAsync()
        {
            var buffer = new byte[1];
            await incomingStream.ReadAsync(buffer, 0, 1).ConfigureAwait(false);
            return buffer[0].ToSelection();
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

        public void Cancel()
        {
            incomingStream.Dispose();
            outgoingStream.Dispose();
            tcpClient.Dispose();
        }
    }
}
