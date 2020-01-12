using System;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Threading.Tasks;

namespace Session.Streaming.Serializers
{
	public sealed class BinarySerializer : ISerializer
	{
		private readonly BinaryFormatter binaryFormatter;

		public BinarySerializer()
		{
			binaryFormatter = new BinaryFormatter();
		}

		public BinarySerializer(ISurrogateSelector selector, StreamingContext context)
		{
			binaryFormatter = new BinaryFormatter(selector, context);
		}

		public void Serialize<T>(Stream stream, T value)
		{
			if (stream is null) throw new ArgumentNullException(nameof(stream));
			if (value is null) throw new ArgumentNullException(nameof(value));
			binaryFormatter.Serialize(stream, value);
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
			return (T)binaryFormatter.Deserialize(stream);
		}

		public Task<T> DeserializeAsync<T>(Stream stream)
		{
			if (stream is null) throw new ArgumentNullException(nameof(stream));
			return Task.Run(() => Deserialize<T>(stream));
		}
	}
}
