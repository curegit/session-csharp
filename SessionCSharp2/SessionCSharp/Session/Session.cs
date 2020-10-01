using System;
using System.ComponentModel;
using System.Threading.Tasks;

namespace Session
{
	public abstract class Session
	{
		private bool used;

		private Task? lastTask;

		private protected ICommunicator communicator;

		private protected Session()
		{

		}

		private protected Session(Session session)
		{
			communicator = session.communicator;
			lastTask = session.lastTask;
		}

		private protected Session(ICommunicator communicator)
		{
			this.communicator = communicator;
		}

		internal void Cancel()
		{
			communicator.Cancel();
		}

		internal void WaitForLastTask()
		{
			lastTask?.Wait();
			lastTask = null;
		}

		internal async Task AwaitLastTask()
		{
			if (lastTask != null)
			{
				await lastTask;
				lastTask = null;
			}
		}

		protected Task ContinueAsync(Func<Task> continuation)
		{
			async Task AppendContinuation()
			{
				if (lastTask != null)
				{
					await lastTask;
				}
				await continuation();
			}
			var task = AppendContinuation();
			lastTask = task;
			return task;
		}

		protected Task<T> ContinueAsync<T>(Func<Task<T>> continuation)
		{
			async Task<T> AppendContinuation()
			{
				if (lastTask != null)
				{
					await lastTask;
				}
				return await continuation();
			}
			var task = AppendContinuation();
			lastTask = task;
			return task;
		}

		[EditorBrowsable(EditorBrowsableState.Never)]
		public override string ToString()
		{
			return base.ToString();
		}

		[EditorBrowsable(EditorBrowsableState.Never)]
		public override bool Equals(object obj)
		{
			return base.Equals(obj);
		}

		[EditorBrowsable(EditorBrowsableState.Never)]
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		internal S ToNextSession<S>() where S : Session
		{
			var s = (S)Activator.CreateInstance(typeof(S), true);
			s.communicator = communicator;
			s.lastTask = lastTask;
			return s;
		}

		internal S Duplicate<S>() where S : Session
		{
			return ToNextSession<S>();
		}

		internal static S Create<S>(ICommunicator communicator) where S : Session
		{
			var s = (S)Activator.CreateInstance(typeof(S), true);
			s.communicator = communicator;
			return s;
		}

		internal void Send()
		{
			TrySpendLinearity();
			WaitForLastTask();
			communicator.Send();
		}

		internal Task SendAsync()
		{
			TrySpendLinearity();
			return ContinueAsync(() => communicator.SendAsync());
		}

		internal void Send<T>(T value)
		{
			TrySpendLinearity();
			WaitForLastTask();
			communicator.Send(value);
		}

		internal Task SendAsync<T>(T value)
		{
			TrySpendLinearity();
			return ContinueAsync(() => communicator.SendAsync(value));
		}

		internal void Receive()
		{
			TrySpendLinearity();
			WaitForLastTask();
			communicator.Receive();
		}

		internal Task ReceiveAsync()
		{
			TrySpendLinearity();
			return ContinueAsync(() => communicator.ReceiveAsync());
		}

		internal T Receive<T>()
		{
			TrySpendLinearity();
			WaitForLastTask();
			return communicator.Receive<T>();
		}

		internal Task<T> ReceiveAsync<T>()
		{
			TrySpendLinearity();
			return ContinueAsync(() => communicator.ReceiveAsync<T>());
		}

		internal Z ThrowNewChannel<Z>() where Z : Session
		{
			TrySpendLinearity();
			WaitForLastTask();
			return communicator.ThrowNewChannel<Z>();
		}

		internal Task<Z> ThrowNewChannelAsync<Z>() where Z : Session
		{
			TrySpendLinearity();
			return ContinueAsync(() => communicator.ThrowNewChannelAsync<Z>());
		}

		internal Z CatchNewChannel<Z>() where Z : Session
		{
			TrySpendLinearity();
			WaitForLastTask();
			return communicator.CatchNewChannel<Z>();
		}

		internal Task<Z> CatchNewChannelAsync<Z>() where Z : Session
		{
			TrySpendLinearity();
			return ContinueAsync(() => communicator.CatchNewChannelAsync<Z>());
		}

		internal void Select(Selection selection)
		{
			TrySpendLinearity();
			WaitForLastTask();
			communicator.Select(selection);
		}

		internal Task SelectAsync(Selection selection)
		{
			TrySpendLinearity();
			return ContinueAsync(() => communicator.SelectAsync(selection));
		}

		internal Selection Follow()
		{
			TrySpendLinearity();
			WaitForLastTask();
			return communicator.Follow();
		}

		internal Task<Selection> FollowAsync()
		{
			TrySpendLinearity();
			return ContinueAsync(() => communicator.FollowAsync());
		}

		internal void CallSimply()
		{
			TrySpendLinearity();
		}

		internal void Close()
		{
			TrySpendLinearity();
			communicator.Close();
		}

		internal async Task CloseAsync()
		{
			TrySpendLinearity();
			await AwaitLastTask();
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
