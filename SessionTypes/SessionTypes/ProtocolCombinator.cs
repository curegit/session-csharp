using System;

namespace SessionTypes
{
	public sealed class UnitPayload
	{
		internal UnitPayload() { }
	}

	public sealed class Payload<T>
	{
		internal Payload() { }
	}

	public sealed class SerialPayloads<T1, T2>
	{
		internal SerialPayloads() { }
	}

	public sealed class SerialPayloads<T1, T2, T3>
	{
		internal SerialPayloads() { }
	}

	public sealed class SerialPayloads<T1, T2, T3, T4>
	{
		internal SerialPayloads() { }
	}

	public sealed class SerialPayloads<T1, T2, T3, T4, T5>
	{
		internal SerialPayloads() { }
	}

	public delegate Payload<T> PayloadDelegate<T>();

	public delegate SerialPayloads<T1, T2> SerialPayloadDelegate<T1, T2>();

	public delegate SerialPayloads<T1, T2, T3> SerialPayloadDelegate<T1, T2, T3>();

	public delegate SerialPayloads<T1, T2, T3, T4> SerialPayloadDelegate<T1, T2, T3, T4>();

	public delegate SerialPayloads<T1, T2, T3, T4, T5> SerialPayloadDelegate<T1, T2, T3, T4, T5>();

	public static class ProtocolCombinator
	{
		public static UnitPayload Unit => new UnitPayload();

		public static Payload<T> Value<T>() => new Payload<T>();

		public static SerialPayloads<T1, T2> SerialValues<T1, T2>() => new SerialPayloads<T1, T2>();

		public static SerialPayloads<T1, T2, T3> SerialValues<T1, T2, T3>() => new SerialPayloads<T1, T2, T3>();

		public static SerialPayloads<T1, T2, T3, T4> SerialValues<T1, T2, T3, T4>() => new SerialPayloads<T1, T2, T3, T4>();

		public static SerialPayloads<T1, T2, T3, T4, T5> SerialValues<T1, T2, T3, T4, T5>() => new SerialPayloads<T1, T2, T3, T4, T5>();

		public static Payload<Session<S, Empty, P>> Channel<S, P, Z, Q>(Protocol<S, P, Z, Q> protocol) where S : SessionType where P : ProtocolType where Z : SessionType where Q : ProtocolType
		{
			if (protocol is null) throw new ArgumentNullException(nameof(protocol));
			return new Payload<Session<S, Empty, P>>();
		}

		public static Payload<Session<S, Empty, A>> Channel<S, P, X, A, Z, Q, Y, B>(Protocol<S, P, Z, Q> session, Protocol<X, A, Y, B> protocol) where S : SessionType where P : ProtocolType where X : SessionType where A : ProtocolType where Z : SessionType where Q : ProtocolType where Y : SessionType where B : ProtocolType
		{
			if (session is null) throw new ArgumentNullException(nameof(session));
			if (protocol is null) throw new ArgumentNullException(nameof(protocol));
			return new Payload<Session<S, Empty, A>>();
		}

		public static Payload<Session<S, E, P>> Channel<S, E, P>(Session<S, E, P> session) where S : SessionType where E : SessionStack where P : ProtocolType
		{
			if (session is null) throw new ArgumentNullException(nameof(session));
			return new Payload<Session<S, E, P>>();
		}

		public static Protocol<Send<S>, Send<S>, Receive<Z>, Receive<Z>> Send<S, Z>(UnitPayload unit, Protocol<S, S, Z, Z> continuation) where S : SessionType where Z : SessionType
		{
			if (unit is null) throw new ArgumentNullException(nameof(unit));
			if (continuation is null) throw new ArgumentNullException(nameof(continuation));
			return new Protocol<Send<S>, Send<S>, Receive<Z>, Receive<Z>>();
		}

		public static Protocol<Send<T, S>, Send<T, S>, Receive<T, Z>, Receive<T, Z>> Send<T, S, Z>(PayloadDelegate<T> payload, Protocol<S, S, Z, Z> continuation) where S : SessionType where Z : SessionType
		{
			if (payload is null) throw new ArgumentNullException(nameof(payload));
			if (payload() is null) throw new ArgumentException("Return value cannot be null.", nameof(payload));
			if (continuation is null) throw new ArgumentNullException(nameof(continuation));
			return new Protocol<Send<T, S>, Send<T, S>, Receive<T, Z>, Receive<T, Z>>();
		}

		public static Protocol<Send<T1, Send<T2, S>>, Send<T1, Send<T2, S>>, Receive<T1, Receive<T2, Z>>, Receive<T1, Receive<T2, Z>>> Send<T1, T2, S, Z>(SerialPayloadDelegate<T1, T2> payload, Protocol<S, S, Z, Z> continuation) where S : SessionType where Z : SessionType
		{
			if (payload is null) throw new ArgumentNullException(nameof(payload));
			if (payload() is null) throw new ArgumentException("Return value cannot be null.", nameof(payload));
			if (continuation is null) throw new ArgumentNullException(nameof(continuation));
			return new Protocol<Send<T1, Send<T2, S>>, Send<T1, Send<T2, S>>, Receive<T1, Receive<T2, Z>>, Receive<T1, Receive<T2, Z>>>();
		}

