using System.IO;
using System.Threading.Tasks;

namespace Session.Streaming
{
    public interface ISerializer
    {
        public void Serialize<T>(Stream stream, T value);

        public Task SerializeAsync<T>(Stream stream, T value);

        public T Deserialize<T>(Stream stream);

        public Task<T> DeserializeAsync<T>(Stream stream);
    }
}
