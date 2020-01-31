using System;
using System.Collections.Generic;
using System.Text;

namespace Session
{
	using Session.Types;

	public static class ProtocolCombinators
	{
		public static Val<V> Val<V>() => new Val<V>();

		public static Dual<Eps, Eps> Eps = new Dual<Eps, Eps>();
		public static Dual<Goto0, Goto0> Goto0 = new Dual<Goto0, Goto0>();
		public static Dual<Goto1, Goto1> Goto1 = new Dual<Goto1, Goto1>();
		public static Dual<Goto2, Goto2> Goto2 = new Dual<Goto2, Goto2>();
		public static Dual<Goto3, Goto3> Goto3 = new Dual<Goto3, Goto3>();
		public static Dual<Send<V, S>, Recv<V, T>> Send<V, S, T>(Func<Val<V>> v, Dual<S, T> cont)
			where S : SessionType
			where T : SessionType
		{ throw new NotImplementedException(); }
		public static Dual<Send<V, S>, Recv<V, T>> Recv<V, S, T>(Func<Val<V>> v, Dual<S, T> cont)
			where S : SessionType
			where T : SessionType
		{ throw new NotImplementedException(); }
		public static Dual<Select<SL, SR>, Offer<TL, TR>> Select<SL, SR, TL, TR>(Dual<SL, TL> contL, Dual<SR, TR> contR)
			where SL : SessionType
			where SR : SessionType
			where TL : SessionType
			where TR : SessionType
		{ throw new NotImplementedException(); }
		public static Dual<Offer<SL, SR>, Select<TL, TR>> Offer<SL, SR, TL, TR>(Dual<SL, TL> contL, Dual<SR, TR> contR)
			where SL : SessionType
			where SR : SessionType
			where TL : SessionType
			where TR : SessionType
		{ throw new NotImplementedException(); }
		public static Dual<Deleg<S0, T0, S>, DelegRecv<S0, T>> Deleg<S0, T0, S, T>(Dual<S0, T0> deleg, Dual<S, T> cont)
			where S0 : SessionType
			where T0 : SessionType
			where S : SessionType
			where T : SessionType
		{ throw new NotImplementedException(); }
		public static Dual<DelegRecv<S0, S>, DelegRecv<S0, T>> DelegRecv<S0, T0, S, T>(Dual<S0, T0> deleg, Dual<S, T> cont)
			where S0 : SessionType
			where T0 : SessionType
			where S : SessionType
			where T : SessionType
		{ throw new NotImplementedException(); }
		public static Dual<SessionList<S0, S1>, SessionList<T0, T1>> Arrange<S0, S1, T0, T1>(Dual<S0, T0> p0, Dual<S1, T1> p1)
			where S0 : SessionType
			where S1 : SessionType
			where T0 : SessionType
			where T1 : SessionType
		{ throw new NotImplementedException(); }
		public static Dual<SessionList<S0, S1, S2>, SessionList<T0, T1, T2>> Arrange<S0, S1, S2, T0, T1, T2>(Dual<S0, T0> p0, Dual<S1, T1> p1, Dual<S2, T2> p2)
			where S0 : SessionType
			where S1 : SessionType
			where S2 : SessionType
			where T0 : SessionType
			where T1 : SessionType
			where T2 : SessionType
		{ throw new NotImplementedException(); }
		public static Dual<SessionList<S0, S1, S2, S3>, SessionList<T0, T1, T2, T3>> Arrange<S0, S1, S2, S3, T0, T1, T2, T3>(Dual<S0, T0> p0, Dual<S1, T1> p1, Dual<S2, T2> p2, Dual<S3, T3> p3)
			where S0 : SessionType
			where S1 : SessionType
			where S2 : SessionType
			where S3 : SessionType
			where T0 : SessionType
			where T1 : SessionType
			where T2 : SessionType
			where T3 : SessionType
		{ throw new NotImplementedException(); }

	}

}
