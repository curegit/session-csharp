using System;
using System.Collections.Generic;

namespace Session
{
	public sealed class SessionCanceller : IDisposable
	{
		private bool open = true;

		private readonly List<Session> sessions = new List<Session>();

		public void Register(Session session)
		{
			if (session is null)
			{
				throw new ArgumentNullException(nameof(session));
			}
			if (open)
			{
				sessions.Add(session);
			}
			else
			{
				throw new ObjectDisposedException(nameof(SessionCanceller));
			}
		}

		public void Dispose()
		{
			if (open)
			{
				open = false;
				var exceptions = new List<Exception>();
				foreach (var session in sessions)
				{
					try
					{
						session.Cancel();
					}
					catch (Exception exception)
					{
						exceptions.Add(exception);
					}
				}
				if (exceptions.Count > 0)
				{
					throw new AggregateException(exceptions);
				}
			}
		}
	}
}
