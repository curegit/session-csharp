using System;

namespace Session
{
	public sealed class UnitPayload
	{
		internal UnitPayload() { }
	}

	public sealed class Payload<T>
	{
		internal Payload() { }
	}

	/*
	public sealed class PayloadSeries<T1, T2>
	{
		internal PayloadSeries() { }
	}

	public sealed class PayloadSeries<T1, T2, T3>
	{
		internal PayloadSeries() { }
	}

	public sealed class PayloadSeries<T1, T2, T3, T4>
	{
		internal PayloadSeries() { }
	}

	public sealed class PayloadSeries<T1, T2, T3, T4, T5>
	{
		internal PayloadSeries() { }
	}*/

	public delegate Payload<T> PayloadDelegate<T>();

	/*
	public delegate PayloadSeries<T1, T2> PayloadSeriesDelegate<T1, T2>();

	public delegate PayloadSeries<T1, T2, T3> PayloadSeriesDelegate<T1, T2, T3>();

	public delegate PayloadSeries<T1, T2, T3, T4> PayloadSeriesDelegate<T1, T2, T3, T4>();

	public delegate PayloadSeries<T1, T2, T3, T4, T5> PayloadSeriesDelegate<T1, T2, T3, T4, T5>();*/

	public static class ProtocolCombinator
	{
		public static UnitPayload Unit => new UnitPayload();

		public static Payload<T> Val<T>() => new Payload<T>();

		public static Payload<T> Value<T>() => new Payload<T>();

		/*
		public static PayloadSeries<T1, T2> ValueSeries<T1, T2>() => new PayloadSeries<T1, T2>();

		public static PayloadSeries<T1, T2, T3> ValueSeries<T1, T2, T3>() => new PayloadSeries<T1, T2, T3>();

		public static PayloadSeries<T1, T2, T3, T4> ValueSeries<T1, T2, T3, T4>() => new PayloadSeries<T1, T2, T3, T4>();

		public static PayloadSeries<T1, T2, T3, T4, T5> ValueSeries<T1, T2, T3, T4, T5>() => new PayloadSeries<T1, T2, T3, T4, T5>();*/

		public static Payload<(T1, T2)> Tuple<T1, T2>() => new Payload<(T1, T2)>();

		public static Payload<(T1, T2, T3)> Tuple<T1, T2, T3>() => new Payload<(T1, T2, T3)>();

		public static Payload<(T1, T2, T3, T4)> Tuple<T1, T2, T3, T4>() => new Payload<(T1, T2, T3, T4)>();

		public static Payload<(T1, T2, T3, T4, T5)> Tuple<T1, T2, T3, T4, T5>() => new Payload<(T1, T2, T3, T4, T5)>();

		public static Dual<Send<S>, Receive<Z>> Send<S, Z>(UnitPayload unit, Dual<S, Z> continuation) where S : Session where Z : Session
		{
			if (unit is null) throw new ArgumentNullException(nameof(unit));
			if (continuation is null) throw new ArgumentNullException(nameof(continuation));
			return new Dual<Send<S>, Receive<Z>>();
		}

		public static Dual<Send<T, S>, Receive<T, Z>> Send<T, S, Z>(PayloadDelegate<T> payload, Dual<S, Z> continuation) where S : Session where Z : Session
		{
			if (payload is null) throw new ArgumentNullException(nameof(payload));
			if (payload() is null) throw new ArgumentException("Return value cannot be null.", nameof(payload));
			if (continuation is null) throw new ArgumentNullException(nameof(continuation));
			return new Dual<Send<T, S>, Receive<T, Z>>();
		}

