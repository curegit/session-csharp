using System;
using System.IO;
using System.Buffers.Binary;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace Session.Streaming.Serializers
{
    public sealed class BasicSerializer : ISerializer
    {
        private readonly JsonSerializerOptions options;

        public BasicSerializer()
        {
            options = new()
            {
                IncludeFields = true
            };
        }

        public void Serialize<T>(Stream stream, T value)
        {
            ArgumentNullException.ThrowIfNull(stream);
            ArgumentNullException.ThrowIfNull(value);
            var json = JsonSerializer.Serialize(value, options);
            var data = Encoding.UTF8.GetBytes(json);
            var length = data.Length;
            var len = IntToBytes(length);
            stream.Write(len);
            stream.Write(data);
            stream.Flush();
        }

        public async Task SerializeAsync<T>(Stream stream, T value)
        {
            ArgumentNullException.ThrowIfNull(stream);
            ArgumentNullException.ThrowIfNull(value);
            var json = JsonSerializer.Serialize(value, options);
            var data = Encoding.UTF8.GetBytes(json);
            var length = data.Length;
            var len = IntToBytes(length);
            await stream.WriteAsync(len).ConfigureAwait(false);
            await stream.WriteAsync(data).ConfigureAwait(false);
            await stream.FlushAsync().ConfigureAwait(false);
        }

        public T Deserialize<T>(Stream stream)
        {
            ArgumentNullException.ThrowIfNull(stream);
            var buffer = new byte[4];
            stream.Read(buffer, 0, 4);
            var length = BytesToInt(buffer);
            var dataBuffer = new byte[length];
            stream.Read(dataBuffer, 0, length);
            var result = JsonSerializer.Deserialize<T>(dataBuffer, options);
            return result ?? throw new SerializationException();
        }

        public async Task<T> DeserializeAsync<T>(Stream stream)
        {
            ArgumentNullException.ThrowIfNull(stream);
            var buffer = new byte[4];
            await stream.ReadAsync(buffer, 0, 4).ConfigureAwait(false);
            var length = BytesToInt(buffer);
            var dataBuffer = new byte[length];
            await stream.ReadAsync(dataBuffer, 0, length).ConfigureAwait(false);
            var result = JsonSerializer.Deserialize<T>(dataBuffer, options);
            return result ?? throw new SerializationException();
        }

        private static byte[] IntToBytes(int n)
        {
            byte[] buffer = new byte[4];
            BinaryPrimitives.WriteInt32BigEndian(buffer, n);
            return buffer;
        }

        private static int BytesToInt(byte[] bytes)
        {
            return BinaryPrimitives.ReadInt32BigEndian(bytes);
        }
    }
}
