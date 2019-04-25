using System.Threading.Tasks;

namespace SessionTypes.Binary
{
	internal abstract class BinaryCommunicator
	{
		public abstract void Send<T>(T value);

		public abstract Task SendAsync<T>(T value);

		public abstract T Receive<T>();

		public abstract Task<T> ReceiveAsync<T>();
	}
}