		/*
		public static Protocol<Send<T1, Send<T2, S>>, Receive<T1, Receive<T2, Z>>> Send<T1, T2, S, Z>(PayloadSeriesDelegate<T1, T2> payloads, Protocol<S, Z> continuation) where S : SessionType where Z : SessionType
		{
			if (payloads is null) throw new ArgumentNullException(nameof(payloads));
			if (payloads() is null) throw new ArgumentException("Return value cannot be null.", nameof(payloads));
			if (continuation is null) throw new ArgumentNullException(nameof(continuation));
			return new Protocol<Send<T1, Send<T2, S>>, Receive<T1, Receive<T2, Z>>>();
		}

		public static Protocol<Send<T1, Send<T2, Send<T3, S>>>, Receive<T1, Receive<T2, Receive<T3, Z>>>> Send<T1, T2, T3, S, Z>(PayloadSeriesDelegate<T1, T2, T3> payloads, Protocol<S, Z> continuation) where S : SessionType where Z : SessionType
		{
			if (payloads is null) throw new ArgumentNullException(nameof(payloads));
			if (payloads() is null) throw new ArgumentException("Return value cannot be null.", nameof(payloads));
			if (continuation is null) throw new ArgumentNullException(nameof(continuation));
			return new Protocol<Send<T1, Send<T2, Send<T3, S>>>, Receive<T1, Receive<T2, Receive<T3, Z>>>>();
		}

		public static Protocol<Send<T1, Send<T2, Send<T3, Send<T4, S>>>>, Receive<T1, Receive<T2, Receive<T3, Receive<T4, Z>>>>> Send<T1, T2, T3, T4, S, Z>(PayloadSeriesDelegate<T1, T2, T3, T4> payloads, Protocol<S, Z> continuation) where S : SessionType where Z : SessionType
		{
			if (payloads is null) throw new ArgumentNullException(nameof(payloads));
			if (payloads() is null) throw new ArgumentException("Return value cannot be null.", nameof(payloads));
			if (continuation is null) throw new ArgumentNullException(nameof(continuation));
			return new Protocol<Send<T1, Send<T2, Send<T3, Send<T4, S>>>>, Receive<T1, Receive<T2, Receive<T3, Receive<T4, Z>>>>>();
		}

		public static Protocol<Send<T1, Send<T2, Send<T3, Send<T4, Send<T5, S>>>>>, Receive<T1, Receive<T2, Receive<T3, Receive<T4, Receive<T5, Z>>>>>> Send<T1, T2, T3, T4, T5, S, Z>(PayloadSeriesDelegate<T1, T2, T3, T4, T5> payloads, Protocol<S, Z> continuation) where S : SessionType where Z : SessionType
		{
			if (payloads is null) throw new ArgumentNullException(nameof(payloads));
			if (payloads() is null) throw new ArgumentException("Return value cannot be null.", nameof(payloads));
			if (continuation is null) throw new ArgumentNullException(nameof(continuation));
			return new Protocol<Send<T1, Send<T2, Send<T3, Send<T4, Send<T5, S>>>>>, Receive<T1, Receive<T2, Receive<T3, Receive<T4, Receive<T5, Z>>>>>>();
		}*/

		public static Dual<Receive<S>, Send<Z>> Receive<S, Z>(UnitPayload unit, Dual<S, Z> continuation) where S : Session where Z : Session
		{
			if (unit is null) throw new ArgumentNullException(nameof(unit));
			if (continuation is null) throw new ArgumentNullException(nameof(continuation));
			return new Dual<Receive<S>, Send<Z>>();
		}

		public static Dual<Receive<T, S>, Send<T, Z>> Receive<T, S, Z>(PayloadDelegate<T> payload, Dual<S, Z> continuation) where S : Session where Z : Session
		{
			if (payload is null) throw new ArgumentNullException(nameof(payload));
			if (payload() is null) throw new ArgumentException("Return value cannot be null.", nameof(payload));
			if (continuation is null) throw new ArgumentNullException(nameof(continuation));
			return new Dual<Receive<T, S>, Send<T, Z>>();
		}

