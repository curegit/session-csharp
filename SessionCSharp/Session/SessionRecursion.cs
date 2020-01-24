using System;

namespace Session
{
	public delegate Session<Eps, Any, P> DepletionFunc<S, P>(Session<S, Any, P> session, DepletionFunc<S, P> depletion) where S : SessionType where P : ProtocolType;

	public static class SessionRecursion
	{
		public static Session<S, E, S> Goto<S, E>(this Session<Call0, E, S> session) where S : SessionType where E : SessionStack
		{
			if (session is null) throw new ArgumentNullException(nameof(session));
			session.CallSimply();
			return session.ToNextSession<S>();
		}

		public static Session<S0, E, Cons<S0, L>> Goto<S0, E, L>(this Session<Call0, E, Cons<S0, L>> session) where S0 : SessionType where E : SessionStack where L : SessionList
		{
			if (session is null) throw new ArgumentNullException(nameof(session));
			session.CallSimply();
			return session.ToNextSession<S0>();
		}

		public static Session<S1, E, Cons<S0, Cons<S1, L>>> Goto<S0, S1, E, L>(this Session<Call1, E, Cons<S0, Cons<S1, L>>> session) where S0 : SessionType where S1 : SessionType where E : SessionStack where L : SessionList
		{
			if (session is null) throw new ArgumentNullException(nameof(session));
			session.CallSimply();
			return session.ToNextSession<S1>();
		}

		public static Session<S2, E, Cons<S0, Cons<S1, Cons<S2, L>>>> Goto<S0, S1, S2, E, L>(this Session<Call2, E, Cons<S0, Cons<S1, Cons<S2, L>>>> session) where S0 : SessionType where S1 : SessionType where S2 : SessionType where E : SessionStack where L : SessionList
		{
			if (session is null) throw new ArgumentNullException(nameof(session));
			session.CallSimply();
			return session.ToNextSession<S2>();
		}

		public static Session<S3, E, Cons<S0, Cons<S1, Cons<S2, Cons<S3, L>>>>> Goto<S0, S1, S2, S3, E, L>(this Session<Call3, E, Cons<S0, Cons<S1, Cons<S2, Cons<S3, L>>>>> session) where S0 : SessionType where S1 : SessionType where S2 : SessionType where S3 : SessionType where E : SessionStack where L : SessionList
		{
			if (session is null) throw new ArgumentNullException(nameof(session));
			session.CallSimply();
			return session.ToNextSession<S3>();
		}

		public static Session<S4, E, Cons<S0, Cons<S1, Cons<S2, Cons<S3, Cons<S4, L>>>>>> Goto<S0, S1, S2, S3, S4, E, L>(this Session<Call4, E, Cons<S0, Cons<S1, Cons<S2, Cons<S3, Cons<S4, L>>>>>> session) where S0 : SessionType where S1 : SessionType where S2 : SessionType where S3 : SessionType where S4 : SessionType where E : SessionStack where L : SessionList
		{
			if (session is null) throw new ArgumentNullException(nameof(session));
			session.CallSimply();
			return session.ToNextSession<S4>();
		}

		public static Session<S5, E, Cons<S0, Cons<S1, Cons<S2, Cons<S3, Cons<S4, Cons<S5, L>>>>>>> Goto<S0, S1, S2, S3, S4, S5, E, L>(this Session<Call5, E, Cons<S0, Cons<S1, Cons<S2, Cons<S3, Cons<S4, Cons<S5, L>>>>>>> session) where S0 : SessionType where S1 : SessionType where S2 : SessionType where S3 : SessionType where S4 : SessionType where S5 : SessionType where E : SessionStack where L : SessionList
		{
			if (session is null) throw new ArgumentNullException(nameof(session));
			session.CallSimply();
			return session.ToNextSession<S5>();
		}

		public static Session<S6, E, Cons<S0, Cons<S1, Cons<S2, Cons<S3, Cons<S4, Cons<S5, Cons<S6, L>>>>>>>> Goto<S0, S1, S2, S3, S4, S5, S6, E, L>(this Session<Call6, E, Cons<S0, Cons<S1, Cons<S2, Cons<S3, Cons<S4, Cons<S5, Cons<S6, L>>>>>>>> session) where S0 : SessionType where S1 : SessionType where S2 : SessionType where S3 : SessionType where S4 : SessionType where S5 : SessionType where S6 : SessionType where E : SessionStack where L : SessionList
		{
			if (session is null) throw new ArgumentNullException(nameof(session));
			session.CallSimply();
			return session.ToNextSession<S6>();
		}

