using System;

namespace SessionTypes
{
	public sealed class Payload<T>
	{
		private Payload() { }
	}

	public static class Combinator<S>
	{
		public static Payload<T> S<T>()
		{
			throw new MethodAccessException();
		}
	}

	public static class Combinator
	{
		public static Payload<T> P<T>()
		{
			throw new MethodAccessException();
		}

		public static Protocol<Send<T, C>, Receive<T, S>> C2S<T, C, S>(Func<Payload<T>> type, Protocol<C, S> tail) where C : SessionType where S : SessionType
		{
			return tail == null ? throw new ArgumentNullException(nameof(tail)) : new Protocol<Send<T, C>, Receive<T, S>>();
		}

		public static Protocol<Receive<T, C>, Send<T, S>> S2C<T, C, S>(Func<Payload<T>> type, Protocol<C, S> tail) where C : SessionType where S : SessionType
		{
			return tail == null ? throw new ArgumentNullException(nameof(tail)) : new Protocol<Receive<T, C>, Send<T, S>>();
		}

		public static Protocol<AddSend<DC, DS, C>, AddReceive<DS, S>> C2SAdd<DC, DS, C, S>(Protocol<DC, DS> d, Protocol<C, S> tail) where DC : SessionType where DS : SessionType where C : SessionType where S : SessionType
		{
			return new Protocol<AddSend<DC, DS, C>, AddReceive<DS, S>>();
		}

		public static Protocol<AddReceive<DC, C>, AddSend<DS, DC, S>> S2CAdd<DC, DS, C, S>(Protocol<DC, DS> d, Protocol<C, S> tail) where DC : SessionType where DS : SessionType where C : SessionType where S : SessionType
		{
			return new Protocol<AddReceive<DC, C>, AddSend<DS, DC, S>>();
		}

		public static Protocol<Select<CL, CR>, Follow<SL, SR>> AtC<CL, CR, SL, SR>(Protocol<CL, SL> left, Protocol<CR, SR> right) where CL : SessionType where CR : SessionType where SL : SessionType where SR : SessionType
		{
			return left == null ? throw new ArgumentNullException(nameof(left)) : right == null ? throw new ArgumentNullException(nameof(right)) : new Protocol<Select<CL, CR>, Follow<SL, SR>>();
		}

		public static Protocol<Follow<CL, CR>, Select<SL, SR>> AtS<CL, CR, SL, SR>(Protocol<CL, SL> left, Protocol<CR, SR> right) where CL : SessionType where CR : SessionType where SL : SessionType where SR : SessionType
		{
			return left == null ? throw new ArgumentNullException(nameof(left)) : right == null ? throw new ArgumentNullException(nameof(right)) : new Protocol<Follow<CL, CR>, Select<SL, SR>>();
		}

		public static Protocol<Goto0, Goto0> Goto0
		{
			get
			{
				return new Protocol<Goto0, Goto0>();
			}
		}

		public static Protocol<Goto1, Goto1> Goto1
		{
			get
			{
				return new Protocol<Goto1, Goto1>();
			}
		}

		public static Protocol<Goto2, Goto2> Goto2
		{
			get
			{
				return new Protocol<Goto2, Goto2>();
			}
		}

		public static Protocol<Close, Close> End
		{
			get
			{
				return new Protocol<Close, Close>();
			}
		}

		public static Protocol<Cons<C0, Nil>, Cons<S0, Nil>> SessionList<S0, C0>(Protocol<C0, S0> duality0) where S0 : SessionType where C0 : SessionType
		{
			if (duality0 == null) throw new ArgumentNullException(nameof(duality0));
			return new Protocol<Cons<C0, Nil>, Cons<S0, Nil>>();
		}

		public static Protocol<Cons<C0, Cons<C1, Nil>>, Cons<S0, Cons<S1, Nil>>> SessionList<S0, S1, C0, C1>(Protocol<C0, S0> duality0, Protocol<C1, S1> duality1) where S0 : SessionType where S1 : SessionType where C0 : SessionType where C1 : SessionType
		{
			if (duality0 == null) throw new ArgumentNullException(nameof(duality0));
			if (duality1 == null) throw new ArgumentNullException(nameof(duality1));
			return new Protocol<Cons<C0, Cons<C1, Nil>>, Cons<S0, Cons<S1, Nil>>>();
		}

		public static Protocol<Cons<C0, Cons<C1, Cons<C2, Nil>>>, Cons<S0, Cons<S1, Cons<S2, Nil>>>> SessionList<S0, S1, S2, C0, C1, C2>(Protocol<C0, S0> duality0, Protocol<C1, S1> duality1, Protocol<C2, S2> duality2) where S0 : SessionType where S1 : SessionType where S2 : SessionType where C0 : SessionType where C1 : SessionType where C2 : SessionType
		{
			if (duality0 == null) throw new ArgumentNullException(nameof(duality0));
			if (duality1 == null) throw new ArgumentNullException(nameof(duality1));
			if (duality2 == null) throw new ArgumentNullException(nameof(duality2));
			return new Protocol<Cons<C0, Cons<C1, Cons<C2, Nil>>>, Cons<S0, Cons<S1, Cons<S2, Nil>>>>();
		}
	}
}