		/*
		public static Protocol<Receive<T1, Receive<T2, S>>, Send<T1, Send<T2, Z>>> Receive<T1, T2, S, Z>(PayloadSeriesDelegate<T1, T2> payloads, Protocol<S, Z> continuation) where S : SessionType where Z : SessionType
		{
			if (payloads is null) throw new ArgumentNullException(nameof(payloads));
			if (payloads() is null) throw new ArgumentException("Return value cannot be null.", nameof(payloads));
			if (continuation is null) throw new ArgumentNullException(nameof(continuation));
			return new Protocol<Receive<T1, Receive<T2, S>>, Send<T1, Send<T2, Z>>>();
		}

		public static Protocol<Receive<T1, Receive<T2, Receive<T3, S>>>, Send<T1, Send<T2, Send<T3, Z>>>> Receive<T1, T2, T3, S, Z>(PayloadSeriesDelegate<T1, T2, T3> payloads, Protocol<S, Z> continuation) where S : SessionType where Z : SessionType
		{
			if (payloads is null) throw new ArgumentNullException(nameof(payloads));
			if (payloads() is null) throw new ArgumentException("Return value cannot be null.", nameof(payloads));
			if (continuation is null) throw new ArgumentNullException(nameof(continuation));
			return new Protocol<Receive<T1, Receive<T2, Receive<T3, S>>>, Send<T1, Send<T2, Send<T3, Z>>>>();
		}

		public static Protocol<Receive<T1, Receive<T2, Receive<T3, Receive<T4, S>>>>, Send<T1, Send<T2, Send<T3, Send<T4, Z>>>>> Receive<T1, T2, T3, T4, S, Z>(PayloadSeriesDelegate<T1, T2, T3, T4> payloads, Protocol<S, Z> continuation) where S : SessionType where Z : SessionType
		{
			if (payloads is null) throw new ArgumentNullException(nameof(payloads));
			if (payloads() is null) throw new ArgumentException("Return value cannot be null.", nameof(payloads));
			if (continuation is null) throw new ArgumentNullException(nameof(continuation));
			return new Protocol<Receive<T1, Receive<T2, Receive<T3, Receive<T4, S>>>>, Send<T1, Send<T2, Send<T3, Send<T4, Z>>>>>();
		}

		public static Protocol<Receive<T1, Receive<T2, Receive<T3, Receive<T4, Receive<T5, S>>>>>, Send<T1, Send<T2, Send<T3, Send<T4, Send<T5, Z>>>>>> Receive<T1, T2, T3, T4, T5, S, Z>(PayloadSeriesDelegate<T1, T2, T3, T4, T5> payloads, Protocol<S, Z> continuation) where S : SessionType where Z : SessionType
		{
			if (payloads is null) throw new ArgumentNullException(nameof(payloads));
			if (payloads() is null) throw new ArgumentException("Return value cannot be null.", nameof(payloads));
			if (continuation is null) throw new ArgumentNullException(nameof(continuation));
			return new Protocol<Receive<T1, Receive<T2, Receive<T3, Receive<T4, Receive<T5, S>>>>>, Send<T1, Send<T2, Send<T3, Send<T4, Send<T5, Z>>>>>>();
		}*/

		public static Dual<Select<L, R>, Offer<X, Y>> Select<L, R, X, Y>(Dual<L, X> left, Dual<R, Y> right) where L : Session where R : Session where X : Session where Y : Session
		{
			if (left is null) throw new ArgumentNullException(nameof(left));
			if (right is null) throw new ArgumentNullException(nameof(right));
			return new Dual<Select<L, R>, Offer<X, Y>>();
		}

		public static Dual<Select<L, C, R>, Offer<X, W, Y>> Select<L, C, R, X, W, Y>(Dual<L, X> left, Dual<C, W> center, Dual<R, Y> right) where L : Session where C : Session where R : Session where X : Session where W : Session where Y : Session
		{
			if (left is null) throw new ArgumentNullException(nameof(left));
			if (center is null) throw new ArgumentNullException(nameof(center));
			if (right is null) throw new ArgumentNullException(nameof(right));
			return new Dual<Select<L, C, R>, Offer<X, W, Y>>();
		}

		public static Dual<Offer<L, R>, Select<X, Y>> Follow<L, R, X, Y>(Dual<L, X> left, Dual<R, Y> right) where L : Session where R : Session where X : Session where Y : Session
		{
			if (left is null) throw new ArgumentNullException(nameof(left));
			if (right is null) throw new ArgumentNullException(nameof(right));
			return new Dual<Offer<L, R>, Select<X, Y>>();
		}

		public static Dual<Offer<L, C, R>, Select<X, W, Y>> Follow<L, C, R, X, W, Y>(Dual<L, X> left, Dual<C, W> center, Dual<R, Y> right) where L : Session where C : Session where R : Session where X : Session where W : Session where Y : Session
		{
			if (left is null) throw new ArgumentNullException(nameof(left));
			if (center is null) throw new ArgumentNullException(nameof(center));
			if (right is null) throw new ArgumentNullException(nameof(right));
			return new Dual<Offer<L, C, R>, Select<X, W, Y>>();
		}