		public static Protocol<Send<T1, Send<T2, Send<T3, S>>>, Send<T1, Send<T2, Send<T3, S>>>, Receive<T1, Receive<T2, Receive<T3, Z>>>, Receive<T1, Receive<T2, Receive<T3, Z>>>> Send<T1, T2, T3, S, Z>(SerialPayloadDelegate<T1, T2, T3> payload, Protocol<S, S, Z, Z> continuation) where S : SessionType where Z : SessionType
		{
			if (payload is null) throw new ArgumentNullException(nameof(payload));
			if (payload() is null) throw new ArgumentException("Return value cannot be null.", nameof(payload));
			if (continuation is null) throw new ArgumentNullException(nameof(continuation));
			return new Protocol<Send<T1, Send<T2, Send<T3, S>>>, Send<T1, Send<T2, Send<T3, S>>>, Receive<T1, Receive<T2, Receive<T3, Z>>>, Receive<T1, Receive<T2, Receive<T3, Z>>>>();
		}

		public static Protocol<Send<T1, Send<T2, Send<T3, Send<T4, S>>>>, Send<T1, Send<T2, Send<T3, Send<T4, S>>>>, Receive<T1, Receive<T2, Receive<T3, Receive<T4, Z>>>>, Receive<T1, Receive<T2, Receive<T3, Receive<T4, Z>>>>> Send<T1, T2, T3, T4, S, Z>(SerialPayloadDelegate<T1, T2, T3, T4> payload, Protocol<S, S, Z, Z> continuation) where S : SessionType where Z : SessionType
		{
			if (payload is null) throw new ArgumentNullException(nameof(payload));
			if (payload() is null) throw new ArgumentException("Return value cannot be null.", nameof(payload));
			if (continuation is null) throw new ArgumentNullException(nameof(continuation));
			return new Protocol<Send<T1, Send<T2, Send<T3, Send<T4, S>>>>, Send<T1, Send<T2, Send<T3, Send<T4, S>>>>, Receive<T1, Receive<T2, Receive<T3, Receive<T4, Z>>>>, Receive<T1, Receive<T2, Receive<T3, Receive<T4, Z>>>>>();
		}

		public static Protocol<Send<T1, Send<T2, Send<T3, Send<T4, Send<T5, S>>>>>, Send<T1, Send<T2, Send<T3, Send<T4, Send<T5, S>>>>>, Receive<T1, Receive<T2, Receive<T3, Receive<T4, Receive<T5, Z>>>>>, Receive<T1, Receive<T2, Receive<T3, Receive<T4, Receive<T5, Z>>>>>> Send<T1, T2, T3, T4, T5, S, Z>(SerialPayloadDelegate<T1, T2, T3, T4, T5> payload, Protocol<S, S, Z, Z> continuation) where S : SessionType where Z : SessionType
		{
			if (payload is null) throw new ArgumentNullException(nameof(payload));
			if (payload() is null) throw new ArgumentException("Return value cannot be null.", nameof(payload));
			if (continuation is null) throw new ArgumentNullException(nameof(continuation));
			return new Protocol<Send<T1, Send<T2, Send<T3, Send<T4, Send<T5, S>>>>>, Send<T1, Send<T2, Send<T3, Send<T4, Send<T5, S>>>>>, Receive<T1, Receive<T2, Receive<T3, Receive<T4, Receive<T5, Z>>>>>, Receive<T1, Receive<T2, Receive<T3, Receive<T4, Receive<T5, Z>>>>>>();
		}

		public static Protocol<Receive<S>, Receive<S>, Send<Z>, Send<Z>> Receive<S, Z>(UnitPayload unit, Protocol<S, S, Z, Z> continuation) where S : SessionType where Z : SessionType
		{
			if (unit is null) throw new ArgumentNullException(nameof(unit));
			if (continuation is null) throw new ArgumentNullException(nameof(continuation));
			return new Protocol<Receive<S>, Receive<S>, Send<Z>, Send<Z>>();
		}

		public static Protocol<Receive<T, S>, Receive<T, S>, Send<T, Z>, Send<T, Z>> Receive<T, S, Z>(PayloadDelegate<T> payload, Protocol<S, S, Z, Z> continuation) where S : SessionType where Z : SessionType
		{
			if (payload is null) throw new ArgumentNullException(nameof(payload));
			if (payload() is null) throw new ArgumentException("Return value cannot be null.", nameof(payload));
			if (continuation is null) throw new ArgumentNullException(nameof(continuation));
			return new Protocol<Receive<T, S>, Receive<T, S>, Send<T, Z>, Send<T, Z>>();
		}

