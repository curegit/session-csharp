using System.Threading.Tasks;

namespace SessionTypes.Binary
{
	public abstract class BinarySession
	{
		private bool used;

		private readonly BinaryCommunicator communicator;

		internal BinarySession(BinarySession session)
		{
			communicator = session.communicator;
		}

		private protected BinarySession(BinaryCommunicator communicator)
		{
			this.communicator = communicator;
		}

		internal void Send<T>(T value)
		{
			if (used)
			{
				throw new LinearityViolationException();
			}
			else
			{
				used = true;
				communicator.Send(value);
			}
		}

		internal Task<T> ReceiveAsync<T>()
		{
			if (used)
			{
				throw new LinearityViolationException();
			}
			else
			{
				used = true;
				return communicator.ReceiveAsync<T>();
			}	
		}
	}

	public sealed class Server<S, P> : BinarySession where S : SessionType where P : SessionType
	{
		internal Server(BinarySession session) : base(session) { }

		internal Server(BinaryCommunicator communicator) : base(communicator) { }
	}

	public sealed class Client<S, P> : BinarySession where S : SessionType where P : SessionType
	{
		internal Client(BinarySession session) : base(session) { }

		internal Client(BinaryCommunicator communicator) : base(communicator) { }
	}
}