		/*
		public static Dual<Call0, Call0> Goto0 => new Dual<Call0, Call0>();

		public static Dual<Call1, Call1> Goto1 => new Dual<Call1, Call1>();

		public static Dual<Call2, Call2> Goto2 => new Dual<Call2, Call2>();

		public static Dual<Call3, Call3> Goto3 => new Dual<Call3, Call3>();

		public static Dual<Call4, Call4> Goto4 => new Dual<Call4, Call4>();

		public static Dual<Call5, Call5> Goto5 => new Dual<Call5, Call5>();

		public static Dual<Call6, Call6> Goto6 => new Dual<Call6, Call6>();

		public static Dual<Call7, Call7> Goto7 => new Dual<Call7, Call7>();

		public static Dual<Call8, Call8> Goto8 => new Dual<Call8, Call8>();

		public static Dual<Call9, Call9> Goto9 => new Dual<Call9, Call9>();

		public static Dual<Call0<S>, Call0<Z>> Call0<S, Z>(Dual<S, Z> continuation) where S : SessionType where Z : SessionType
		{
			if (continuation is null) throw new ArgumentNullException(nameof(continuation));
			return new Dual<Call0<S>, Call0<Z>>();
		}

		public static Dual<Call1<S>, Call1<Z>> Call1<S, Z>(Dual<S, Z> continuation) where S : SessionType where Z : SessionType
		{
			if (continuation is null) throw new ArgumentNullException(nameof(continuation));
			return new Dual<Call1<S>, Call1<Z>>();
		}

		public static Dual<Call2<S>, Call2<Z>> Call2<S, Z>(Dual<S, Z> continuation) where S : SessionType where Z : SessionType
		{
			if (continuation is null) throw new ArgumentNullException(nameof(continuation));
			return new Dual<Call2<S>, Call2<Z>>();
		}

		public static Dual<Call3<S>, Call3<Z>> Call3<S, Z>(Dual<S, Z> continuation) where S : SessionType where Z : SessionType
		{
			if (continuation is null) throw new ArgumentNullException(nameof(continuation));
			return new Dual<Call3<S>, Call3<Z>>();
		}

		public static Dual<Call4<S>, Call4<Z>> Call4<S, Z>(Dual<S, Z> continuation) where S : SessionType where Z : SessionType
		{
			if (continuation is null) throw new ArgumentNullException(nameof(continuation));
			return new Dual<Call4<S>, Call4<Z>>();
		}

		public static Dual<Call5<S>, Call5<Z>> Call5<S, Z>(Dual<S, Z> continuation) where S : SessionType where Z : SessionType
		{
			if (continuation is null) throw new ArgumentNullException(nameof(continuation));
			return new Dual<Call5<S>, Call5<Z>>();
		}

		public static Dual<Call6<S>, Call6<Z>> Call6<S, Z>(Dual<S, Z> continuation) where S : SessionType where Z : SessionType
		{
			if (continuation is null) throw new ArgumentNullException(nameof(continuation));
			return new Dual<Call6<S>, Call6<Z>>();
		}

		public static Dual<Call7<S>, Call7<Z>> Call7<S, Z>(Dual<S, Z> continuation) where S : SessionType where Z : SessionType
		{
			if (continuation is null) throw new ArgumentNullException(nameof(continuation));
			return new Dual<Call7<S>, Call7<Z>>();
		}

		public static Dual<Call8<S>, Call8<Z>> Call8<S, Z>(Dual<S, Z> continuation) where S : SessionType where Z : SessionType
		{
			if (continuation is null) throw new ArgumentNullException(nameof(continuation));
			return new Dual<Call8<S>, Call8<Z>>();
		}

		public static Dual<Call9<S>, Call9<Z>> Call9<S, Z>(Dual<S, Z> continuation) where S : SessionType where Z : SessionType
		{
			if (continuation is null) throw new ArgumentNullException(nameof(continuation));
			return new Dual<Call9<S>, Call9<Z>>();
		}*/

		public static Dual<DelegSend<P, Q, S>, DelegRecv<P, Z>> DelegSend<P, Q, S, Z>(Dual<P, Q> delegated, Dual<S, Z> continuation)
			where P : Session
			where Q : Session
			where S : Session
			where Z : Session
		{
			if (delegated is null) throw new ArgumentNullException(nameof(delegated));
			if (continuation is null) throw new ArgumentNullException(nameof(continuation));
			return new Dual<DelegSend<P, Q, S>, DelegRecv<P, Z>>();
		}

		public static Dual<DelegRecv<P, S>, DelegSend<P, Q, Z>> DelegRecv<P, Q, S, Z>(Dual<P, Q> delegated, Dual<S, Z> continuation)
			where P : Session
			where Q : Session
			where S : Session
			where Z : Session
		{
			if (delegated is null) throw new ArgumentNullException(nameof(delegated));
			if (continuation is null) throw new ArgumentNullException(nameof(continuation));
			return new Dual<DelegRecv<P, S>, DelegSend<P, Q, Z>>();
		}

