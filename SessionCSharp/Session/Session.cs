using System.Threading.Tasks;

namespace Session
{
	public abstract class Session
	{
		private protected Session() { }

		internal abstract void Die();
	}

	public sealed class Session<S, E, P> : Session where S : SessionType where E : SessionStack where P : ProtocolType
	{
		private bool used;

		private readonly ICommunicator communicator;

		internal Session(ICommunicator communicator)
		{
			this.communicator = communicator;
		}

		internal Session<Z, E, P> ToNextSession<Z>() where Z : SessionType
		{
			return new Session<Z, E, P>(communicator);
		}

		internal Session<Z, H, P> ToNextSession<Z, H>() where Z : SessionType where H : SessionStack
		{
			return new Session<Z, H, P>(communicator);
		}

		private void CheckLinearity()
		{
			if (used)
			{
				throw new LinearityException();
			}
			else
			{
				used = true;
			}
		}

		internal void Send()
		{
			CheckLinearity();
			communicator.Send();
		}

		internal Task SendAsync()
		{
			CheckLinearity();
			return communicator.SendAsync();
		}

		internal void Send<T>(T value)
		{
			CheckLinearity();
			communicator.Send(value);
		}

		internal Task SendAsync<T>(T value)
		{
			CheckLinearity();
			return communicator.SendAsync(value);
		}

		internal void Receive()
		{
			CheckLinearity();
			communicator.Receive();
		}

		internal Task ReceiveAsync()
		{
			CheckLinearity();
			return communicator.ReceiveAsync();
		}

		internal T Receive<T>()
		{
			CheckLinearity();
			return communicator.Receive<T>();
		}

		internal Task<T> ReceiveAsync<T>()
		{
			CheckLinearity();
			return communicator.ReceiveAsync<T>();
		}

		internal Session<Z, Empty, Q> CastNewChannel<Z, Q>() where Z : SessionType where Q : ProtocolType
		{
			CheckLinearity();
			return communicator.SendNewChannel<Z, Q>();
		}

		internal Task<Session<Z, Empty, Q>> CastNewChannelAsync<Z, Q>() where Z : SessionType where Q : ProtocolType
		{
			CheckLinearity();
			return communicator.SendNewChannelAsync<Z, Q>();
		}

		internal Session<Z, Empty, Q> AcceptNewChannel<Z, Q>() where Z : SessionType where Q : ProtocolType
		{
			CheckLinearity();
			return communicator.ReceiveNewChannel<Z, Q>();
		}

		internal Task<Session<Z, Empty, Q>> AcceptNewChannelAsync<Z, Q>() where Z : SessionType where Q : ProtocolType
		{
			CheckLinearity();
			return communicator.ReceiveNewChannelAsync<Z, Q>();
		}

		internal void Select(Selection direction)
		{
			CheckLinearity();
			communicator.Select(direction);
		}

		internal Task SelectAsync(Selection direction)
		{
			CheckLinearity();
			return communicator.SelectAsync(direction);
		}

		internal Selection Follow()
		{
			CheckLinearity();
			return communicator.Follow();
		}

		internal Task<Selection> FollowAsync()
		{
			CheckLinearity();
			return communicator.FollowAsync();
		}

		internal void Call()
		{
			CheckLinearity();
		}

		internal void Close()
		{
			CheckLinearity();
			communicator.Close();
		}

		internal override void Die()
		{
			communicator.Die();
		}
	}
}
