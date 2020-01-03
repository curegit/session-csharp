using System.IO;

namespace Session.Streaming
{
	public interface ISerializer
	{
		//public byte[] Serialize(T obj);

		public void Serialize(object obj, Stream stream);

		//public T Deserialize(byte[] data);

		public object Deserialize(Stream stream);
	}
}