		public static Protocol<Receive<T1, Receive<T2, S>>, Receive<T1, Receive<T2, S>>, Send<T1, Send<T2, Z>>, Send<T1, Send<T2, Z>>> Receive<T1, T2, S, Z>(SerialPayloadDelegate<T1, T2> payload, Protocol<S, S, Z, Z> continuation) where S : SessionType where Z : SessionType
		{
			if (payload is null) throw new ArgumentNullException(nameof(payload));
			if (payload() is null) throw new ArgumentException("Return value cannot be null.", nameof(payload));
			if (continuation is null) throw new ArgumentNullException(nameof(continuation));
			return new Protocol<Receive<T1, Receive<T2, S>>, Receive<T1, Receive<T2, S>>, Send<T1, Send<T2, Z>>, Send<T1, Send<T2, Z>>>();
		}

		public static Protocol<Receive<T1, Receive<T2, Receive<T3, S>>>, Receive<T1, Receive<T2, Receive<T3, S>>>, Send<T1, Send<T2, Send<T3, Z>>>, Send<T1, Send<T2, Send<T3, Z>>>> Receive<T1, T2, T3, S, Z>(SerialPayloadDelegate<T1, T2, T3> payload, Protocol<S, S, Z, Z> continuation) where S : SessionType where Z : SessionType
		{
			if (payload is null) throw new ArgumentNullException(nameof(payload));
			if (payload() is null) throw new ArgumentException("Return value cannot be null.", nameof(payload));
			if (continuation is null) throw new ArgumentNullException(nameof(continuation));
			return new Protocol<Receive<T1, Receive<T2, Receive<T3, S>>>, Receive<T1, Receive<T2, Receive<T3, S>>>, Send<T1, Send<T2, Send<T3, Z>>>, Send<T1, Send<T2, Send<T3, Z>>>>();
		}

		public static Protocol<Receive<T1, Receive<T2, Receive<T3, Receive<T4, S>>>>, Receive<T1, Receive<T2, Receive<T3, Receive<T4, S>>>>, Send<T1, Send<T2, Send<T3, Send<T4, Z>>>>, Send<T1, Send<T2, Send<T3, Send<T4, Z>>>>> Receive<T1, T2, T3, T4, S, Z>(SerialPayloadDelegate<T1, T2, T3, T4> payload, Protocol<S, S, Z, Z> continuation) where S : SessionType where Z : SessionType
		{
			if (payload is null) throw new ArgumentNullException(nameof(payload));
			if (payload() is null) throw new ArgumentException("Return value cannot be null.", nameof(payload));
			if (continuation is null) throw new ArgumentNullException(nameof(continuation));
			return new Protocol<Receive<T1, Receive<T2, Receive<T3, Receive<T4, S>>>>, Receive<T1, Receive<T2, Receive<T3, Receive<T4, S>>>>, Send<T1, Send<T2, Send<T3, Send<T4, Z>>>>, Send<T1, Send<T2, Send<T3, Send<T4, Z>>>>>();
		}

		public static Protocol<Receive<T1, Receive<T2, Receive<T3, Receive<T4, Receive<T5, S>>>>>, Receive<T1, Receive<T2, Receive<T3, Receive<T4, Receive<T5, S>>>>>, Send<T1, Send<T2, Send<T3, Send<T4, Send<T5, Z>>>>>, Send<T1, Send<T2, Send<T3, Send<T4, Send<T5, Z>>>>>> Receive<T1, T2, T3, T4, T5, S, Z>(SerialPayloadDelegate<T1, T2, T3, T4, T5> payload, Protocol<S, S, Z, Z> continuation) where S : SessionType where Z : SessionType
		{
			if (payload is null) throw new ArgumentNullException(nameof(payload));
			if (payload() is null) throw new ArgumentException("Return value cannot be null.", nameof(payload));
			if (continuation is null) throw new ArgumentNullException(nameof(continuation));
			return new Protocol<Receive<T1, Receive<T2, Receive<T3, Receive<T4, Receive<T5, S>>>>>, Receive<T1, Receive<T2, Receive<T3, Receive<T4, Receive<T5, S>>>>>, Send<T1, Send<T2, Send<T3, Send<T4, Send<T5, Z>>>>>, Send<T1, Send<T2, Send<T3, Send<T4, Send<T5, Z>>>>>>();
		}

		public static Protocol<Select<L, R>, Select<L, R>, Follow<X, Y>, Follow<X, Y>> Select<L, R, X, Y>(Protocol<L, L, X, X> left, Protocol<R, R, Y, Y> right) where L : SessionType where R : SessionType where X : SessionType where Y : SessionType
		{
			if (left is null) throw new ArgumentNullException(nameof(left));
			if (right is null) throw new ArgumentNullException(nameof(right));
			return new Protocol<Select<L, R>, Select<L, R>, Follow<X, Y>, Follow<X, Y>>();
		}

