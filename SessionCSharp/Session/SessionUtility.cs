using System;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace Session
{
	public static class SessionUtility
	{
		public static Session<S, E, P> Let<S, E, P, T>(this Session<S, E, P> session, out T variable, T value) where S : SessionType where E : SessionStack where P : ProtocolType
		{
			if (session is null) throw new ArgumentNullException(nameof(session));
			variable = value;
			return session;
		}

		public static Session<S, E, P> Wait<S, E, P>(this Session<S, E, P> session) where S : SessionType where E : SessionStack where P : ProtocolType
		{
			if (session is null) throw new ArgumentNullException(nameof(session));
			session.CallSimply();
			session.WaitForLastTask();
			return session.Duplicate();
		}

		public static async Task<Session<S, E, P>> Sync<S, E, P>(this Session<S, E, P> session) where S : SessionType where E : SessionStack where P : ProtocolType
		{
			if (session is null) throw new ArgumentNullException(nameof(session));
			session.CallSimply();
			await session.AwaitLastTask();
			return session.Duplicate();
		}

		/*
		public static void Do<S, P>(this IEnumerable<Session<S, P>> sessions, Action<Session<S, P>> action) where S : ProtocolType where P : ProtocolType
		{
			foreach (var session in sessions)
			{
				action(session);
			}
		}

		
		public static IReadOnlyList<Session<N, P>> Do<S, N, P>(this IEnumerable<Session<S, P>> sessions, Func<Session<S, P>, Session<N, P>> action)
			where S : ProtocolType where N : ProtocolType where P : ProtocolType
		{
			var next = new List<Session<N, P>>();
			foreach (var session in sessions)
			{
				action(session);
			}
			return next;
		}

		// TODO: async Task version
		*/
		// TODO: Task
		public static (IEnumerable<Session<SN, E, P>> s, IEnumerable<Session<S, E, P>> rs, IEnumerable<T> rargs) ZipWith<S, E, P, SN, T>(this IEnumerable<Session<S, E, P>> sessions, IEnumerable<T> args, Func<Session<S, E, P>, T, Session<SN, E, P>> f) where S : SessionType where E : SessionStack where P : ProtocolType where SN : SessionType
		{
			var a = new List<Session<SN, E, P>>();
			var b = new List<Session<S, E, P>>();
			List<T> c = new List<T>();
			//var i = 0;
			var t = sessions.GetEnumerator();
			foreach (var h in args)
			{
				var d = f(t.Current, h);
				t.MoveNext();
			}
			return (a, b, c);
		}
	}
}