		public static Session<S7, E, Cons<S0, Cons<S1, Cons<S2, Cons<S3, Cons<S4, Cons<S5, Cons<S6, Cons<S7, L>>>>>>>>> Goto<S0, S1, S2, S3, S4, S5, S6, S7, E, L>(this Session<Call7, E, Cons<S0, Cons<S1, Cons<S2, Cons<S3, Cons<S4, Cons<S5, Cons<S6, Cons<S7, L>>>>>>>>> session) where S0 : SessionType where S1 : SessionType where S2 : SessionType where S3 : SessionType where S4 : SessionType where S5 : SessionType where S6 : SessionType where S7 : SessionType where E : SessionStack where L : SessionList
		{
			if (session is null) throw new ArgumentNullException(nameof(session));
			session.CallSimply();
			return session.ToNextSession<S7>();
		}

		public static Session<S8, E, Cons<S0, Cons<S1, Cons<S2, Cons<S3, Cons<S4, Cons<S5, Cons<S6, Cons<S7, Cons<S8, L>>>>>>>>>> Goto<S0, S1, S2, S3, S4, S5, S6, S7, S8, E, L>(this Session<Call8, E, Cons<S0, Cons<S1, Cons<S2, Cons<S3, Cons<S4, Cons<S5, Cons<S6, Cons<S7, Cons<S8, L>>>>>>>>>> session) where S0 : SessionType where S1 : SessionType where S2 : SessionType where S3 : SessionType where S4 : SessionType where S5 : SessionType where S6 : SessionType where S7 : SessionType where S8 : SessionType where E : SessionStack where L : SessionList
		{
			if (session is null) throw new ArgumentNullException(nameof(session));
			session.CallSimply();
			return session.ToNextSession<S8>();
		}

		public static Session<S9, E, Cons<S0, Cons<S1, Cons<S2, Cons<S3, Cons<S4, Cons<S5, Cons<S6, Cons<S7, Cons<S8, Cons<S9, L>>>>>>>>>>> Goto<S0, S1, S2, S3, S4, S5, S6, S7, S8, S9, E, L>(this Session<Call9, E, Cons<S0, Cons<S1, Cons<S2, Cons<S3, Cons<S4, Cons<S5, Cons<S6, Cons<S7, Cons<S8, Cons<S9, L>>>>>>>>>>> session) where S0 : SessionType where S1 : SessionType where S2 : SessionType where S3 : SessionType where S4 : SessionType where S5 : SessionType where S6 : SessionType where S7 : SessionType where S8 : SessionType where S9 : SessionType where E : SessionStack where L : SessionList
		{
			if (session is null) throw new ArgumentNullException(nameof(session));
			session.CallSimply();
			return session.ToNextSession<S9>();
		}

		public static Session<S, Push<Z, E>, S> Call<S, Z, E, L>(this Session<Call0<Z>, E, S> session) where S : SessionType where Z : SessionType where E : SessionStack
		{
			if (session is null) throw new ArgumentNullException(nameof(session));
			session.CallSimply();
			return session.ToNextSession<S, Push<Z, E>>();
		}

		public static Session<S0, Push<Z, E>, Cons<S0, L>> Call<S0, Z, E, L>(this Session<Call0<Z>, E, Cons<S0, L>> session) where S0 : SessionType where Z : SessionType where E : SessionStack where L : SessionList
		{
			if (session is null) throw new ArgumentNullException(nameof(session));
			session.CallSimply();
			return session.ToNextSession<S0, Push<Z, E>>();
		}

		public static Session<S1, Push<Z, E>, Cons<S0, Cons<S1, L>>> Call<S0, S1, Z, E, L>(this Session<Call1<Z>, E, Cons<S0, Cons<S1, L>>> session) where S0 : SessionType where S1 : SessionType where Z : SessionType where E : SessionStack where L : SessionList
		{
			if (session is null) throw new ArgumentNullException(nameof(session));
			session.CallSimply();
			return session.ToNextSession<S1, Push<Z, E>>();
		}

