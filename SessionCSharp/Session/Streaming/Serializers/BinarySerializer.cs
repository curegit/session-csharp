using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace Session.Streaming.Serializers
{
	public sealed class BinarySerializer : ISerializer
	{
		private readonly BinaryFormatter binaryFormatter;

		public BinarySerializer()
		{
			binaryFormatter = new BinaryFormatter();
		}

		public void Serialize<T>(Stream stream, T value)
		{
			binaryFormatter.Serialize(stream, value);
		}

		public T Deserialize<T>(Stream stream)
		{
			return (T)binaryFormatter.Deserialize(stream);
		}
	}
}
