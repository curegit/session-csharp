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
	}

	public delegate Payload<T> PayloadDelegate<T>();

	public delegate PayloadSeries<T1, T2> PayloadSeriesDelegate<T1, T2>();

	public delegate PayloadSeries<T1, T2, T3> PayloadSeriesDelegate<T1, T2, T3>();

	public delegate PayloadSeries<T1, T2, T3, T4> PayloadSeriesDelegate<T1, T2, T3, T4>();

	public delegate PayloadSeries<T1, T2, T3, T4, T5> PayloadSeriesDelegate<T1, T2, T3, T4, T5>();

	public static class ProtocolCombinator
	{
		public static UnitPayload Unit => new UnitPayload();

		public static Payload<T> Val<T>() => new Payload<T>();

		public static Payload<T> Value<T>() => new Payload<T>();

		public static PayloadSeries<T1, T2> ValueSeries<T1, T2>() => new PayloadSeries<T1, T2>();

		public static PayloadSeries<T1, T2, T3> ValueSeries<T1, T2, T3>() => new PayloadSeries<T1, T2, T3>();

		public static PayloadSeries<T1, T2, T3, T4> ValueSeries<T1, T2, T3, T4>() => new PayloadSeries<T1, T2, T3, T4>();

		public static PayloadSeries<T1, T2, T3, T4, T5> ValueSeries<T1, T2, T3, T4, T5>() => new PayloadSeries<T1, T2, T3, T4, T5>();

		public static Payload<(T1, T2)> Tuple<T1, T2>() => new Payload<(T1, T2)>();

		public static Payload<(T1, T2, T3)> Tuple<T1, T2, T3>() => new Payload<(T1, T2, T3)>();

		public static Payload<(T1, T2, T3, T4)> Tuple<T1, T2, T3, T4>() => new Payload<(T1, T2, T3, T4)>();

		public static Payload<(T1, T2, T3, T4, T5)> Tuple<T1, T2, T3, T4, T5>() => new Payload<(T1, T2, T3, T4, T5)>();

		public static Protocol<Send<S>, Receive<Z>> Send<S, Z>(UnitPayload unit, Protocol<S, Z> continuation) where S : SessionType where Z : SessionType
		{
			if (unit is null) throw new ArgumentNullException(nameof(unit));
			if (continuation is null) throw new ArgumentNullException(nameof(continuation));
			return new Protocol<Send<S>, Receive<Z>>();
		}

		public static Protocol<Send<T, S>, Receive<T, Z>> Send<T, S, Z>(PayloadDelegate<T> payload, Protocol<S, Z> continuation) where S : SessionType where Z : SessionType
		{
			if (payload is null) throw new ArgumentNullException(nameof(payload));
			if (payload() is null) throw new ArgumentException("Return value cannot be null.", nameof(payload));
			if (continuation is null) throw new ArgumentNullException(nameof(continuation));
			return new Protocol<Send<T, S>, Receive<T, Z>>();
		}

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
		}

		public static Protocol<Receive<S>, Send<Z>> Receive<S, Z>(UnitPayload unit, Protocol<S, Z> continuation) where S : SessionType where Z : SessionType
		{
			if (unit is null) throw new ArgumentNullException(nameof(unit));
			if (continuation is null) throw new ArgumentNullException(nameof(continuation));
			return new Protocol<Receive<S>, Send<Z>>();
		}

		public static Protocol<Receive<T, S>, Send<T, Z>> Receive<T, S, Z>(PayloadDelegate<T> payload, Protocol<S, Z> continuation) where S : SessionType where Z : SessionType
		{
			if (payload is null) throw new ArgumentNullException(nameof(payload));
			if (payload() is null) throw new ArgumentException("Return value cannot be null.", nameof(payload));
			if (continuation is null) throw new ArgumentNullException(nameof(continuation));
			return new Protocol<Receive<T, S>, Send<T, Z>>();
		}

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
		}

		public static Protocol<Select<L, R>, Offer<X, Y>> Select<L, R, X, Y>(Protocol<L, X> left, Protocol<R, Y> right) where L : SessionType where R : SessionType where X : SessionType where Y : SessionType
		{
			if (left is null) throw new ArgumentNullException(nameof(left));
			if (right is null) throw new ArgumentNullException(nameof(right));
			return new Protocol<Select<L, R>, Offer<X, Y>>();
		}

		public static Protocol<Select<L, C, R>, Offer<X, W, Y>> Select<L, C, R, X, W, Y>(Protocol<L, X> left, Protocol<C, W> center, Protocol<R, Y> right) where L : SessionType where C : SessionType where R : SessionType where X : SessionType where W : SessionType where Y : SessionType
		{
			if (left is null) throw new ArgumentNullException(nameof(left));
			if (center is null) throw new ArgumentNullException(nameof(center));
			if (right is null) throw new ArgumentNullException(nameof(right));
			return new Protocol<Select<L, C, R>, Offer<X, W, Y>>();
		}

		public static Protocol<Offer<L, R>, Select<X, Y>> Follow<L, R, X, Y>(Protocol<L, X> left, Protocol<R, Y> right) where L : SessionType where R : SessionType where X : SessionType where Y : SessionType
		{
			if (left is null) throw new ArgumentNullException(nameof(left));
			if (right is null) throw new ArgumentNullException(nameof(right));
			return new Protocol<Offer<L, R>, Select<X, Y>>();
		}

		public static Protocol<Offer<L, C, R>, Select<X, W, Y>> Follow<L, C, R, X, W, Y>(Protocol<L, X> left, Protocol<C, W> center, Protocol<R, Y> right) where L : SessionType where C : SessionType where R : SessionType where X : SessionType where W : SessionType where Y : SessionType
		{
			if (left is null) throw new ArgumentNullException(nameof(left));
			if (center is null) throw new ArgumentNullException(nameof(center));
			if (right is null) throw new ArgumentNullException(nameof(right));
			return new Protocol<Offer<L, C, R>, Select<X, W, Y>>();
		}

		public static Protocol<Call0, Call0> Goto0 => new Protocol<Call0, Call0>();

		public static Protocol<Call1, Call1> Goto1 => new Protocol<Call1, Call1>();

		public static Protocol<Call2, Call2> Goto2 => new Protocol<Call2, Call2>();

		public static Protocol<Call3, Call3> Goto3 => new Protocol<Call3, Call3>();

		public static Protocol<Call4, Call4> Goto4 => new Protocol<Call4, Call4>();

		public static Protocol<Call5, Call5> Goto5 => new Protocol<Call5, Call5>();

		public static Protocol<Call6, Call6> Goto6 => new Protocol<Call6, Call6>();

		public static Protocol<Call7, Call7> Goto7 => new Protocol<Call7, Call7>();

		public static Protocol<Call8, Call8> Goto8 => new Protocol<Call8, Call8>();

		public static Protocol<Call9, Call9> Goto9 => new Protocol<Call9, Call9>();

		public static Protocol<Call0<S>, Call0<Z>> Call0<S, Z>(Protocol<S, Z> continuation) where S : SessionType where Z : SessionType
		{
			if (continuation is null) throw new ArgumentNullException(nameof(continuation));
			return new Protocol<Call0<S>, Call0<Z>>();
		}

		public static Protocol<Call1<S>, Call1<Z>> Call1<S, Z>(Protocol<S, Z> continuation) where S : SessionType where Z : SessionType
		{
			if (continuation is null) throw new ArgumentNullException(nameof(continuation));
			return new Protocol<Call1<S>, Call1<Z>>();
		}

		public static Protocol<Call2<S>, Call2<Z>> Call2<S, Z>(Protocol<S, Z> continuation) where S : SessionType where Z : SessionType
		{
			if (continuation is null) throw new ArgumentNullException(nameof(continuation));
			return new Protocol<Call2<S>, Call2<Z>>();
		}

		public static Protocol<Call3<S>, Call3<Z>> Call3<S, Z>(Protocol<S, Z> continuation) where S : SessionType where Z : SessionType
		{
			if (continuation is null) throw new ArgumentNullException(nameof(continuation));
			return new Protocol<Call3<S>, Call3<Z>>();
		}

		public static Protocol<Call4<S>, Call4<Z>> Call4<S, Z>(Protocol<S, Z> continuation) where S : SessionType where Z : SessionType
		{
			if (continuation is null) throw new ArgumentNullException(nameof(continuation));
			return new Protocol<Call4<S>, Call4<Z>>();
		}

		public static Protocol<Call5<S>, Call5<Z>> Call5<S, Z>(Protocol<S, Z> continuation) where S : SessionType where Z : SessionType
		{
			if (continuation is null) throw new ArgumentNullException(nameof(continuation));
			return new Protocol<Call5<S>, Call5<Z>>();
		}

		public static Protocol<Call6<S>, Call6<Z>> Call6<S, Z>(Protocol<S, Z> continuation) where S : SessionType where Z : SessionType
		{
			if (continuation is null) throw new ArgumentNullException(nameof(continuation));
			return new Protocol<Call6<S>, Call6<Z>>();
		}

		public static Protocol<Call7<S>, Call7<Z>> Call7<S, Z>(Protocol<S, Z> continuation) where S : SessionType where Z : SessionType
		{
			if (continuation is null) throw new ArgumentNullException(nameof(continuation));
			return new Protocol<Call7<S>, Call7<Z>>();
		}

		public static Protocol<Call8<S>, Call8<Z>> Call8<S, Z>(Protocol<S, Z> continuation) where S : SessionType where Z : SessionType
		{
			if (continuation is null) throw new ArgumentNullException(nameof(continuation));
			return new Protocol<Call8<S>, Call8<Z>>();
		}

		public static Protocol<Call9<S>, Call9<Z>> Call9<S, Z>(Protocol<S, Z> continuation) where S : SessionType where Z : SessionType
		{
			if (continuation is null) throw new ArgumentNullException(nameof(continuation));
			return new Protocol<Call9<S>, Call9<Z>>();
		}

		public static Protocol<DelegSend<P, Q, S>, DelegRecv<P, Z>> DelegSend<P, Q, S, Z>(Protocol<P, Q> delegated, Protocol<S, Z> continuation)
			where P : ProtocolType
			where Q : ProtocolType
			where S : SessionType
			where Z : SessionType
		{
			if (delegated is null) throw new ArgumentNullException(nameof(delegated));
			if (continuation is null) throw new ArgumentNullException(nameof(continuation));
			return new Protocol<DelegSend<P, Q, S>, DelegRecv<P, Z>>();
		}
		public static Protocol<DelegRecv<P, S>, DelegSend<P, Q, Z>> DelegRecv<P, Q, S, Z>(Protocol<P, Q> delegated, Protocol<S, Z> continuation)
			where P : ProtocolType
			where Q : ProtocolType
			where S : SessionType
			where Z : SessionType
		{
			if (delegated is null) throw new ArgumentNullException(nameof(delegated));
			if (continuation is null) throw new ArgumentNullException(nameof(continuation));
			return new Protocol<DelegRecv<P, S>, DelegSend<P, Q, Z>>();
		}

		public static Protocol<ThrowNewChannel<X, Cons<X,Nil>, S>, CatchNewChannel<Y, Cons<Y,Nil>, Z>> ThrowNewChannel<X, Y, Z, S>(Protocol<X, Y> protocol, Protocol<S, Z> continuation) where X : SessionType where Y : SessionType where S : SessionType where Z : SessionType
		{
			if (protocol is null) throw new ArgumentNullException(nameof(protocol));
			if (continuation is null) throw new ArgumentNullException(nameof(continuation));
			return new Protocol<ThrowNewChannel<X, Cons<X,Nil>, S>, CatchNewChannel<Y, Cons<Y,Nil>, Z>>();
		}

		public static Protocol<ThrowNewChannel<X0, Cons<X0,XS>, S>, CatchNewChannel<Y0, Cons<Y0,YS>, Z>> ThrowNewChannel<X0, XS, Y0, YS, Z, S>(Protocol<Cons<X0,XS>, Cons<Y0,YS>> protocol, Protocol<S, Z> continuation) where X0 : SessionType where XS : SessionList where Y0 : SessionType where YS : SessionList where S : SessionType where Z : SessionType
		{
			if (protocol is null) throw new ArgumentNullException(nameof(protocol));
			if (continuation is null) throw new ArgumentNullException(nameof(continuation));
			return new Protocol<ThrowNewChannel<X0, Cons<X0, XS>, S>, CatchNewChannel<Y0, Cons<Y0, YS>, Z>>();
		}

		public static Protocol<CatchNewChannel<X, Cons<X,Nil>, S>, ThrowNewChannel<Y, Cons<Y,Nil>, Z>> CatchNewChannel<X, Y, Z, S>(Protocol<X, Y> protocol, Protocol<S, Z> continuation) where X : SessionType where Y : SessionType where S : SessionType where Z : SessionType
		{
			if (protocol is null) throw new ArgumentNullException(nameof(protocol));
			if (continuation is null) throw new ArgumentNullException(nameof(continuation));
			return new Protocol<CatchNewChannel<X, Cons<X,Nil>, S>, ThrowNewChannel<Y, Cons<Y,Nil>, Z>>();
		}
		public static Protocol<CatchNewChannel<X0, Cons<X0, XS>, S>, ThrowNewChannel<Y0, Cons<Y0, YS>, Z>> CatchNewChannel<X0, XS, Y0, YS, Z, S>(Protocol<Cons<X0, XS>, Cons<Y0, YS>> protocol, Protocol<S, Z> continuation) where X0 : SessionType where XS : SessionList where Y0 : SessionType where YS : SessionList where S : SessionType where Z : SessionType
		{
			if (protocol is null) throw new ArgumentNullException(nameof(protocol));
			if (continuation is null) throw new ArgumentNullException(nameof(continuation));
			return new Protocol<CatchNewChannel<X0, Cons<X0, XS>, S>, ThrowNewChannel<Y0, Cons<Y0, YS>, Z>>();
		}

		public static Protocol<Eps, Eps> End => new Protocol<Eps, Eps>();

		public static Protocol<Cons<S0, Nil>, Cons<Z0, Nil>> Arrange<S0, Z0>(Protocol<S0, Z0> sub0) where S0 : SessionType where Z0 : SessionType
		{
			if (sub0 is null) throw new ArgumentNullException(nameof(sub0));
			return new Protocol<Cons<S0, Nil>, Cons<Z0, Nil>>();
		}

		public static Protocol<Cons<S0, Cons<S1, Nil>>, Cons<Z0, Cons<Z1, Nil>>> Arrange<S0, S1, Z0, Z1>(Protocol<S0, Z0> sub0, Protocol<S1, Z1> sub1) where S0 : SessionType where S1 : SessionType where Z0 : SessionType where Z1 : SessionType
		{
			if (sub0 is null) throw new ArgumentNullException(nameof(sub0));
			if (sub1 is null) throw new ArgumentNullException(nameof(sub1));
			return new Protocol<Cons<S0, Cons<S1, Nil>>, Cons<Z0, Cons<Z1, Nil>>>();
		}

		public static Protocol<Cons<S0, Cons<S1, Cons<S2, Nil>>>, Cons<Z0, Cons<Z1, Cons<Z2, Nil>>>> Arrange<S0, S1, S2, Z0, Z1, Z2>(Protocol<S0, Z0> sub0, Protocol<S1, Z1> sub1, Protocol<S2, Z2> sub2) where S0 : SessionType where S1 : SessionType where S2 : SessionType where Z0 : SessionType where Z1 : SessionType where Z2 : SessionType
		{
			if (sub0 is null) throw new ArgumentNullException(nameof(sub0));
			if (sub1 is null) throw new ArgumentNullException(nameof(sub1));
			if (sub2 is null) throw new ArgumentNullException(nameof(sub2));
			return new Protocol<Cons<S0, Cons<S1, Cons<S2, Nil>>>, Cons<Z0, Cons<Z1, Cons<Z2, Nil>>>>();
		}

		public static Protocol<Cons<S0, Cons<S1, Cons<S2, Cons<S3, Nil>>>>, Cons<Z0, Cons<Z1, Cons<Z2, Cons<Z3, Nil>>>>> Arrange<S0, S1, S2, S3, Z0, Z1, Z2, Z3>(Protocol<S0, Z0> sub0, Protocol<S1, Z1> sub1, Protocol<S2, Z2> sub2, Protocol<S3, Z3> sub3) where S0 : SessionType where S1 : SessionType where S2 : SessionType where Z0 : SessionType where Z1 : SessionType where Z2 : SessionType where S3 : SessionType where Z3 : SessionType
		{
			if (sub0 is null) throw new ArgumentNullException(nameof(sub0));
			if (sub1 is null) throw new ArgumentNullException(nameof(sub1));
			if (sub2 is null) throw new ArgumentNullException(nameof(sub2));
			if (sub3 is null) throw new ArgumentNullException(nameof(sub3));
			return new Protocol<Cons<S0, Cons<S1, Cons<S2, Cons<S3, Nil>>>>, Cons<Z0, Cons<Z1, Cons<Z2, Cons<Z3, Nil>>>>>();
		}

		public static Protocol<Cons<S0, Cons<S1, Cons<S2, Cons<S3, Cons<S4, Nil>>>>>, Cons<Z0, Cons<Z1, Cons<Z2, Cons<Z3, Cons<Z4, Nil>>>>>> Arrange<S0, S1, S2, S3, S4, Z0, Z1, Z2, Z3, Z4>(Protocol<S0, Z0> sub0, Protocol<S1, Z1> sub1, Protocol<S2, Z2> sub2, Protocol<S3, Z3> sub3, Protocol<S4, Z4> sub4) where S0 : SessionType where S1 : SessionType where S2 : SessionType where Z0 : SessionType where Z1 : SessionType where Z2 : SessionType where S3 : SessionType where Z3 : SessionType where S4 : SessionType where Z4 : SessionType
		{
			if (sub0 is null) throw new ArgumentNullException(nameof(sub0));
			if (sub1 is null) throw new ArgumentNullException(nameof(sub1));
			if (sub2 is null) throw new ArgumentNullException(nameof(sub2));
			if (sub3 is null) throw new ArgumentNullException(nameof(sub3));
			if (sub4 is null) throw new ArgumentNullException(nameof(sub4));
			return new Protocol<Cons<S0, Cons<S1, Cons<S2, Cons<S3, Cons<S4, Nil>>>>>, Cons<Z0, Cons<Z1, Cons<Z2, Cons<Z3, Cons<Z4, Nil>>>>>>();
		}

		public static Protocol<Cons<S0, Cons<S1, Cons<S2, Cons<S3, Cons<S4, Cons<S5, Nil>>>>>>, Cons<Z0, Cons<Z1, Cons<Z2, Cons<Z3, Cons<Z4, Cons<Z5, Nil>>>>>>> Arrange<S0, S1, S2, S3, S4, S5, Z0, Z1, Z2, Z3, Z4, Z5>(Protocol<S0, Z0> sub0, Protocol<S1, Z1> sub1, Protocol<S2, Z2> sub2, Protocol<S3, Z3> sub3, Protocol<S4, Z4> sub4, Protocol<S5, Z5> sub5) where S0 : SessionType where S1 : SessionType where S2 : SessionType where Z0 : SessionType where Z1 : SessionType where Z2 : SessionType where S3 : SessionType where Z3 : SessionType where S4 : SessionType where Z4 : SessionType where S5 : SessionType where Z5 : SessionType
		{
			if (sub0 is null) throw new ArgumentNullException(nameof(sub0));
			if (sub1 is null) throw new ArgumentNullException(nameof(sub1));
			if (sub2 is null) throw new ArgumentNullException(nameof(sub2));
			if (sub3 is null) throw new ArgumentNullException(nameof(sub3));
			if (sub4 is null) throw new ArgumentNullException(nameof(sub4));
			if (sub5 is null) throw new ArgumentNullException(nameof(sub5));
			return new Protocol<Cons<S0, Cons<S1, Cons<S2, Cons<S3, Cons<S4, Cons<S5, Nil>>>>>>, Cons<Z0, Cons<Z1, Cons<Z2, Cons<Z3, Cons<Z4, Cons<Z5, Nil>>>>>>>();
		}

		public static Protocol<Cons<S0, Cons<S1, Cons<S2, Cons<S3, Cons<S4, Cons<S5, Cons<S6, Nil>>>>>>>, Cons<Z0, Cons<Z1, Cons<Z2, Cons<Z3, Cons<Z4, Cons<Z5, Cons<Z6, Nil>>>>>>>> Arrange<S0, S1, S2, S3, S4, S5, S6, Z0, Z1, Z2, Z3, Z4, Z5, Z6>(Protocol<S0, Z0> sub0, Protocol<S1, Z1> sub1, Protocol<S2, Z2> sub2, Protocol<S3, Z3> sub3, Protocol<S4, Z4> sub4, Protocol<S5, Z5> sub5, Protocol<S6, Z6> sub6) where S0 : SessionType where S1 : SessionType where S2 : SessionType where Z0 : SessionType where Z1 : SessionType where Z2 : SessionType where S3 : SessionType where Z3 : SessionType where S4 : SessionType where Z4 : SessionType where S5 : SessionType where Z5 : SessionType where S6 : SessionType where Z6 : SessionType
		{
			if (sub0 is null) throw new ArgumentNullException(nameof(sub0));
			if (sub1 is null) throw new ArgumentNullException(nameof(sub1));
			if (sub2 is null) throw new ArgumentNullException(nameof(sub2));
			if (sub3 is null) throw new ArgumentNullException(nameof(sub3));
			if (sub4 is null) throw new ArgumentNullException(nameof(sub4));
			if (sub5 is null) throw new ArgumentNullException(nameof(sub5));
			if (sub6 is null) throw new ArgumentNullException(nameof(sub6));
			return new Protocol<Cons<S0, Cons<S1, Cons<S2, Cons<S3, Cons<S4, Cons<S5, Cons<S6, Nil>>>>>>>, Cons<Z0, Cons<Z1, Cons<Z2, Cons<Z3, Cons<Z4, Cons<Z5, Cons<Z6, Nil>>>>>>>>();
		}

		public static Protocol<Cons<S0, Cons<S1, Cons<S2, Cons<S3, Cons<S4, Cons<S5, Cons<S6, Cons<S7, Nil>>>>>>>>, Cons<Z0, Cons<Z1, Cons<Z2, Cons<Z3, Cons<Z4, Cons<Z5, Cons<Z6, Cons<Z7, Nil>>>>>>>>> Arrange<S0, S1, S2, S3, S4, S5, S6, S7, Z0, Z1, Z2, Z3, Z4, Z5, Z6, Z7>(Protocol<S0, Z0> sub0, Protocol<S1, Z1> sub1, Protocol<S2, Z2> sub2, Protocol<S3, Z3> sub3, Protocol<S4, Z4> sub4, Protocol<S5, Z5> sub5, Protocol<S6, Z6> sub6, Protocol<S7, Z7> sub7) where S0 : SessionType where S1 : SessionType where S2 : SessionType where Z0 : SessionType where Z1 : SessionType where Z2 : SessionType where S3 : SessionType where Z3 : SessionType where S4 : SessionType where Z4 : SessionType where S5 : SessionType where Z5 : SessionType where S6 : SessionType where Z6 : SessionType where S7 : SessionType where Z7 : SessionType
		{
			if (sub0 is null) throw new ArgumentNullException(nameof(sub0));
			if (sub1 is null) throw new ArgumentNullException(nameof(sub1));
			if (sub2 is null) throw new ArgumentNullException(nameof(sub2));
			if (sub3 is null) throw new ArgumentNullException(nameof(sub3));
			if (sub4 is null) throw new ArgumentNullException(nameof(sub4));
			if (sub5 is null) throw new ArgumentNullException(nameof(sub5));
			if (sub6 is null) throw new ArgumentNullException(nameof(sub6));
			if (sub7 is null) throw new ArgumentNullException(nameof(sub7));
			return new Protocol<Cons<S0, Cons<S1, Cons<S2, Cons<S3, Cons<S4, Cons<S5, Cons<S6, Cons<S7, Nil>>>>>>>>, Cons<Z0, Cons<Z1, Cons<Z2, Cons<Z3, Cons<Z4, Cons<Z5, Cons<Z6, Cons<Z7, Nil>>>>>>>>>();
		}

		public static Protocol<Cons<S0, Cons<S1, Cons<S2, Cons<S3, Cons<S4, Cons<S5, Cons<S6, Cons<S7, Cons<S8, Nil>>>>>>>>>, Cons<Z0, Cons<Z1, Cons<Z2, Cons<Z3, Cons<Z4, Cons<Z5, Cons<Z6, Cons<Z7, Cons<Z8, Nil>>>>>>>>>> Arrange<S0, S1, S2, S3, S4, S5, S6, S7, S8, Z0, Z1, Z2, Z3, Z4, Z5, Z6, Z7, Z8>(Protocol<S0, Z0> sub0, Protocol<S1, Z1> sub1, Protocol<S2, Z2> sub2, Protocol<S3, Z3> sub3, Protocol<S4, Z4> sub4, Protocol<S5, Z5> sub5, Protocol<S6, Z6> sub6, Protocol<S7, Z7> sub7, Protocol<S8, Z8> sub8) where S0 : SessionType where S1 : SessionType where S2 : SessionType where Z0 : SessionType where Z1 : SessionType where Z2 : SessionType where S3 : SessionType where Z3 : SessionType where S4 : SessionType where Z4 : SessionType where S5 : SessionType where Z5 : SessionType where S6 : SessionType where Z6 : SessionType where S7 : SessionType where Z7 : SessionType where S8 : SessionType where Z8 : SessionType
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
			return new Protocol<Cons<S0, Cons<S1, Cons<S2, Cons<S3, Cons<S4, Cons<S5, Cons<S6, Cons<S7, Cons<S8, Nil>>>>>>>>>, Cons<Z0, Cons<Z1, Cons<Z2, Cons<Z3, Cons<Z4, Cons<Z5, Cons<Z6, Cons<Z7, Cons<Z8, Nil>>>>>>>>>>();
		}

		public static Protocol<Cons<S0, Cons<S1, Cons<S2, Cons<S3, Cons<S4, Cons<S5, Cons<S6, Cons<S7, Cons<S8, Cons<S9, Nil>>>>>>>>>>, Cons<Z0, Cons<Z1, Cons<Z2, Cons<Z3, Cons<Z4, Cons<Z5, Cons<Z6, Cons<Z7, Cons<Z8, Cons<Z9, Nil>>>>>>>>>>> Arrange<S0, S1, S2, S3, S4, S5, S6, S7, S8, S9, Z0, Z1, Z2, Z3, Z4, Z5, Z6, Z7, Z8, Z9>(Protocol<S0, Z0> sub0, Protocol<S1, Z1> sub1, Protocol<S2, Z2> sub2, Protocol<S3, Z3> sub3, Protocol<S4, Z4> sub4, Protocol<S5, Z5> sub5, Protocol<S6, Z6> sub6, Protocol<S7, Z7> sub7, Protocol<S8, Z8> sub8, Protocol<S9, Z9> sub9) where S0 : SessionType where S1 : SessionType where S2 : SessionType where Z0 : SessionType where Z1 : SessionType where Z2 : SessionType where S3 : SessionType where Z3 : SessionType where S4 : SessionType where Z4 : SessionType where S5 : SessionType where Z5 : SessionType where S6 : SessionType where Z6 : SessionType where S7 : SessionType where Z7 : SessionType where S8 : SessionType where Z8 : SessionType where S9 : SessionType where Z9 : SessionType
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
			return new Protocol<Cons<S0, Cons<S1, Cons<S2, Cons<S3, Cons<S4, Cons<S5, Cons<S6, Cons<S7, Cons<S8, Cons<S9, Nil>>>>>>>>>>, Cons<Z0, Cons<Z1, Cons<Z2, Cons<Z3, Cons<Z4, Cons<Z5, Cons<Z6, Cons<Z7, Cons<Z8, Cons<Z9, Nil>>>>>>>>>>>();
		}
	}
}
