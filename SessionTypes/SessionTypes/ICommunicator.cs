using System.Threading.Tasks;

namespace SessionTypes
{
	internal interface ICommunicator
	{
		public void Send<T>(T value);

		public Task SendAsync<T>(T value);

		public T Receive<T>();

		public Task<T> ReceiveAsync<T>();

		public void Select(Direction direction);

		public Task SelectAsync(Direction direction);

		public Direction Follow();

		public Task<Direction> FollowAsync();

		public Session<P, P> Cast<P, O>() where P : ProtocolType where O : ProtocolType;

		public Task<Session<P, P>> CastAsync<P, O>() where P : ProtocolType where O : ProtocolType;

		public Session<P, P> Accept<P>() where P : ProtocolType;

		public Task<Session<P, P>> AcceptAsync<P>() where P : ProtocolType;

		public void Close();
	}
}