		public static Session<S2, Push<Z, E>, Cons<S0, Cons<S1, Cons<S2, L>>>> Call<S0, S1, S2, Z, E, L>(this Session<Call2<Z>, E, Cons<S0, Cons<S1, Cons<S2, L>>>> session) where S0 : SessionType where S1 : SessionType where S2 : SessionType where Z : SessionType where E : SessionStack where L : SessionList
		{
			if (session is null) throw new ArgumentNullException(nameof(session));
			session.CallSimply();
			return session.ToNextSession<S2, Push<Z, E>>();
		}

		public static Session<S3, Push<Z, E>, Cons<S0, Cons<S1, Cons<S2, Cons<S3, L>>>>> Call<S0, S1, S2, S3, Z, E, L>(this Session<Call3<Z>, E, Cons<S0, Cons<S1, Cons<S2, Cons<S3, L>>>>> session) where S0 : SessionType where S1 : SessionType where S2 : SessionType where S3 : SessionType where Z : SessionType where E : SessionStack where L : SessionList
		{
			if (session is null) throw new ArgumentNullException(nameof(session));
			session.CallSimply();
			return session.ToNextSession<S3, Push<Z, E>>();
		}

		public static Session<S4, Push<Z, E>, Cons<S0, Cons<S1, Cons<S2, Cons<S3, Cons<S4, L>>>>>> Call<S0, S1, S2, S3, S4, Z, E, L>(this Session<Call4<Z>, E, Cons<S0, Cons<S1, Cons<S2, Cons<S3, Cons<S4, L>>>>>> session) where S0 : SessionType where S1 : SessionType where S2 : SessionType where S3 : SessionType where S4 : SessionType where Z : SessionType where E : SessionStack where L : SessionList
		{
			if (session is null) throw new ArgumentNullException(nameof(session));
			session.CallSimply();
			return session.ToNextSession<S4, Push<Z, E>>();
		}

		public static Session<S5, Push<Z, E>, Cons<S0, Cons<S1, Cons<S2, Cons<S3, Cons<S4, Cons<S5, L>>>>>>> Call<S0, S1, S2, S3, S4, S5, Z, E, L>(this Session<Call5<Z>, E, Cons<S0, Cons<S1, Cons<S2, Cons<S3, Cons<S4, Cons<S5, L>>>>>>> session) where S0 : SessionType where S1 : SessionType where S2 : SessionType where S3 : SessionType where S4 : SessionType where S5 : SessionType where Z : SessionType where E : SessionStack where L : SessionList
		{
			if (session is null) throw new ArgumentNullException(nameof(session));
			session.CallSimply();
			return session.ToNextSession<S5, Push<Z, E>>();
		}

		public static Session<S6, Push<Z, E>, Cons<S0, Cons<S1, Cons<S2, Cons<S3, Cons<S4, Cons<S5, Cons<S6, L>>>>>>>> Call<S0, S1, S2, S3, S4, S5, S6, Z, E, L>(this Session<Call6<Z>, E, Cons<S0, Cons<S1, Cons<S2, Cons<S3, Cons<S4, Cons<S5, Cons<S6, L>>>>>>>> session) where S0 : SessionType where S1 : SessionType where S2 : SessionType where S3 : SessionType where S4 : SessionType where S5 : SessionType where S6 : SessionType where Z : SessionType where E : SessionStack where L : SessionList
		{
			if (session is null) throw new ArgumentNullException(nameof(session));
			session.CallSimply();
			return session.ToNextSession<S6, Push<Z, E>>();
		}

		public static Session<S7, Push<Z, E>, Cons<S0, Cons<S1, Cons<S2, Cons<S3, Cons<S4, Cons<S5, Cons<S6, Cons<S7, L>>>>>>>>> Call<S0, S1, S2, S3, S4, S5, S6, S7, Z, E, L>(this Session<Call7<Z>, E, Cons<S0, Cons<S1, Cons<S2, Cons<S3, Cons<S4, Cons<S5, Cons<S6, Cons<S7, L>>>>>>>>> session) where S0 : SessionType where S1 : SessionType where S2 : SessionType where S3 : SessionType where S4 : SessionType where S5 : SessionType where S6 : SessionType where S7 : SessionType where Z : SessionType where E : SessionStack where L : SessionList
		{
			if (session is null) throw new ArgumentNullException(nameof(session));
			session.CallSimply();
			return session.ToNextSession<S7, Push<Z, E>>();
		}

