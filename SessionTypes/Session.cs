using System.Threading.Tasks;

namespace SessionTypes
{
	public sealed class Session<S, P> where S : ProtocolType where P : ProtocolType
	{
		private bool used;

		private readonly Communicator communicator;

		internal Session(Communicator communicator)
		{
			this.communicator = communicator;
		}

		internal Session<N, P> ToNext<N>() where N : SessionType
		{
			return new Session<N, P>(communicator);
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

		internal Task SendAsync<T>(T value)
		{
			if (used)
			{
				throw new LinearityViolationException();
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
				throw new LinearityViolationException();
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
				throw new LinearityViolationException();
			}
			else
			{
				used = true;
				return communicator.ReceiveAsync<T>();
			}
		}

		internal void Choose(Choice choice)
		{
			if (used)
			{
				throw new LinearityViolationException();
			}
			else
			{
				used = true;
				communicator.Choose(choice);
			}
		}

		internal Task ChooseAsync(Choice choice)
		{
			if (used)
			{
				throw new LinearityViolationException();
			}
			else
			{
				used = true;
				return communicator.ChooseAsync(choice);
			}
		}

		internal Choice Follow()
		{
			if (used)
			{
				throw new LinearityViolationException();
			}
			else
			{
				used = true;
				return communicator.Follow();
			}
		}

		internal Task<Choice> FollowAsync()
		{
			if (used)
			{
				throw new LinearityViolationException();
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
				throw new LinearityViolationException();
			}
			else
			{
				used = true;
				communicator.Close();
			}
		}
	}
}
