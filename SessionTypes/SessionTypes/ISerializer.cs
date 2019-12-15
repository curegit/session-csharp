using System.IO;

namespace SessionTypes
{
	public interface ISerializer<T>
	{
		//public byte[] Serialize(T obj);

		public void Serialize(T obj, Stream stream);

		//public T Deserialize(byte[] data);

		public T Deserialize(Stream stream);
	}
}