		public static Session<S8, Push<Z, E>, Cons<S0, Cons<S1, Cons<S2, Cons<S3, Cons<S4, Cons<S5, Cons<S6, Cons<S7, Cons<S8, L>>>>>>>>>> Call<S0, S1, S2, S3, S4, S5, S6, S7, S8, Z, E, L>(this Session<Call8<Z>, E, Cons<S0, Cons<S1, Cons<S2, Cons<S3, Cons<S4, Cons<S5, Cons<S6, Cons<S7, Cons<S8, L>>>>>>>>>> session) where S0 : SessionType where S1 : SessionType where S2 : SessionType where S3 : SessionType where S4 : SessionType where S5 : SessionType where S6 : SessionType where S7 : SessionType where S8 : SessionType where Z : SessionType where E : SessionStack where L : SessionList
		{
			if (session is null) throw new ArgumentNullException(nameof(session));
			session.CallSimply();
			return session.ToNextSession<S8, Push<Z, E>>();
		}

		public static Session<S9, Push<Z, E>, Cons<S0, Cons<S1, Cons<S2, Cons<S3, Cons<S4, Cons<S5, Cons<S6, Cons<S7, Cons<S8, Cons<S9, L>>>>>>>>>>> Call<S0, S1, S2, S3, S4, S5, S6, S7, S8, S9, Z, E, L>(this Session<Call9<Z>, E, Cons<S0, Cons<S1, Cons<S2, Cons<S3, Cons<S4, Cons<S5, Cons<S6, Cons<S7, Cons<S8, Cons<S9, L>>>>>>>>>>> session) where S0 : SessionType where S1 : SessionType where S2 : SessionType where S3 : SessionType where S4 : SessionType where S5 : SessionType where S6 : SessionType where S7 : SessionType where S8 : SessionType where S9 : SessionType where Z : SessionType where E : SessionStack where L : SessionList
		{
			if (session is null) throw new ArgumentNullException(nameof(session));
			session.CallSimply();
			return session.ToNextSession<S9, Push<Z, E>>();
		}

		public static Session<Z, E, S> Call<S, Z, E, L>(this Session<Call0<Z>, E, S> session, DepletionFunc<S, S> depletion) where S : SessionType where Z : SessionType where E : SessionStack where L : SessionList
		{
			if (session is null) throw new ArgumentNullException(nameof(session));
			if (depletion is null) throw new ArgumentNullException(nameof(depletion));
			session.CallSimply();
			var depleted = depletion(session.ToNextSession<S, Any>(), depletion);
			depleted.CallSimply();
			return depleted.ToNextSession<Z, E>();
		}

		public static Session<Z, E, Cons<S0, L>> Call<S0, Z, E, L>(this Session<Call0<Z>, E, Cons<S0, L>> session, DepletionFunc<S0, Cons<S0, L>> depletion) where S0 : SessionType where Z : SessionType where E : SessionStack where L : SessionList
		{
			if (session is null) throw new ArgumentNullException(nameof(session));
			if (depletion is null) throw new ArgumentNullException(nameof(depletion));
			session.CallSimply();
			var depleted = depletion(session.ToNextSession<S0, Any>(), depletion);
			depleted.CallSimply();
			return depleted.ToNextSession<Z, E>();
		}

		public static Session<Z, E, Cons<S0, Cons<S1, L>>> Call<S0, S1, Z, E, L>(this Session<Call1<Z>, E, Cons<S0, Cons<S1, L>>> session, DepletionFunc<S1, Cons<S0, Cons<S1, L>>> depletion) where S0 : SessionType where S1 : SessionType where Z : SessionType where E : SessionStack where L : SessionList
		{
			if (session is null) throw new ArgumentNullException(nameof(session));
			if (depletion is null) throw new ArgumentNullException(nameof(depletion));
			session.CallSimply();
			var depleted = depletion(session.ToNextSession<S1, Any>(), depletion);
			depleted.CallSimply();
			return depleted.ToNextSession<Z, E>();
		}

		public static Session<Z, E, Cons<S0, Cons<S1, Cons<S2, L>>>> Call<S0, S1, S2, Z, E, L>(this Session<Call2<Z>, E, Cons<S0, Cons<S1, Cons<S2, L>>>> session, DepletionFunc<S2, Cons<S0, Cons<S1, Cons<S2, L>>>> depletion) where S0 : SessionType where S1 : SessionType where S2 : SessionType where Z : SessionType where E : SessionStack where L : SessionList
		{
			if (session is null) throw new ArgumentNullException(nameof(session));
			if (depletion is null) throw new ArgumentNullException(nameof(depletion));
			session.CallSimply();
			var depleted = depletion(session.ToNextSession<S2, Any>(), depletion);
			depleted.CallSimply();
			return depleted.ToNextSession<Z, E>();
		}