		public static Protocol<Select<L, C, R>, Select<L, C, R>, Follow<X, W, Y>, Follow<X, W, Y>> Select<L, C, R, X, W, Y>(Protocol<L, L, X, X> left, Protocol<C, C, W, W> center, Protocol<R, R, Y, Y> right) where L : SessionType where C : SessionType where R : SessionType where X : SessionType where W : SessionType where Y : SessionType
		{
			if (left is null) throw new ArgumentNullException(nameof(left));
			if (center is null) throw new ArgumentNullException(nameof(center));
			if (right is null) throw new ArgumentNullException(nameof(right));
			return new Protocol<Select<L, C, R>, Select<L, C, R>, Follow<X, W, Y>, Follow<X, W, Y>>();
		}

		public static Protocol<Follow<L, R>, Follow<L, R>, Select<X, Y>, Select<X, Y>> Follow<L, R, X, Y>(Protocol<L, L, X, X> left, Protocol<R, R, Y, Y> right) where L : SessionType where R : SessionType where X : SessionType where Y : SessionType
		{
			if (left is null) throw new ArgumentNullException(nameof(left));
			if (right is null) throw new ArgumentNullException(nameof(right));
			return new Protocol<Follow<L, R>, Follow<L, R>, Select<X, Y>, Select<X, Y>>();
		}

		public static Protocol<Follow<L, C, R>, Follow<L, C, R>, Select<X, W, Y>, Select<X, W, Y>> Follow<L, C, R, X, W, Y>(Protocol<L, L, X, X> left, Protocol<C, C, W, W> center, Protocol<R, R, Y, Y> right) where L : SessionType where C : SessionType where R : SessionType where X : SessionType where W : SessionType where Y : SessionType
		{
			if (left is null) throw new ArgumentNullException(nameof(left));
			if (center is null) throw new ArgumentNullException(nameof(center));
			if (right is null) throw new ArgumentNullException(nameof(right));
			return new Protocol<Follow<L, C, R>, Follow<L, C, R>, Select<X, W, Y>, Select<X, W, Y>>();
		}

		public static Protocol<Call0, Call0, Call0, Call0> Goto0 => new Protocol<Call0, Call0, Call0, Call0>();

		public static Protocol<Call1, Call1, Call1, Call1> Goto1 => new Protocol<Call1, Call1, Call1, Call1>();

		public static Protocol<Call2, Call2, Call2, Call2> Goto2 => new Protocol<Call2, Call2, Call2, Call2>();

		public static Protocol<Call3, Call3, Call3, Call3> Goto3 => new Protocol<Call3, Call3, Call3, Call3>();

		public static Protocol<Call4, Call4, Call4, Call4> Goto4 => new Protocol<Call4, Call4, Call4, Call4>();

		public static Protocol<Call5, Call5, Call5, Call5> Goto5 => new Protocol<Call5, Call5, Call5, Call5>();

		public static Protocol<Call6, Call6, Call6, Call6> Goto6 => new Protocol<Call6, Call6, Call6, Call6>();

		public static Protocol<Call7, Call7, Call7, Call7> Goto7 => new Protocol<Call7, Call7, Call7, Call7>();

		public static Protocol<Call8, Call8, Call8, Call8> Goto8 => new Protocol<Call8, Call8, Call8, Call8>();

		public static Protocol<Call9, Call9, Call9, Call9> Goto9 => new Protocol<Call9, Call9, Call9, Call9>();

		public static Protocol<Call0<S>, Call0<S>, Call0<Z>, Call0<Z>> Call0<S, Z>(Protocol<S, S, Z, Z> continuation) where S : SessionType where Z : SessionType
		{
			if (continuation is null) throw new ArgumentNullException(nameof(continuation));
			return new Protocol<Call0<S>, Call0<S>, Call0<Z>, Call0<Z>>();
		}

		public static Protocol<Call1<S>, Call1<S>, Call1<Z>, Call1<Z>> Call1<S, Z>(Protocol<S, S, Z, Z> continuation) where S : SessionType where Z : SessionType
		{
			if (continuation is null) throw new ArgumentNullException(nameof(continuation));
			return new Protocol<Call1<S>, Call1<S>, Call1<Z>, Call1<Z>>();
		}

		public static Protocol<Call2<S>, Call2<S>, Call2<Z>, Call2<Z>> Call2<S, Z>(Protocol<S, S, Z, Z> continuation) where S : SessionType where Z : SessionType
		{
			if (continuation is null) throw new ArgumentNullException(nameof(continuation));
			return new Protocol<Call2<S>, Call2<S>, Call2<Z>, Call2<Z>>();
		}

		public static Protocol<Call3<S>, Call3<S>, Call3<Z>, Call3<Z>> Call3<S, Z>(Protocol<S, S, Z, Z> continuation) where S : SessionType where Z : SessionType
		{
			if (continuation is null) throw new ArgumentNullException(nameof(continuation));
			return new Protocol<Call3<S>, Call3<S>, Call3<Z>, Call3<Z>>();
		}

