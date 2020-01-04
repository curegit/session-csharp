using System.IO;

namespace Session.Streaming
{
	public interface ISerializer
	{
		public void Serialize<T>(Stream stream, T value);

		//public Task SerializeAsync(object obj, Stream stream);

		public T Deserialize<T>(Stream stream);

		//public Task<object> DeserializeAsync(Stream stream);
	}
}
