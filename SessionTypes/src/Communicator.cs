using System.Threading.Tasks;

namespace SessionTypes
{
	internal abstract class Communicator
	{
		public abstract void Send<T>(T value);

		public abstract Task SendAsync<T>(T value);

		public abstract T Receive<T>();

		public abstract Task<T> ReceiveAsync<T>();

		public abstract Session<P, P> AddSend<P, O>() where P : ProtocolType where O : ProtocolType;

		public abstract Task<Session<P, P>> AddSendAsync<P, O>() where P : ProtocolType where O : ProtocolType;

		public abstract Session<P, P> AddReceive<P>() where P : ProtocolType;

		public abstract Task<Session<P, P>> AddReceiveAsync<P>() where P : ProtocolType;

		public virtual void Select(Direction direction)
		{
			Send(direction);
		}

		public virtual Task SelectAsync(Direction direction)
		{
			return SendAsync(direction);
		}

		public virtual Direction Follow()
		{
			return Receive<Direction>();
		}

		public virtual Task<Direction> FollowAsync()
		{
			return ReceiveAsync<Direction>();
		}

		public virtual void Close() { }
	}
}
