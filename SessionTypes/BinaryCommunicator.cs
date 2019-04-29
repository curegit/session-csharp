using System.Threading.Tasks;

namespace SessionTypes.Binary
{
	internal abstract class BinaryCommunicator
	{
		public abstract void Send<T>(T value);

		public abstract Task SendAsync<T>(T value);

		public abstract T Receive<T>();

		public abstract Task<T> ReceiveAsync<T>();

		public virtual void Choose(BinaryChoice choice)
		{
			Send(choice);
		}

		public virtual Task ChooseAsync(BinaryChoice choice)
		{
			return SendAsync(choice);
		}

		public virtual BinaryChoice Follow()
		{
			return Receive<BinaryChoice>();
		}

		public virtual Task<BinaryChoice> FollowAsync()
		{
			return ReceiveAsync<BinaryChoice>();
		}

		public virtual void Close() { }
	}
}