		/*
		public static Dual<ThrowNewChannel<X, Cons<X,Nil>, S>, CatchNewChannel<Y, Cons<Y,Nil>, Z>> ThrowNewChannel<X, Y, Z, S>(Dual<X, Y> protocol, Dual<S, Z> continuation) where X : SessionType where Y : SessionType where S : SessionType where Z : SessionType
		{
			if (protocol is null) throw new ArgumentNullException(nameof(protocol));
			if (continuation is null) throw new ArgumentNullException(nameof(continuation));
			return new Dual<ThrowNewChannel<X, Cons<X,Nil>, S>, CatchNewChannel<Y, Cons<Y,Nil>, Z>>();
		}

		public static Dual<ThrowNewChannel<X0, Cons<X0,XS>, S>, CatchNewChannel<Y0, Cons<Y0,YS>, Z>> ThrowNewChannel<X0, XS, Y0, YS, Z, S>(Dual<Cons<X0,XS>, Cons<Y0,YS>> protocol, Dual<S, Z> continuation) where X0 : SessionType where XS : SessionList where Y0 : SessionType where YS : SessionList where S : SessionType where Z : SessionType
		{
			if (protocol is null) throw new ArgumentNullException(nameof(protocol));
			if (continuation is null) throw new ArgumentNullException(nameof(continuation));
			return new Dual<ThrowNewChannel<X0, Cons<X0, XS>, S>, CatchNewChannel<Y0, Cons<Y0, YS>, Z>>();
		}

		public static Dual<CatchNewChannel<X, Cons<X,Nil>, S>, ThrowNewChannel<Y, Cons<Y,Nil>, Z>> CatchNewChannel<X, Y, Z, S>(Dual<X, Y> protocol, Dual<S, Z> continuation) where X : SessionType where Y : SessionType where S : SessionType where Z : SessionType
		{
			if (protocol is null) throw new ArgumentNullException(nameof(protocol));
			if (continuation is null) throw new ArgumentNullException(nameof(continuation));
			return new Dual<CatchNewChannel<X, Cons<X,Nil>, S>, ThrowNewChannel<Y, Cons<Y,Nil>, Z>>();
		}
		public static Dual<CatchNewChannel<X0, Cons<X0, XS>, S>, ThrowNewChannel<Y0, Cons<Y0, YS>, Z>> CatchNewChannel<X0, XS, Y0, YS, Z, S>(Dual<Cons<X0, XS>, Cons<Y0, YS>> protocol, Dual<S, Z> continuation) where X0 : SessionType where XS : SessionList where Y0 : SessionType where YS : SessionList where S : SessionType where Z : SessionType
		{
			if (protocol is null) throw new ArgumentNullException(nameof(protocol));
			if (continuation is null) throw new ArgumentNullException(nameof(continuation));
			return new Dual<CatchNewChannel<X0, Cons<X0, XS>, S>, ThrowNewChannel<Y0, Cons<Y0, YS>, Z>>();
		}
		*/

		public static Dual<S, T> Recur<S0, T0, S, T>(Func<Dual<S0, T0>> dual, Func<S0, S> f, Func<T0, T> g) where S : S0 where S0 : Session where T : T0 where T0 : Session
		{
			return new Dual<S, T>();
		}

		public static Dual<Eps, Eps> End => new Dual<Eps, Eps>();

