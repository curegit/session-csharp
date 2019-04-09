using System.Threading.Tasks;

namespace SessionTypes.Binary
{
	public abstract class BinaryCommunication
	{
		public abstract void Send<T>(T value);

		public abstract Task<T> ReceiveAsync<T>();

		//public abstract void Close();
	}
}
