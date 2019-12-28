using System.Threading.Tasks;

namespace SessionTypes
{
	internal interface ICommunicator
	{
		public void Send();

		public void Send<T>(T value);

		public Task SendAsync();

		public Task SendAsync<T>(T value);

		public void Receive();

		public T Receive<T>();

		public Task ReceiveAsync();

		public Task<T> ReceiveAsync<T>();

		public void Select(Selection direction);

		public Task SelectAsync(Selection direction);

		public Selection Follow();

		public Task<Selection> FollowAsync();

		public Session<S, Empty, P> CastNewChannel<S, P>() where S : SessionType where P : ProtocolType;

		public Task<Session<S, Empty, P>> CastNewChannelAsync<S, P>() where S : SessionType where P : ProtocolType;

		public Session<S, Empty, P> AcceptNewChannel<S, P>() where S : SessionType where P : ProtocolType;

		public Task<Session<S, Empty, P>> AcceptNewChannelAsync<S, P>() where S : SessionType where P : ProtocolType;

		public void Close();

		public void Die();
	}
}
