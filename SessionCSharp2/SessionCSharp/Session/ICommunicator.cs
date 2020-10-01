using System.Threading.Tasks;

namespace Session
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

		public S ThrowNewChannel<S>() where S : Session;

		public Task<S> ThrowNewChannelAsync<S>() where S : Session;

		public S CatchNewChannel<S>() where S : Session;

		public Task<S> CatchNewChannelAsync<S>() where S : Session;

		public void Close();

		public void Cancel();
	}
}
