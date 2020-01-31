using System;
using System.Collections.Generic;
using System.Text;

namespace Session
{
	using Session.Types;

	public class Session<S, E>
		where S : SessionType
		where E : Protocol
	{
	}

	public static class Communication
	{
		public static Session<S, S> ForkThread<S, T>(this Dual<S, T> p, Action<Session<T, T>> func)
			where S : SessionType
			where T : SessionType
		{
			throw new NotImplementedException();
		}
		public static Session<S, S> ForkThread<S, T>(this Dual<S, T> p, Action<Session<T, T>> func)
			where S : Protocol
			where T : Protocol
		{
			throw new NotImplementedException();
		}

		public static Session<S,E> Send<S,V,E>(this Session<Send<V,S>, E> p, V v)
			where S : SessionType
			where E : Protocol
		{
			throw new NotImplementedException();
		}

		public static Session<S, E> Recv<S, V, E>(this Session<Recv<V, S>, E> p, out V v)
			where S : SessionType
			where E : Protocol
		{
			throw new NotImplementedException();
		}

		public static void Close<E>(this Session<Eps, E> p)
			where E : Protocol
		{
			throw new NotImplementedException();
		}
	}
}