		public static Protocol<Call4<S>, Call4<S>, Call4<Z>, Call4<Z>> Call4<S, Z>(Protocol<S, S, Z, Z> continuation) where S : SessionType where Z : SessionType
		{
			if (continuation is null) throw new ArgumentNullException(nameof(continuation));
			return new Protocol<Call4<S>, Call4<S>, Call4<Z>, Call4<Z>>();
		}

		public static Protocol<Call5<S>, Call5<S>, Call5<Z>, Call5<Z>> Call5<S, Z>(Protocol<S, S, Z, Z> continuation) where S : SessionType where Z : SessionType
		{
			if (continuation is null) throw new ArgumentNullException(nameof(continuation));
			return new Protocol<Call5<S>, Call5<S>, Call5<Z>, Call5<Z>>();
		}

		public static Protocol<Call6<S>, Call6<S>, Call6<Z>, Call6<Z>> Call6<S, Z>(Protocol<S, S, Z, Z> continuation) where S : SessionType where Z : SessionType
		{
			if (continuation is null) throw new ArgumentNullException(nameof(continuation));
			return new Protocol<Call6<S>, Call6<S>, Call6<Z>, Call6<Z>>();
		}

		public static Protocol<Call7<S>, Call7<S>, Call7<Z>, Call7<Z>> Call7<S, Z>(Protocol<S, S, Z, Z> continuation) where S : SessionType where Z : SessionType
		{
			if (continuation is null) throw new ArgumentNullException(nameof(continuation));
			return new Protocol<Call7<S>, Call7<S>, Call7<Z>, Call7<Z>>();
		}

		public static Protocol<Call8<S>, Call8<S>, Call8<Z>, Call8<Z>> Call8<S, Z>(Protocol<S, S, Z, Z> continuation) where S : SessionType where Z : SessionType
		{
			if (continuation is null) throw new ArgumentNullException(nameof(continuation));
			return new Protocol<Call8<S>, Call8<S>, Call8<Z>, Call8<Z>>();
		}

		public static Protocol<Call9<S>, Call9<S>, Call9<Z>, Call9<Z>> Call9<S, Z>(Protocol<S, S, Z, Z> continuation) where S : SessionType where Z : SessionType
		{
			if (continuation is null) throw new ArgumentNullException(nameof(continuation));
			return new Protocol<Call9<S>, Call9<S>, Call9<Z>, Call9<Z>>();
		}

		public static Protocol<Cast<X, P, S>, Cast<X, P, S>, Accept<Y, Q, Z>, Accept<Y, Q, Z>> CastNewChannel<X, P, Y, Q, Z, S>(Protocol<X, P, Y, Q> protocol, Protocol<S, S, Z, Z> continuation) where X : SessionType where P : ProtocolType where Y : SessionType where Q : ProtocolType where S : SessionType where Z : SessionType
		{
			if (protocol is null) throw new ArgumentNullException(nameof(protocol));
			if (continuation is null) throw new ArgumentNullException(nameof(continuation));
			return new Protocol<Cast<X, P, S>, Cast<X, P, S>, Accept<Y, Q, Z>, Accept<Y, Q, Z>>();
		}

		public static Protocol<Accept<X, P, S>, Accept<X, P, S>, Cast<Y, Q, Z>, Cast<Y, Q, Z>> AcceptNewChannel<X, P, Y, Q, Z, S>(Protocol<X, P, Y, Q> protocol, Protocol<S, S, Z, Z> continuation) where X : SessionType where P : ProtocolType where Y : SessionType where Q : ProtocolType where S : SessionType where Z : SessionType
		{
			if (protocol is null) throw new ArgumentNullException(nameof(protocol));
			if (continuation is null) throw new ArgumentNullException(nameof(continuation));
			return new Protocol<Accept<X, P, S>, Accept<X, P, S>, Cast<Y, Q, Z>, Cast<Y, Q, Z>>();
		}

		public static Protocol<Eps, Eps, Eps, Eps> End => new Protocol<Eps, Eps, Eps, Eps>();

		public static Protocol<S0, Cons<S0, Nil>, Z0, Cons<Z0, Nil>> Array<S0, Z0>(Protocol<S0, S0, Z0, Z0> sub0) where S0 : SessionType where Z0 : SessionType
		{
			if (sub0 is null) throw new ArgumentNullException(nameof(sub0));
			return new Protocol<S0, Cons<S0, Nil>, Z0, Cons<Z0, Nil>>();
		}

		public static Protocol<S0, Cons<S0, Cons<S1, Nil>>, Z0, Cons<Z0, Cons<Z1, Nil>>> Array<S0, S1, Z0, Z1>(Protocol<S0, S0, Z0, Z0> sub0, Protocol<S1, S1, Z1, Z1> sub1) where S0 : SessionType where S1 : SessionType where Z0 : SessionType where Z1 : SessionType
		{
			if (sub0 is null) throw new ArgumentNullException(nameof(sub0));
			if (sub1 is null) throw new ArgumentNullException(nameof(sub1));
			return new Protocol<S0, Cons<S0, Cons<S1, Nil>>, Z0, Cons<Z0, Cons<Z1, Nil>>>();
		}