		/*
		public static Dual<Cons<S0, Nil>, Cons<Z0, Nil>> Arrange<S0, Z0>(Dual<S0, Z0> sub0) where S0 : SessionType where Z0 : SessionType
		{
			if (sub0 is null) throw new ArgumentNullException(nameof(sub0));
			return new Dual<Cons<S0, Nil>, Cons<Z0, Nil>>();
		}

		public static Dual<Cons<S0, Cons<S1, Nil>>, Cons<Z0, Cons<Z1, Nil>>> Arrange<S0, S1, Z0, Z1>(Dual<S0, Z0> sub0, Dual<S1, Z1> sub1) where S0 : SessionType where S1 : SessionType where Z0 : SessionType where Z1 : SessionType
		{
			if (sub0 is null) throw new ArgumentNullException(nameof(sub0));
			if (sub1 is null) throw new ArgumentNullException(nameof(sub1));
			return new Dual<Cons<S0, Cons<S1, Nil>>, Cons<Z0, Cons<Z1, Nil>>>();
		}

		public static Dual<Cons<S0, Cons<S1, Cons<S2, Nil>>>, Cons<Z0, Cons<Z1, Cons<Z2, Nil>>>> Arrange<S0, S1, S2, Z0, Z1, Z2>(Dual<S0, Z0> sub0, Dual<S1, Z1> sub1, Dual<S2, Z2> sub2) where S0 : SessionType where S1 : SessionType where S2 : SessionType where Z0 : SessionType where Z1 : SessionType where Z2 : SessionType
		{
			if (sub0 is null) throw new ArgumentNullException(nameof(sub0));
			if (sub1 is null) throw new ArgumentNullException(nameof(sub1));
			if (sub2 is null) throw new ArgumentNullException(nameof(sub2));
			return new Dual<Cons<S0, Cons<S1, Cons<S2, Nil>>>, Cons<Z0, Cons<Z1, Cons<Z2, Nil>>>>();
		}

		public static Dual<Cons<S0, Cons<S1, Cons<S2, Cons<S3, Nil>>>>, Cons<Z0, Cons<Z1, Cons<Z2, Cons<Z3, Nil>>>>> Arrange<S0, S1, S2, S3, Z0, Z1, Z2, Z3>(Dual<S0, Z0> sub0, Dual<S1, Z1> sub1, Dual<S2, Z2> sub2, Dual<S3, Z3> sub3) where S0 : SessionType where S1 : SessionType where S2 : SessionType where Z0 : SessionType where Z1 : SessionType where Z2 : SessionType where S3 : SessionType where Z3 : SessionType
		{
			if (sub0 is null) throw new ArgumentNullException(nameof(sub0));
			if (sub1 is null) throw new ArgumentNullException(nameof(sub1));
			if (sub2 is null) throw new ArgumentNullException(nameof(sub2));
			if (sub3 is null) throw new ArgumentNullException(nameof(sub3));
			return new Dual<Cons<S0, Cons<S1, Cons<S2, Cons<S3, Nil>>>>, Cons<Z0, Cons<Z1, Cons<Z2, Cons<Z3, Nil>>>>>();
		}

		public static Dual<Cons<S0, Cons<S1, Cons<S2, Cons<S3, Cons<S4, Nil>>>>>, Cons<Z0, Cons<Z1, Cons<Z2, Cons<Z3, Cons<Z4, Nil>>>>>> Arrange<S0, S1, S2, S3, S4, Z0, Z1, Z2, Z3, Z4>(Dual<S0, Z0> sub0, Dual<S1, Z1> sub1, Dual<S2, Z2> sub2, Dual<S3, Z3> sub3, Dual<S4, Z4> sub4) where S0 : SessionType where S1 : SessionType where S2 : SessionType where Z0 : SessionType where Z1 : SessionType where Z2 : SessionType where S3 : SessionType where Z3 : SessionType where S4 : SessionType where Z4 : SessionType
		{
			if (sub0 is null) throw new ArgumentNullException(nameof(sub0));
			if (sub1 is null) throw new ArgumentNullException(nameof(sub1));
			if (sub2 is null) throw new ArgumentNullException(nameof(sub2));
			if (sub3 is null) throw new ArgumentNullException(nameof(sub3));
			if (sub4 is null) throw new ArgumentNullException(nameof(sub4));
			return new Dual<Cons<S0, Cons<S1, Cons<S2, Cons<S3, Cons<S4, Nil>>>>>, Cons<Z0, Cons<Z1, Cons<Z2, Cons<Z3, Cons<Z4, Nil>>>>>>();
		}

		public static Dual<Cons<S0, Cons<S1, Cons<S2, Cons<S3, Cons<S4, Cons<S5, Nil>>>>>>, Cons<Z0, Cons<Z1, Cons<Z2, Cons<Z3, Cons<Z4, Cons<Z5, Nil>>>>>>> Arrange<S0, S1, S2, S3, S4, S5, Z0, Z1, Z2, Z3, Z4, Z5>(Dual<S0, Z0> sub0, Dual<S1, Z1> sub1, Dual<S2, Z2> sub2, Dual<S3, Z3> sub3, Dual<S4, Z4> sub4, Dual<S5, Z5> sub5) where S0 : SessionType where S1 : SessionType where S2 : SessionType where Z0 : SessionType where Z1 : SessionType where Z2 : SessionType where S3 : SessionType where Z3 : SessionType where S4 : SessionType where Z4 : SessionType where S5 : SessionType where Z5 : SessionType
		{
			if (sub0 is null) throw new ArgumentNullException(nameof(sub0));
			if (sub1 is null) throw new ArgumentNullException(nameof(sub1));
			if (sub2 is null) throw new ArgumentNullException(nameof(sub2));
			if (sub3 is null) throw new ArgumentNullException(nameof(sub3));
			if (sub4 is null) throw new ArgumentNullException(nameof(sub4));
			if (sub5 is null) throw new ArgumentNullException(nameof(sub5));
			return new Dual<Cons<S0, Cons<S1, Cons<S2, Cons<S3, Cons<S4, Cons<S5, Nil>>>>>>, Cons<Z0, Cons<Z1, Cons<Z2, Cons<Z3, Cons<Z4, Cons<Z5, Nil>>>>>>>();
		}

		public static Dual<Cons<S0, Cons<S1, Cons<S2, Cons<S3, Cons<S4, Cons<S5, Cons<S6, Nil>>>>>>>, Cons<Z0, Cons<Z1, Cons<Z2, Cons<Z3, Cons<Z4, Cons<Z5, Cons<Z6, Nil>>>>>>>> Arrange<S0, S1, S2, S3, S4, S5, S6, Z0, Z1, Z2, Z3, Z4, Z5, Z6>(Dual<S0, Z0> sub0, Dual<S1, Z1> sub1, Dual<S2, Z2> sub2, Dual<S3, Z3> sub3, Dual<S4, Z4> sub4, Dual<S5, Z5> sub5, Dual<S6, Z6> sub6) where S0 : SessionType where S1 : SessionType where S2 : SessionType where Z0 : SessionType where Z1 : SessionType where Z2 : SessionType where S3 : SessionType where Z3 : SessionType where S4 : SessionType where Z4 : SessionType where S5 : SessionType where Z5 : SessionType where S6 : SessionType where Z6 : SessionType
		{
			if (sub0 is null) throw new ArgumentNullException(nameof(sub0));
			if (sub1 is null) throw new ArgumentNullException(nameof(sub1));
			if (sub2 is null) throw new ArgumentNullException(nameof(sub2));
			if (sub3 is null) throw new ArgumentNullException(nameof(sub3));
			if (sub4 is null) throw new ArgumentNullException(nameof(sub4));
			if (sub5 is null) throw new ArgumentNullException(nameof(sub5));
			if (sub6 is null) throw new ArgumentNullException(nameof(sub6));
			return new Dual<Cons<S0, Cons<S1, Cons<S2, Cons<S3, Cons<S4, Cons<S5, Cons<S6, Nil>>>>>>>, Cons<Z0, Cons<Z1, Cons<Z2, Cons<Z3, Cons<Z4, Cons<Z5, Cons<Z6, Nil>>>>>>>>();
		}

		public static Dual<Cons<S0, Cons<S1, Cons<S2, Cons<S3, Cons<S4, Cons<S5, Cons<S6, Cons<S7, Nil>>>>>>>>, Cons<Z0, Cons<Z1, Cons<Z2, Cons<Z3, Cons<Z4, Cons<Z5, Cons<Z6, Cons<Z7, Nil>>>>>>>>> Arrange<S0, S1, S2, S3, S4, S5, S6, S7, Z0, Z1, Z2, Z3, Z4, Z5, Z6, Z7>(Dual<S0, Z0> sub0, Dual<S1, Z1> sub1, Dual<S2, Z2> sub2, Dual<S3, Z3> sub3, Dual<S4, Z4> sub4, Dual<S5, Z5> sub5, Dual<S6, Z6> sub6, Dual<S7, Z7> sub7) where S0 : SessionType where S1 : SessionType where S2 : SessionType where Z0 : SessionType where Z1 : SessionType where Z2 : SessionType where S3 : SessionType where Z3 : SessionType where S4 : SessionType where Z4 : SessionType where S5 : SessionType where Z5 : SessionType where S6 : SessionType where Z6 : SessionType where S7 : SessionType where Z7 : SessionType
		{
			if (sub0 is null) throw new ArgumentNullException(nameof(sub0));
			if (sub1 is null) throw new ArgumentNullException(nameof(sub1));
			if (sub2 is null) throw new ArgumentNullException(nameof(sub2));
			if (sub3 is null) throw new ArgumentNullException(nameof(sub3));
			if (sub4 is null) throw new ArgumentNullException(nameof(sub4));
			if (sub5 is null) throw new ArgumentNullException(nameof(sub5));
			if (sub6 is null) throw new ArgumentNullException(nameof(sub6));
			if (sub7 is null) throw new ArgumentNullException(nameof(sub7));
			return new Dual<Cons<S0, Cons<S1, Cons<S2, Cons<S3, Cons<S4, Cons<S5, Cons<S6, Cons<S7, Nil>>>>>>>>, Cons<Z0, Cons<Z1, Cons<Z2, Cons<Z3, Cons<Z4, Cons<Z5, Cons<Z6, Cons<Z7, Nil>>>>>>>>>();
		}

		public static Dual<Cons<S0, Cons<S1, Cons<S2, Cons<S3, Cons<S4, Cons<S5, Cons<S6, Cons<S7, Cons<S8, Nil>>>>>>>>>, Cons<Z0, Cons<Z1, Cons<Z2, Cons<Z3, Cons<Z4, Cons<Z5, Cons<Z6, Cons<Z7, Cons<Z8, Nil>>>>>>>>>> Arrange<S0, S1, S2, S3, S4, S5, S6, S7, S8, Z0, Z1, Z2, Z3, Z4, Z5, Z6, Z7, Z8>(Dual<S0, Z0> sub0, Dual<S1, Z1> sub1, Dual<S2, Z2> sub2, Dual<S3, Z3> sub3, Dual<S4, Z4> sub4, Dual<S5, Z5> sub5, Dual<S6, Z6> sub6, Dual<S7, Z7> sub7, Dual<S8, Z8> sub8) where S0 : SessionType where S1 : SessionType where S2 : SessionType where Z0 : SessionType where Z1 : SessionType where Z2 : SessionType where S3 : SessionType where Z3 : SessionType where S4 : SessionType where Z4 : SessionType where S5 : SessionType where Z5 : SessionType where S6 : SessionType where Z6 : SessionType where S7 : SessionType where Z7 : SessionType where S8 : SessionType where Z8 : SessionType
		{
			if (sub0 is null) throw new ArgumentNullException(nameof(sub0));
			if (sub1 is null) throw new ArgumentNullException(nameof(sub1));
			if (sub2 is null) throw new ArgumentNullException(nameof(sub2));
			if (sub3 is null) throw new ArgumentNullException(nameof(sub3));
			if (sub4 is null) throw new ArgumentNullException(nameof(sub4));
			if (sub5 is null) throw new ArgumentNullException(nameof(sub5));
			if (sub6 is null) throw new ArgumentNullException(nameof(sub6));
			if (sub7 is null) throw new ArgumentNullException(nameof(sub7));
			if (sub8 is null) throw new ArgumentNullException(nameof(sub8));
			return new Dual<Cons<S0, Cons<S1, Cons<S2, Cons<S3, Cons<S4, Cons<S5, Cons<S6, Cons<S7, Cons<S8, Nil>>>>>>>>>, Cons<Z0, Cons<Z1, Cons<Z2, Cons<Z3, Cons<Z4, Cons<Z5, Cons<Z6, Cons<Z7, Cons<Z8, Nil>>>>>>>>>>();
		}

		public static Dual<Cons<S0, Cons<S1, Cons<S2, Cons<S3, Cons<S4, Cons<S5, Cons<S6, Cons<S7, Cons<S8, Cons<S9, Nil>>>>>>>>>>, Cons<Z0, Cons<Z1, Cons<Z2, Cons<Z3, Cons<Z4, Cons<Z5, Cons<Z6, Cons<Z7, Cons<Z8, Cons<Z9, Nil>>>>>>>>>>> Arrange<S0, S1, S2, S3, S4, S5, S6, S7, S8, S9, Z0, Z1, Z2, Z3, Z4, Z5, Z6, Z7, Z8, Z9>(Dual<S0, Z0> sub0, Dual<S1, Z1> sub1, Dual<S2, Z2> sub2, Dual<S3, Z3> sub3, Dual<S4, Z4> sub4, Dual<S5, Z5> sub5, Dual<S6, Z6> sub6, Dual<S7, Z7> sub7, Dual<S8, Z8> sub8, Dual<S9, Z9> sub9) where S0 : SessionType where S1 : SessionType where S2 : SessionType where Z0 : SessionType where Z1 : SessionType where Z2 : SessionType where S3 : SessionType where Z3 : SessionType where S4 : SessionType where Z4 : SessionType where S5 : SessionType where Z5 : SessionType where S6 : SessionType where Z6 : SessionType where S7 : SessionType where Z7 : SessionType where S8 : SessionType where Z8 : SessionType where S9 : SessionType where Z9 : SessionType
		{
			if (sub0 is null) throw new ArgumentNullException(nameof(sub0));
			if (sub1 is null) throw new ArgumentNullException(nameof(sub1));
			if (sub2 is null) throw new ArgumentNullException(nameof(sub2));
			if (sub3 is null) throw new ArgumentNullException(nameof(sub3));
			if (sub4 is null) throw new ArgumentNullException(nameof(sub4));
			if (sub5 is null) throw new ArgumentNullException(nameof(sub5));
			if (sub6 is null) throw new ArgumentNullException(nameof(sub6));
			if (sub7 is null) throw new ArgumentNullException(nameof(sub7));
			if (sub8 is null) throw new ArgumentNullException(nameof(sub8));
			if (sub9 is null) throw new ArgumentNullException(nameof(sub9));
			return new Dual<Cons<S0, Cons<S1, Cons<S2, Cons<S3, Cons<S4, Cons<S5, Cons<S6, Cons<S7, Cons<S8, Cons<S9, Nil>>>>>>>>>>, Cons<Z0, Cons<Z1, Cons<Z2, Cons<Z3, Cons<Z4, Cons<Z5, Cons<Z6, Cons<Z7, Cons<Z8, Cons<Z9, Nil>>>>>>>>>>>();
		}
		*/
	}
}
