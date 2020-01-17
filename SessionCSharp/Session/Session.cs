using System.Threading.Tasks;

namespace Session
{
	public abstract class Session
	{
		private protected readonly ICommunicator communicator;

		private protected Session(ICommunicator communicator)
		{
			this.communicator = communicator;
		}

		internal void Cancel()
		{
			communicator.Cancel();
		}
	}

	public sealed class Session<S, E, P> : Session where S : SessionType where E : SessionStack where P : ProtocolType
	{
		private bool used;

		internal Session(ICommunicator communicator) : base(communicator) { }

		internal Session<Z, E, P> ToNextSession<Z>() where Z : SessionType
		{
			return new Session<Z, E, P>(communicator);
		}

		internal Session<Z, H, P> ToNextSession<Z, H>() where Z : SessionType where H : SessionStack
		{
			return new Session<Z, H, P>(communicator);
		}

		internal void Send()
		{
			TrySpendLinearity();
			communicator.Send();
		}

		internal Task SendAsync()
		{
			TrySpendLinearity();
			return communicator.SendAsync();
		}

		internal void Send<T>(T value)
		{
			TrySpendLinearity();
			communicator.Send(value);
		}

		internal Task SendAsync<T>(T value)
		{
			TrySpendLinearity();
			return communicator.SendAsync(value);
		}

		internal void Receive()
		{
			TrySpendLinearity();
			communicator.Receive();
		}

		internal Task ReceiveAsync()
		{
			TrySpendLinearity();
			return communicator.ReceiveAsync();
		}

		internal T Receive<T>()
		{
			TrySpendLinearity();
			return communicator.Receive<T>();
		}

		internal Task<T> ReceiveAsync<T>()
		{
			TrySpendLinearity();
			return communicator.ReceiveAsync<T>();
		}

		internal Session<Z, Empty, Q> ThrowNewChannel<Z, Q>() where Z : SessionType where Q : ProtocolType
		{
			TrySpendLinearity();
			return communicator.ThrowNewChannel<Z, Q>();
		}

		internal Task<Session<Z, Empty, Q>> ThrowNewChannelAsync<Z, Q>() where Z : SessionType where Q : ProtocolType
		{
			TrySpendLinearity();
			return communicator.ThrowNewChannelAsync<Z, Q>();
		}

		internal Session<Z, Empty, Q> CatchNewChannel<Z, Q>() where Z : SessionType where Q : ProtocolType
		{
			TrySpendLinearity();
			return communicator.CatchNewChannel<Z, Q>();
		}

		internal Task<Session<Z, Empty, Q>> CatchNewChannelAsync<Z, Q>() where Z : SessionType where Q : ProtocolType
		{
			TrySpendLinearity();
			return communicator.CatchNewChannelAsync<Z, Q>();
		}

		internal void Select(Selection selection)
		{
			TrySpendLinearity();
			communicator.Select(selection);
		}

		internal Task SelectAsync(Selection selection)
		{
			TrySpendLinearity();
			return communicator.SelectAsync(selection);
		}

		internal Selection Follow()
		{
			TrySpendLinearity();
			return communicator.Follow();
		}

		internal Task<Selection> FollowAsync()
		{
			TrySpendLinearity();
			return communicator.FollowAsync();
		}

		internal void Call()
		{
			TrySpendLinearity();
		}

		internal void Close()
		{
			TrySpendLinearity();
			communicator.Close();
		}

		private void TrySpendLinearity()
		{
			if (used)
			{
				throw new LinearityViolationException();
			}
			else
			{
				used = true;
			}
		}
	}
}