		public static Protocol<S0, Cons<S0, Cons<S1, Cons<S2, Nil>>>, Z0, Cons<Z0, Cons<Z1, Cons<Z2, Nil>>>> Array<S0, S1, S2, Z0, Z1, Z2>(Protocol<S0, S0, Z0, Z0> sub0, Protocol<S1, S1, Z1, Z1> sub1, Protocol<S2, S2, Z2, Z2> sub2) where S0 : SessionType where S1 : SessionType where S2 : SessionType where Z0 : SessionType where Z1 : SessionType where Z2 : SessionType
		{
			if (sub0 is null) throw new ArgumentNullException(nameof(sub0));
			if (sub1 is null) throw new ArgumentNullException(nameof(sub1));
			if (sub2 is null) throw new ArgumentNullException(nameof(sub2));
			return new Protocol<S0, Cons<S0, Cons<S1, Cons<S2, Nil>>>, Z0, Cons<Z0, Cons<Z1, Cons<Z2, Nil>>>>();
		}

		public static Protocol<S0, Cons<S0, Cons<S1, Cons<S2, Cons<S3, Nil>>>>, Z0, Cons<Z0, Cons<Z1, Cons<Z2, Cons<Z3, Nil>>>>> Array<S0, S1, S2, S3, Z0, Z1, Z2, Z3>(Protocol<S0, S0, Z0, Z0> sub0, Protocol<S1, S1, Z1, Z1> sub1, Protocol<S2, S2, Z2, Z2> sub2, Protocol<S3, S3, Z3, Z3> sub3) where S0 : SessionType where S1 : SessionType where S2 : SessionType where Z0 : SessionType where Z1 : SessionType where Z2 : SessionType where S3 : SessionType where Z3 : SessionType
		{
			if (sub0 is null) throw new ArgumentNullException(nameof(sub0));
			if (sub1 is null) throw new ArgumentNullException(nameof(sub1));
			if (sub2 is null) throw new ArgumentNullException(nameof(sub2));
			if (sub3 is null) throw new ArgumentNullException(nameof(sub3));
			return new Protocol<S0, Cons<S0, Cons<S1, Cons<S2, Cons<S3, Nil>>>>, Z0, Cons<Z0, Cons<Z1, Cons<Z2, Cons<Z3, Nil>>>>>();
		}

		public static Protocol<S0, Cons<S0, Cons<S1, Cons<S2, Cons<S3, Cons<S4, Nil>>>>>, Z0, Cons<Z0, Cons<Z1, Cons<Z2, Cons<Z3, Cons<Z4, Nil>>>>>> Array<S0, S1, S2, S3, S4, Z0, Z1, Z2, Z3, Z4>(Protocol<S0, S0, Z0, Z0> sub0, Protocol<S1, S1, Z1, Z1> sub1, Protocol<S2, S2, Z2, Z2> sub2, Protocol<S3, S3, Z3, Z3> sub3, Protocol<S4, S4, Z4, Z4> sub4) where S0 : SessionType where S1 : SessionType where S2 : SessionType where Z0 : SessionType where Z1 : SessionType where Z2 : SessionType where S3 : SessionType where Z3 : SessionType where S4 : SessionType where Z4 : SessionType
		{
			if (sub0 is null) throw new ArgumentNullException(nameof(sub0));
			if (sub1 is null) throw new ArgumentNullException(nameof(sub1));
			if (sub2 is null) throw new ArgumentNullException(nameof(sub2));
			if (sub3 is null) throw new ArgumentNullException(nameof(sub3));
			if (sub4 is null) throw new ArgumentNullException(nameof(sub4));
			return new Protocol<S0, Cons<S0, Cons<S1, Cons<S2, Cons<S3, Cons<S4, Nil>>>>>, Z0, Cons<Z0, Cons<Z1, Cons<Z2, Cons<Z3, Cons<Z4, Nil>>>>>>();
		}