		public static Session<Z, E, Cons<S0, Cons<S1, Cons<S2, Cons<S3, L>>>>> Call<S0, S1, S2, S3, Z, E, L>(this Session<Call3<Z>, E, Cons<S0, Cons<S1, Cons<S2, Cons<S3, L>>>>> session, DepletionFunc<S3, Cons<S0, Cons<S1, Cons<S2, Cons<S3, L>>>>> depletion) where S0 : SessionType where S1 : SessionType where S2 : SessionType where S3 : SessionType where Z : SessionType where E : SessionStack where L : SessionList
		{
			if (session is null) throw new ArgumentNullException(nameof(session));
			if (depletion is null) throw new ArgumentNullException(nameof(depletion));
			session.CallSimply();
			var depleted = depletion(session.ToNextSession<S3, Any>(), depletion);
			depleted.CallSimply();
			return depleted.ToNextSession<Z, E>();
		}

		public static Session<Z, E, Cons<S0, Cons<S1, Cons<S2, Cons<S3, Cons<S4, L>>>>>> Call<S0, S1, S2, S3, S4, Z, E, L>(this Session<Call4<Z>, E, Cons<S0, Cons<S1, Cons<S2, Cons<S3, Cons<S4, L>>>>>> session, DepletionFunc<S4, Cons<S0, Cons<S1, Cons<S2, Cons<S3, Cons<S4, L>>>>>> depletion) where S0 : SessionType where S1 : SessionType where S2 : SessionType where S3 : SessionType where S4 : SessionType where Z : SessionType where E : SessionStack where L : SessionList
		{
			if (session is null) throw new ArgumentNullException(nameof(session));
			if (depletion is null) throw new ArgumentNullException(nameof(depletion));
			session.CallSimply();
			var depleted = depletion(session.ToNextSession<S4, Any>(), depletion);
			depleted.CallSimply();
			return depleted.ToNextSession<Z, E>();
		}

		public static Session<Z, E, Cons<S0, Cons<S1, Cons<S2, Cons<S3, Cons<S4, Cons<S5, L>>>>>>> Call<S0, S1, S2, S3, S4, S5, Z, E, L>(this Session<Call5<Z>, E, Cons<S0, Cons<S1, Cons<S2, Cons<S3, Cons<S4, Cons<S5, L>>>>>>> session, DepletionFunc<S5, Cons<S0, Cons<S1, Cons<S2, Cons<S3, Cons<S4, Cons<S5, L>>>>>>> depletion) where S0 : SessionType where S1 : SessionType where S2 : SessionType where S3 : SessionType where S4 : SessionType where S5 : SessionType where Z : SessionType where E : SessionStack where L : SessionList
		{
			if (session is null) throw new ArgumentNullException(nameof(session));
			if (depletion is null) throw new ArgumentNullException(nameof(depletion));
			session.CallSimply();
			var depleted = depletion(session.ToNextSession<S5, Any>(), depletion);
			depleted.CallSimply();
			return depleted.ToNextSession<Z, E>();
		}

		public static Session<Z, E, Cons<S0, Cons<S1, Cons<S2, Cons<S3, Cons<S4, Cons<S5, Cons<S6, L>>>>>>>> Call<S0, S1, S2, S3, S4, S5, S6, Z, E, L>(this Session<Call6<Z>, E, Cons<S0, Cons<S1, Cons<S2, Cons<S3, Cons<S4, Cons<S5, Cons<S6, L>>>>>>>> session, DepletionFunc<S6, Cons<S0, Cons<S1, Cons<S2, Cons<S3, Cons<S4, Cons<S5, Cons<S6, L>>>>>>>> depletion) where S0 : SessionType where S1 : SessionType where S2 : SessionType where S3 : SessionType where S4 : SessionType where S5 : SessionType where S6 : SessionType where Z : SessionType where E : SessionStack where L : SessionList
		{
			if (session is null) throw new ArgumentNullException(nameof(session));
			if (depletion is null) throw new ArgumentNullException(nameof(depletion));
			session.CallSimply();
			var depleted = depletion(session.ToNextSession<S6, Any>(), depletion);
			depleted.CallSimply();
			return depleted.ToNextSession<Z, E>();
		}

