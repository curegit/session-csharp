using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

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

		// doesn't work -- why?
		//public static Session<S0, SS> ForkThread<S0, T0, SS, TT>(this DualEnv<SS, TT> p, Action<Session<T0, TT>> func)
		//	where S0 : SessionType
		//	where T0 : SessionType
		//	where SS : EnvHead<S0>
		//	where TT : EnvHead<T0>
		//{
		//	throw new NotImplementedException();
		//}

		public static Session<S0, Env<S0, S1>> ForkThread<S0, S1, T0, T1>(this DualEnv<Env<S0, S1>, Env<T0, T1>> p, Action<Session<T0, Env<T0, T1>>> func)
			where S0 : SessionType
			where S1 : SessionType
			where T0 : SessionType
			where T1 : SessionType
		{
			throw new NotImplementedException();
		}
		public static Session<S0, Env<S0, S1, S2>> ForkThread<S0, S1, S2, T0, T1, T2>(this DualEnv<Env<S0, S1>, Env<T0, T1>> p, Action<Session<T0, Env<T0, T1, T2>>> func)
			where S0 : SessionType
			where S1 : SessionType
			where S2 : SessionType
			where T0 : SessionType
			where T1 : SessionType
			where T2 : SessionType
		{
			throw new NotImplementedException();
		}
		public static Session<S0, Env<S0, S1, S2, S3>> ForkThread<S0, S1, S2, S3, T0, T1, T2, T3>(this DualEnv<Env<S0, S1>, Env<T0, T1>> p, Action<Session<T0, Env<T0, T1, T2, T3>>> func)
			where S0 : SessionType
			where S1 : SessionType
			where S2 : SessionType
			where S3 : SessionType
			where T0 : SessionType
			where T1 : SessionType
			where T2 : SessionType
			where T3 : SessionType
		{
			throw new NotImplementedException();
		}

		// Send, Recv ====

		public static Session<S,E> Send<V, S, E>(this Session<Send<V, S>, E> p, V v)
			where S : SessionType
			where E : Protocol
		{
			throw new NotImplementedException();
		}
		// Send() for unit
		public static Session<S, E> Send<S, E>(this Session<Send<Unit, S>, E> p)
			where S : SessionType
			where E : Protocol
		{
			throw new NotImplementedException();
		}

		public static Session<S, E> Receive<V, S, E>(this Session<Recv<V, S>, E> p, out V v)
			where S : SessionType
			where E : Protocol
		{
			throw new NotImplementedException();
		}
		// Recv() for unit
		public static Session<S, E> Receive<V, S, E>(this Session<Recv<V, S>, E> p)
			where S : SessionType
			where E : Protocol
		{
			throw new NotImplementedException();
		}

		// convenient overloading for tuples
		public static Session<S, E> Receive<V1, V2, S, E>(this Session<Recv<(V1,V2), S>, E> p, out V1 v1, out V2 v2)
			where S : SessionType
			where E : Protocol
		{
			throw new NotImplementedException();
		}
		public static Session<S, E> Receive<V1, V2, V3, S, E>(this Session<Recv<(V1,V2,V3), S>, E> p, out V1 v1, out V2 v2, out V3 v3)
			where S : SessionType
			where E : Protocol
		{
			throw new NotImplementedException();
		}

		// ReceiveAsync

		public static Session<S, E> ReceiveAsync<V, S, E>(this Session<Recv<V, S>, E> p, out Task<V> v)
			where S : SessionType
			where E : Protocol
		{
			throw new NotImplementedException();
		}
		// RecvAsync() for unit
		public static Session<S, E> ReceiveAsync<S, E>(this Session<Recv<Unit, S>, E> p, out Task v)
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

		// SelectLeft ====

		public static Session<SL, E> SelectLeft<SL, SR, E>(this Session<Select<SL, SR>, E> session)
			where SL : SessionType
			where SR : SessionType
			where E : Protocol
		{
			throw new NotImplementedException();
		}
		public static Session<SL, E> SelectLeft<SL, SM, SR, E>(this Session<Select<SL, SM, SR>, E> session)
			where SL : SessionType
			where SR : SessionType
			where E : Protocol
		{
			throw new NotImplementedException();
		}

		// SelectRight ====

		public static Session<SR, E> SelectRight<SL, SR, E>(this Session<Select<SL, SR>, E> session)
			where SL : SessionType
			where SR : SessionType
			where E : Protocol
		{
			throw new NotImplementedException();
		}
		public static Session<SR, E> SelectRight<SL, SM, SR, E>(this Session<Select<SL, SM, SR>, E> session)
			where SL : SessionType
			where SR : SessionType
			where E : Protocol
		{
			throw new NotImplementedException();
		}

		// SelectMiddle for ternary Select

		public static Session<SM, E> SelectMiddle<SL, SM, SR, E>(this Session<Select<SL, SM, SR>, E> session)
			where SL : SessionType
			where SM : SessionType
			where SR : SessionType
			where E : Protocol
		{
			throw new NotImplementedException();
		}

		// Quaternary Select ====

		public static Session<S0, E> SelectFirst<S0, S1, S2, S3, E>(this Session<Select<S0, S1, S2, S3>, E> session)
			where S0 : SessionType
			where S1 : SessionType
			where S2 : SessionType
			where S3 : SessionType
			where E : Protocol
		{
			throw new NotImplementedException();
		}
		public static Session<S1, E> SelectSecond<S0, S1, S2, S3, E>(this Session<Select<S0, S1, S2, S3>, E> session)
			where S0 : SessionType
			where S1 : SessionType
			where S2 : SessionType
			where S3 : SessionType
			where E : Protocol
		{
			throw new NotImplementedException();
		}
		public static Session<S2, E> SelectThird<S0, S1, S2, S3, E>(this Session<Select<S0, S1, S2, S3>, E> session)
			where S0 : SessionType
			where S1 : SessionType
			where S2 : SessionType
			where S3 : SessionType
			where E : Protocol
		{
			throw new NotImplementedException();
		}
		public static Session<S3, E> SelectFourth<S0, S1, S2, S3, E>(this Session<Select<S0, S1, S2, S3>, E> session)
			where S0 : SessionType
			where S1 : SessionType
			where S2 : SessionType
			where S3 : SessionType
			where E : Protocol
		{
			throw new NotImplementedException();
		}

		// Offer ====

		public static void Offer<SL, SR, E>(this Session<Offer<SL, SR>, E> session, Action<Session<SL, E>> left, Action<Session<SR, E>> right)
			where SL : SessionType
			where SR : SessionType
			where E : Protocol
		{
			throw new NotImplementedException();
		}
		public static T Offer<SL, SR, E, T>(this Session<Offer<SL, SR>, E> session, Func<Session<SL, E>, T> left, Func<Session<SR, E>, T> right)
			where SL : SessionType
			where SR : SessionType
			where E : Protocol
		{
			throw new NotImplementedException();
		}
		public static void Offer<SL, SM, SR, E>(this Session<Offer<SL, SM, SR>, E> session, Action<Session<SL, E>> left, Action<Session<SM, E>> middle, Action<Session<SR, E>> right)
			where SL : SessionType
			where SM : SessionType
			where SR : SessionType
			where E : Protocol
		{
			throw new NotImplementedException();
		}
		public static void Offer<S0, S1, S2, S3, E>(this Session<Offer<S0, S1, S2, S3>, E> session, Action<Session<S0, E>> first, Action<Session<S1, E>> second, Action<Session<S2, E>> third, Action<Session<S3, E>> fourth)
			where S0 : SessionType
			where S1 : SessionType
			where S2 : SessionType
			where S3 : SessionType
			where E : Protocol
		{
			throw new NotImplementedException();
		}

		// delegation ====

		public static Session<S, E> Deleg<S, S0, T0, E>(this Session<Deleg<S0, T0, S>, E> session, Session<S0, E> deleg)
			where S0 : SessionType
			where S : SessionType
			where E : Protocol
		{
			throw new NotImplementedException();
		}
		public static Session<S, E> DelegNew<S, S0, T0, E>(this Session<Deleg<S0, T0, S>, E> session, out Session<T0, E> deleg)
			where T0 : SessionType
			where S : SessionType
			where E : Protocol
		{
			throw new NotImplementedException();
		}
		public static Session<S, E> DelegRecv<S, S0, E>(this Session<DelegRecv<S0, S>, E> session, out Session<S0, E> deleg)
			where S0 : SessionType
			where S : SessionType
			where E : Protocol
		{
			throw new NotImplementedException();
		}

		// Goto0 ========

		/// Goto0 specialised to a single-cycled (i.e. without Arrange) recursions
		public static Session<S0, S0> Goto0<S0>(this Session<Goto0, S0> p)
			where S0 : SessionType
		{
			throw new NotImplementedException();
		}

		public static Session<S0, E> Goto0<S0, E>(this Session<Goto0, E> p)
			where S0 : SessionType
			where E : EnvHead<S0>
		{
			throw new NotImplementedException();
		}

		// Goto1 (overloaded for tuples) ========

		public static Session<S1, Env<S0, S1>> Goto1<S0, S1>(this Session<Goto1, Env<S0, S1>> p)
			where S0 : SessionType
			where S1 : SessionType
		{
			throw new NotImplementedException();
		}
		public static Session<S1, Env<S0, S1, S2>> Goto1<S0, S1, S2>(this Session<Goto1, Env<S0, S1, S2>> p)
			where S0 : SessionType
			where S1 : SessionType
			where S2 : SessionType
		{
			throw new NotImplementedException();
		}
		public static Session<S1, Env<S0, S1, S2, S3>> Goto1<S0, S1, S2, S3>(this Session<Goto1, Env<S0, S1, S2, S3>> p)
			where S0 : SessionType
			where S1 : SessionType
			where S2 : SessionType
			where S3 : SessionType
		{
			throw new NotImplementedException();
		}

		// Goto2 ========

		public static Session<S2, Env<S0, S1, S2>> Goto2<S0, S1, S2>(this Session<Goto2, Env<S0, S1, S2>> p)
			where S0 : SessionType
			where S1 : SessionType
			where S2 : SessionType
		{
			throw new NotImplementedException();
		}
		public static Session<S2, Env<S0, S1, S2, S3>> Goto2<S0, S1, S2, S3>(this Session<Goto2, Env<S0, S1, S2, S3>> p)
			where S0 : SessionType
			where S1 : SessionType
			where S2 : SessionType
			where S3 : SessionType
		{
			throw new NotImplementedException();
		}

		// Goto3 ========

		public static Session<S2, Env<S0, S1, S2, S3>> Goto3<S0, S1, S2, S3>(this Session<Goto2, Env<S0, S1, S2, S3>> p)
			where S0 : SessionType
			where S1 : SessionType
			where S2 : SessionType
			where S3 : SessionType
		{
			throw new NotImplementedException();
		}
	}
}