		public static Protocol<S0, Cons<S0, Cons<S1, Cons<S2, Cons<S3, Cons<S4, Cons<S5, Nil>>>>>>, Z0, Cons<Z0, Cons<Z1, Cons<Z2, Cons<Z3, Cons<Z4, Cons<Z5, Nil>>>>>>> Array<S0, S1, S2, S3, S4, S5, Z0, Z1, Z2, Z3, Z4, Z5>(Protocol<S0, S0, Z0, Z0> sub0, Protocol<S1, S1, Z1, Z1> sub1, Protocol<S2, S2, Z2, Z2> sub2, Protocol<S3, S3, Z3, Z3> sub3, Protocol<S4, S4, Z4, Z4> sub4, Protocol<S5, S5, Z5, Z5> sub5) where S0 : SessionType where S1 : SessionType where S2 : SessionType where Z0 : SessionType where Z1 : SessionType where Z2 : SessionType where S3 : SessionType where Z3 : SessionType where S4 : SessionType where Z4 : SessionType where S5 : SessionType where Z5 : SessionType
		{
			if (sub0 is null) throw new ArgumentNullException(nameof(sub0));
			if (sub1 is null) throw new ArgumentNullException(nameof(sub1));
			if (sub2 is null) throw new ArgumentNullException(nameof(sub2));
			if (sub3 is null) throw new ArgumentNullException(nameof(sub3));
			if (sub4 is null) throw new ArgumentNullException(nameof(sub4));
			if (sub5 is null) throw new ArgumentNullException(nameof(sub5));
			return new Protocol<S0, Cons<S0, Cons<S1, Cons<S2, Cons<S3, Cons<S4, Cons<S5, Nil>>>>>>, Z0, Cons<Z0, Cons<Z1, Cons<Z2, Cons<Z3, Cons<Z4, Cons<Z5, Nil>>>>>>>();
		}

		public static Protocol<S0, Cons<S0, Cons<S1, Cons<S2, Cons<S3, Cons<S4, Cons<S5, Cons<S6, Nil>>>>>>>, Z0, Cons<Z0, Cons<Z1, Cons<Z2, Cons<Z3, Cons<Z4, Cons<Z5, Cons<Z6, Nil>>>>>>>> Array<S0, S1, S2, S3, S4, S5, S6, Z0, Z1, Z2, Z3, Z4, Z5, Z6>(Protocol<S0, S0, Z0, Z0> sub0, Protocol<S1, S1, Z1, Z1> sub1, Protocol<S2, S2, Z2, Z2> sub2, Protocol<S3, S3, Z3, Z3> sub3, Protocol<S4, S4, Z4, Z4> sub4, Protocol<S5, S5, Z5, Z5> sub5, Protocol<S6, S6, Z6, Z6> sub6) where S0 : SessionType where S1 : SessionType where S2 : SessionType where Z0 : SessionType where Z1 : SessionType where Z2 : SessionType where S3 : SessionType where Z3 : SessionType where S4 : SessionType where Z4 : SessionType where S5 : SessionType where Z5 : SessionType where S6 : SessionType where Z6 : SessionType
		{
			if (sub0 is null) throw new ArgumentNullException(nameof(sub0));
			if (sub1 is null) throw new ArgumentNullException(nameof(sub1));
			if (sub2 is null) throw new ArgumentNullException(nameof(sub2));
			if (sub3 is null) throw new ArgumentNullException(nameof(sub3));
			if (sub4 is null) throw new ArgumentNullException(nameof(sub4));
			if (sub5 is null) throw new ArgumentNullException(nameof(sub5));
			if (sub6 is null) throw new ArgumentNullException(nameof(sub6));
			return new Protocol<S0, Cons<S0, Cons<S1, Cons<S2, Cons<S3, Cons<S4, Cons<S5, Cons<S6, Nil>>>>>>>, Z0, Cons<Z0, Cons<Z1, Cons<Z2, Cons<Z3, Cons<Z4, Cons<Z5, Cons<Z6, Nil>>>>>>>>();
		}

		public static Protocol<S0, Cons<S0, Cons<S1, Cons<S2, Cons<S3, Cons<S4, Cons<S5, Cons<S6, Cons<S7, Nil>>>>>>>>, Z0, Cons<Z0, Cons<Z1, Cons<Z2, Cons<Z3, Cons<Z4, Cons<Z5, Cons<Z6, Cons<Z7, Nil>>>>>>>>> Array<S0, S1, S2, S3, S4, S5, S6, S7, Z0, Z1, Z2, Z3, Z4, Z5, Z6, Z7>(Protocol<S0, S0, Z0, Z0> sub0, Protocol<S1, S1, Z1, Z1> sub1, Protocol<S2, S2, Z2, Z2> sub2, Protocol<S3, S3, Z3, Z3> sub3, Protocol<S4, S4, Z4, Z4> sub4, Protocol<S5, S5, Z5, Z5> sub5, Protocol<S6, S6, Z6, Z6> sub6, Protocol<S7, S7, Z7, Z7> sub7) where S0 : SessionType where S1 : SessionType where S2 : SessionType where Z0 : SessionType where Z1 : SessionType where Z2 : SessionType where S3 : SessionType where Z3 : SessionType where S4 : SessionType where Z4 : SessionType where S5 : SessionType where Z5 : SessionType where S6 : SessionType where Z6 : SessionType where S7 : SessionType where Z7 : SessionType
		{
			if (sub0 is null) throw new ArgumentNullException(nameof(sub0));
			if (sub1 is null) throw new ArgumentNullException(nameof(sub1));
			if (sub2 is null) throw new ArgumentNullException(nameof(sub2));
			if (sub3 is null) throw new ArgumentNullException(nameof(sub3));
			if (sub4 is null) throw new ArgumentNullException(nameof(sub4));
			if (sub5 is null) throw new ArgumentNullException(nameof(sub5));
			if (sub6 is null) throw new ArgumentNullException(nameof(sub6));
			if (sub7 is null) throw new ArgumentNullException(nameof(sub7));
			return new Protocol<S0, Cons<S0, Cons<S1, Cons<S2, Cons<S3, Cons<S4, Cons<S5, Cons<S6, Cons<S7, Nil>>>>>>>>, Z0, Cons<Z0, Cons<Z1, Cons<Z2, Cons<Z3, Cons<Z4, Cons<Z5, Cons<Z6, Cons<Z7, Nil>>>>>>>>>();
		}

