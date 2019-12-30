using System;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace Session
{
	public static class SessionUtility
	{
		public static Session<S, E, P> Let<S, E, P, T>(this Session<S, E, P> session, out T variable, T value) where S : SessionType where E : SessionStack where P : ProtocolType
		{
			variable = value;
			return session;
		}

		public static Session<S, E, P> Assign<S, E, P, T>(this (Session<S, E, P> session, T value) received, out T variable) where S : SessionType where E : SessionStack where P : ProtocolType
		{
			variable = received.value;
			return received.session;
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

		// TODO: Task
		public static (IEnumerable<Session<SN, P>> s, IEnumerable<Session<S, P>> rs, IEnumerable<T> rargs) ZipArgs<S, P, SN, T>(this IEnumerable<Session<S, P>> sessions, IEnumerable<T> args, Func<Session<S, P>, T, Session<SN, P>> f) where S : SessionType where P : ProtocolType where SN : SessionType
		{
			List<Session<SN, P>> a = new List<Session<SN, P>>();
			List<Session<S, P>> b = new List<Session<S, P>>();
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
		*/
	}
}
