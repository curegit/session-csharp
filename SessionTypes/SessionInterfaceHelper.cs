using System;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace SessionTypes
{
	public static class SessionInterfaceHelper
	{
		public static Session<S, P> Let<S, P, T>(this Session<S, P> session, out T variable, T value) where S : SessionType where P : ProtocolType
		{
			variable = value;
			return session;
		}

		public static Session<S, P> Bind<S, P, T>(this (Session<S, P> session, T value) received, out T variable) where S : SessionType where P : ProtocolType
		{
			variable = received.value;
			return received.session;
		}

		//public static Task<Session<>, >(this Task<Session<>>)

		public static (IEnumerable<Session<SN, P>> s, IEnumerable<Session<S, P>> rs, IEnumerable<T> rargs) ZipSessions<S, P, SN, T>(this IEnumerable<Session<S, P>> sessions, IEnumerable<T> args, Func<Session<S, P>, T, Session<SN, P>> f) where S : SessionType where P : ProtocolType where SN : SessionType
		{
			
		}

		public static (IEnumerable<Task<(Session<SN, P>, H)>> s, IEnumerable<Session<S, P>> rs, IEnumerable<T> rargs) ZipSessions<S, P, SN, T, H>(this IEnumerable<Session<S, P>> sessions, IEnumerable<T> args, Func<Session<S, P>, T, Task<(Session<SN, P>, H)>> f) where S : SessionType where P : ProtocolType where SN : SessionType
		{

		}
	}
}