		public static Protocol<S0, Cons<S0, Cons<S1, Cons<S2, Cons<S3, Cons<S4, Cons<S5, Cons<S6, Cons<S7, Cons<S8, Nil>>>>>>>>>, Z0, Cons<Z0, Cons<Z1, Cons<Z2, Cons<Z3, Cons<Z4, Cons<Z5, Cons<Z6, Cons<Z7, Cons<Z8, Nil>>>>>>>>>> Array<S0, S1, S2, S3, S4, S5, S6, S7, S8, Z0, Z1, Z2, Z3, Z4, Z5, Z6, Z7, Z8>(Protocol<S0, S0, Z0, Z0> sub0, Protocol<S1, S1, Z1, Z1> sub1, Protocol<S2, S2, Z2, Z2> sub2, Protocol<S3, S3, Z3, Z3> sub3, Protocol<S4, S4, Z4, Z4> sub4, Protocol<S5, S5, Z5, Z5> sub5, Protocol<S6, S6, Z6, Z6> sub6, Protocol<S7, S7, Z7, Z7> sub7, Protocol<S8, S8, Z8, Z8> sub8) where S0 : SessionType where S1 : SessionType where S2 : SessionType where Z0 : SessionType where Z1 : SessionType where Z2 : SessionType where S3 : SessionType where Z3 : SessionType where S4 : SessionType where Z4 : SessionType where S5 : SessionType where Z5 : SessionType where S6 : SessionType where Z6 : SessionType where S7 : SessionType where Z7 : SessionType where S8 : SessionType where Z8 : SessionType
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
			return new Protocol<S0, Cons<S0, Cons<S1, Cons<S2, Cons<S3, Cons<S4, Cons<S5, Cons<S6, Cons<S7, Cons<S8, Nil>>>>>>>>>, Z0, Cons<Z0, Cons<Z1, Cons<Z2, Cons<Z3, Cons<Z4, Cons<Z5, Cons<Z6, Cons<Z7, Cons<Z8, Nil>>>>>>>>>>();
		}

		public static Protocol<S0, Cons<S0, Cons<S1, Cons<S2, Cons<S3, Cons<S4, Cons<S5, Cons<S6, Cons<S7, Cons<S8, Cons<S9, Nil>>>>>>>>>>, Z0, Cons<Z0, Cons<Z1, Cons<Z2, Cons<Z3, Cons<Z4, Cons<Z5, Cons<Z6, Cons<Z7, Cons<Z8, Cons<Z9, Nil>>>>>>>>>>> Array<S0, S1, S2, S3, S4, S5, S6, S7, S8, S9, Z0, Z1, Z2, Z3, Z4, Z5, Z6, Z7, Z8, Z9>(Protocol<S0, S0, Z0, Z0> sub0, Protocol<S1, S1, Z1, Z1> sub1, Protocol<S2, S2, Z2, Z2> sub2, Protocol<S3, S3, Z3, Z3> sub3, Protocol<S4, S4, Z4, Z4> sub4, Protocol<S5, S5, Z5, Z5> sub5, Protocol<S6, S6, Z6, Z6> sub6, Protocol<S7, S7, Z7, Z7> sub7, Protocol<S8, S8, Z8, Z8> sub8, Protocol<S9, S9, Z9, Z9> sub9) where S0 : SessionType where S1 : SessionType where S2 : SessionType where Z0 : SessionType where Z1 : SessionType where Z2 : SessionType where S3 : SessionType where Z3 : SessionType where S4 : SessionType where Z4 : SessionType where S5 : SessionType where Z5 : SessionType where S6 : SessionType where Z6 : SessionType where S7 : SessionType where Z7 : SessionType where S8 : SessionType where Z8 : SessionType where S9 : SessionType where Z9 : SessionType
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
			return new Protocol<S0, Cons<S0, Cons<S1, Cons<S2, Cons<S3, Cons<S4, Cons<S5, Cons<S6, Cons<S7, Cons<S8, Cons<S9, Nil>>>>>>>>>>, Z0, Cons<Z0, Cons<Z1, Cons<Z2, Cons<Z3, Cons<Z4, Cons<Z5, Cons<Z6, Cons<Z7, Cons<Z8, Cons<Z9, Nil>>>>>>>>>>>();
		}
	}
}
