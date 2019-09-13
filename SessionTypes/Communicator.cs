using System.Threading.Tasks;

namespace SessionTypes.Binary
{
	internal abstract class Communicator
	{
		public abstract void Send<T>(T value);

		public abstract Task SendAsync<T>(T value);

		public abstract T Receive<T>();

		public abstract Task<T> ReceiveAsync<T>();

		public virtual void Choose(Choice choice)
		{
			Send(choice);
		}

		public virtual Task ChooseAsync(Choice choice)
		{
			return SendAsync(choice);
		}

		public virtual Choice Follow()
		{
			return Receive<Choice>();
		}

		public virtual Task<Choice> FollowAsync()
		{
			return ReceiveAsync<Choice>();
		}

		public virtual void Close() { }
	}
}
