using System;
using System.IO;
using System.Threading.Tasks;
using Google.Protobuf;

namespace Session.Streaming.Serializers
{
    public sealed class ProtobufSerializer : ISerializer
    {
        public void Serialize<T>(Stream stream, T value)
        {
            ArgumentNullException.ThrowIfNull(stream);
            ArgumentNullException.ThrowIfNull(value);
            (value as IMessage ?? throw new ArgumentException()).WriteDelimitedTo(stream);
        }

        public Task SerializeAsync<T>(Stream stream, T value)
        {
            ArgumentNullException.ThrowIfNull(stream);
            ArgumentNullException.ThrowIfNull(value);
            return Task.Run(() => Serialize(stream, value));
        }

        public T Deserialize<T>(Stream stream)
        {
            ArgumentNullException.ThrowIfNull(stream);
            var instance = (IMessage)(Activator.CreateInstance(typeof(T)) ?? throw new Exception());
            instance.MergeDelimitedFrom(stream);
            return (T)instance;
        }

        public Task<T> DeserializeAsync<T>(Stream stream)
        {
            ArgumentNullException.ThrowIfNull(stream);
            return Task.Run(() => Deserialize<T>(stream));
        }
    }
}
