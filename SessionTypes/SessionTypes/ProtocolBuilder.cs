using System;

namespace SessionTypes
{
	public sealed class Payload<T>
	{
		private Payload() { }
	}

	public static class ProtocolCombinator<T>
	{
		public static Payload<T> P<T>()
		{
			throw new MethodAccessException();
		}

		public static Protocol<T, Send<T, C>, Receive<T, S>> C2S<C, S>(Func<Payload<T>> type, Protocol<T, C, S> tail) where C : SessionType where S : SessionType
		{
			return tail == null ? throw new ArgumentNullException(nameof(tail)) : new Protocol<T, Send<T, C>, Receive<T, S>>();
		}

		public static Protocol<T, Receive<T, C>, Send<T, S>> S2C<T, C, S>(Func<Payload<T>> type, Protocol<T, C, S> tail) where C : SessionType where S : SessionType
		{
			return tail == null ? throw new ArgumentNullException(nameof(tail)) : new Protocol<T, Receive<T, C>, Send<T, S>>();
		}

		public static Protocol<T, Cast<DC, DS, C>, Accept<DS, S>> C2SAdd<DC, DS, C, S>(Protocol<T, DC, DS> d, Protocol<T, C, S> tail) where DC : SessionType where DS : SessionType where C : SessionType where S : SessionType
		{
			return new Protocol<T, Cast<DC, DS, C>, Accept<DS, S>>();
		}

		public static Protocol<T, Accept<DC, C>, Cast<DS, DC, S>> S2CAdd<DC, DS, C, S>(Protocol<T, DC, DS> d, Protocol<T, C, S> tail) where DC : SessionType where DS : SessionType where C : SessionType where S : SessionType
		{
			return new Protocol<T, Accept<DC, C>, Cast<DS, DC, S>>();
		}

		public static Protocol<T, Select<CL, CR>, Follow<SL, SR>> AtC<CL, CR, SL, SR>(Protocol<T, CL, SL> left, Protocol<T, CR, SR> right) where CL : SessionType where CR : SessionType where SL : SessionType where SR : SessionType
		{
			return left == null ? throw new ArgumentNullException(nameof(left)) : right == null ? throw new ArgumentNullException(nameof(right)) : new Protocol<T, Select<CL, CR>, Follow<SL, SR>>();
		}

		public static Protocol<T, Follow<CL, CR>, Select<SL, SR>> AtS<CL, CR, SL, SR>(Protocol<T, CL, SL> left, Protocol<T, CR, SR> right) where CL : SessionType where CR : SessionType where SL : SessionType where SR : SessionType
		{
			return left == null ? throw new ArgumentNullException(nameof(left)) : right == null ? throw new ArgumentNullException(nameof(right)) : new Protocol<T, Follow<CL, CR>, Select<SL, SR>>();
		}

		public static Protocol<T, Goto0, Goto0> Goto0
		{
			get
			{
				return new Protocol<T, Goto0, Goto0>();
			}
		}

		public static Protocol<T, Goto1, Goto1> Goto1
		{
			get
			{
				return new Protocol<T,Goto1, Goto1>();
			}
		}

		public static Protocol<T,Goto2, Goto2> Goto2
		{
			get
			{
				return new Protocol<T,Goto2, Goto2>();
			}
		}

		public static Protocol<T,End, End> End
		{
			get
			{
				return new Protocol<T,End, End>();
			}
		}

		public static Protocol<T,Cons<C0, Nil>, Cons<S0, Nil>> SessionList<S0, C0>(Protocol<T,C0, S0> duality0) where S0 : SessionType where C0 : SessionType
		{
			if (duality0 == null) throw new ArgumentNullException(nameof(duality0));
			return new Protocol<T,Cons<C0, Nil>, Cons<S0, Nil>>();
		}

		public static Protocol<T,Cons<C0, Cons<C1, Nil>>, Cons<S0, Cons<S1, Nil>>> SessionList<S0, S1, C0, C1>(Protocol<T,C0, S0> duality0, Protocol<T,C1, S1> duality1) where S0 : SessionType where S1 : SessionType where C0 : SessionType where C1 : SessionType
		{
			if (duality0 == null) throw new ArgumentNullException(nameof(duality0));
			if (duality1 == null) throw new ArgumentNullException(nameof(duality1));
			return new Protocol<T,Cons<C0, Cons<C1, Nil>>, Cons<S0, Cons<S1, Nil>>>();
		}

		public static Protocol<T,Cons<C0, Cons<C1, Cons<C2, Nil>>>, Cons<S0, Cons<S1, Cons<S2, Nil>>>> SessionList<S0, S1, S2, C0, C1, C2>(Protocol<T,C0, S0> duality0, Protocol<T,C1, S1> duality1, Protocol<T,C2, S2> duality2) where S0 : SessionType where S1 : SessionType where S2 : SessionType where C0 : SessionType where C1 : SessionType where C2 : SessionType
		{
			if (duality0 == null) throw new ArgumentNullException(nameof(duality0));
			if (duality1 == null) throw new ArgumentNullException(nameof(duality1));
			if (duality2 == null) throw new ArgumentNullException(nameof(duality2));
			return new Protocol<T,Cons<C0, Cons<C1, Cons<C2, Nil>>>, Cons<S0, Cons<S1, Cons<S2, Nil>>>>();
		}
	}
}
