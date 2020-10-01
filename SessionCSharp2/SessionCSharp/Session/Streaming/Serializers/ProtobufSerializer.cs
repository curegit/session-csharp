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
			if (stream is null) throw new ArgumentNullException(nameof(stream));
			if (value is null) throw new ArgumentNullException(nameof(value));
			(value as IMessage ?? throw new Exception()).WriteDelimitedTo(stream);
		}

		public Task SerializeAsync<T>(Stream stream, T value)
		{
			if (stream is null) throw new ArgumentNullException(nameof(stream));
			if (value is null) throw new ArgumentNullException(nameof(value));
			return Task.Run(() => Serialize(stream, value));
		}

		public T Deserialize<T>(Stream stream)
		{
			if (stream is null) throw new ArgumentNullException(nameof(stream));
			var instance = (IMessage)Activator.CreateInstance(typeof(T));
			instance.MergeDelimitedFrom(stream);
			return (T)instance;
		}

		public Task<T> DeserializeAsync<T>(Stream stream)
		{
			if (stream is null) throw new ArgumentNullException(nameof(stream));
			return Task.Run(() => Deserialize<T>(stream));
		}
	}
}
