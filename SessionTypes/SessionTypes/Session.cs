using System.Threading.Tasks;

namespace SessionTypes
{
	public sealed class Session<S, P> where S : ProtocolType where P : ProtocolType
	{
		private bool used;

		private readonly ICommunicator communicator;

		// TODO
		public bool Alive => true;
		// TODO
		public bool Used => used;

		internal Session(ICommunicator communicator)
		{
			this.communicator = communicator;
		}

		internal Session<N, P> ToNextSession<N>() where N : SessionType
		{
			return new Session<N, P>(communicator);
		}

		// TODO: Goto等の線形性検査

		private void Consume()
		{
			lock (communicator)
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
		}

		internal void Send<T>(T value)
		{
			Consume();
			communicator.Send(value);
		}

		internal Task SendAsync<T>(T value)
		{
			if (used)
			{
				throw new LinearityException();
			}
			else
			{
				used = true;
				return communicator.SendAsync(value);
			}
		}

		internal T Receive<T>()
		{
			if (used)
			{
				throw new LinearityException();
			}
			else
			{
				used = true;
				return communicator.Receive<T>();
			}
		}

		internal Task<T> ReceiveAsync<T>()
		{
			if (used)
			{
				throw new LinearityException();
			}
			else
			{
				used = true;
				return communicator.ReceiveAsync<T>();
			}
		}

		internal Session<Q, Q> SendNewChannel<Q, O>() where Q : ProtocolType where O : ProtocolType
		{
			if (used)
			{
				throw new LinearityException();
			}
			else
			{
				used = true;
				return communicator.Cast<Q, O>();
			}
		}

		internal Session<Q, Q> ReceiveNewChannel<Q>() where Q : ProtocolType
		{
			if (used)
			{
				throw new LinearityException();
			}
			else
			{
				used = true;
				return communicator.Accept<Q>();
			}
		}

		internal void Select(Direction direction)
		{
			if (used)
			{
				throw new LinearityException();
			}
			else
			{
				used = true;
				communicator.Select(direction);
			}
		}

		internal Task SelectAsync(Direction direction)
		{
			if (used)
			{
				throw new LinearityException();
			}
			else
			{
				used = true;
				return communicator.SelectAsync(direction);
			}
		}

		internal Direction Follow()
		{
			if (used)
			{
				throw new LinearityException();
			}
			else
			{
				used = true;
				return communicator.Follow();
			}
		}

		internal Task<Direction> FollowAsync()
		{
			if (used)
			{
				throw new LinearityException();
			}
			else
			{
				used = true;
				return communicator.FollowAsync();
			}
		}



		internal void Close()
		{
			if (used)
			{
				throw new LinearityException();
			}
			else
			{
				used = true;
				communicator.Close();
			}
		}
	}
}
