using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Session
{


	public class SessionDieList : IDisposable
	{
		private List<Session> sessions;

		public SessionDieList()
		{
			sessions = new List<Session>();
		}

		public void Add(Session session)
		{
			sessions.Add(session);
		}

		public void Dispose()
		{
			foreach(var s in sessions)
			{
				s.Die();
			}
		}
	}
}