		public static Session<Z, E, Cons<S0, Cons<S1, Cons<S2, Cons<S3, Cons<S4, Cons<S5, Cons<S6, Cons<S7, L>>>>>>>>> Call<S0, S1, S2, S3, S4, S5, S6, S7, Z, E, L>(this Session<Call7<Z>, E, Cons<S0, Cons<S1, Cons<S2, Cons<S3, Cons<S4, Cons<S5, Cons<S6, Cons<S7, L>>>>>>>>> session, DepletionFunc<S7, Cons<S0, Cons<S1, Cons<S2, Cons<S3, Cons<S4, Cons<S5, Cons<S6, Cons<S7, L>>>>>>>>> depletion) where S0 : SessionType where S1 : SessionType where S2 : SessionType where S3 : SessionType where S4 : SessionType where S5 : SessionType where S6 : SessionType where S7 : SessionType where Z : SessionType where E : SessionStack where L : SessionList
		{
			if (session is null) throw new ArgumentNullException(nameof(session));
			if (depletion is null) throw new ArgumentNullException(nameof(depletion));
			session.CallSimply();
			var depleted = depletion(session.ToNextSession<S7, Any>(), depletion);
			depleted.CallSimply();
			return depleted.ToNextSession<Z, E>();
		}

		public static Session<Z, E, Cons<S0, Cons<S1, Cons<S2, Cons<S3, Cons<S4, Cons<S5, Cons<S6, Cons<S7, Cons<S8, L>>>>>>>>>> Call<S0, S1, S2, S3, S4, S5, S6, S7, S8, Z, E, L>(this Session<Call8<Z>, E, Cons<S0, Cons<S1, Cons<S2, Cons<S3, Cons<S4, Cons<S5, Cons<S6, Cons<S7, Cons<S8, L>>>>>>>>>> session, DepletionFunc<S8, Cons<S0, Cons<S1, Cons<S2, Cons<S3, Cons<S4, Cons<S5, Cons<S6, Cons<S7, Cons<S8, L>>>>>>>>>> depletion) where S0 : SessionType where S1 : SessionType where S2 : SessionType where S3 : SessionType where S4 : SessionType where S5 : SessionType where S6 : SessionType where S7 : SessionType where S8 : SessionType where Z : SessionType where E : SessionStack where L : SessionList
		{
			if (session is null) throw new ArgumentNullException(nameof(session));
			if (depletion is null) throw new ArgumentNullException(nameof(depletion));
			session.CallSimply();
			var depleted = depletion(session.ToNextSession<S8, Any>(), depletion);
			depleted.CallSimply();
			return depleted.ToNextSession<Z, E>();
		}

		public static Session<Z, E, Cons<S0, Cons<S1, Cons<S2, Cons<S3, Cons<S4, Cons<S5, Cons<S6, Cons<S7, Cons<S8, Cons<S9, L>>>>>>>>>>> Call<S0, S1, S2, S3, S4, S5, S6, S7, S8, S9, Z, E, L>(this Session<Call9<Z>, E, Cons<S0, Cons<S1, Cons<S2, Cons<S3, Cons<S4, Cons<S5, Cons<S6, Cons<S7, Cons<S8, Cons<S9, L>>>>>>>>>>> session, DepletionFunc<S9, Cons<S0, Cons<S1, Cons<S2, Cons<S3, Cons<S4, Cons<S5, Cons<S6, Cons<S7, Cons<S8, Cons<S9, L>>>>>>>>>>> depletion) where S0 : SessionType where S1 : SessionType where S2 : SessionType where S3 : SessionType where S4 : SessionType where S5 : SessionType where S6 : SessionType where S7 : SessionType where S8 : SessionType where S9 : SessionType where Z : SessionType where E : SessionStack where L : SessionList
		{
			if (session is null) throw new ArgumentNullException(nameof(session));
			if (depletion is null) throw new ArgumentNullException(nameof(depletion));
			session.CallSimply();
			var depleted = depletion(session.ToNextSession<S9, Any>(), depletion);
			depleted.CallSimply();
			return depleted.ToNextSession<Z, E>();
		}

		public static Session<F, E, P> Return<F, E, P>(this Session<Eps, Push<F, E>, P> session) where F : SessionType where E : SessionStack where P : ProtocolType
		{
			if (session is null) throw new ArgumentNullException(nameof(session));
			return session.ToNextSession<F, E>();
		}
	}
}
