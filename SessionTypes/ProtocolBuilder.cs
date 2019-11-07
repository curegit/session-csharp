using System;

namespace SessionTypes
{
	public sealed class Payload<T>
	{
		private Payload() { }
	}

	public static class ProtocolBuilder
	{
		public static Payload<T> P<T>()
		{
			throw new MethodAccessException();
		}

		public static Duality<Send<T, C>, Receive<T, S>> C2S<T, C, S>(Func<Payload<T>> type, Duality<C, S> tail) where C : SessionType where S : SessionType
		{
			return tail == null ? throw new ArgumentNullException(nameof(tail)) : new Duality<Send<T, C>, Receive<T, S>>();
		}

		public static Duality<Receive<T, C>, Send<T, S>> S2C<T, C, S>(Func<Payload<T>> type, Duality<C, S> tail) where C : SessionType where S : SessionType
		{
			return tail == null ? throw new ArgumentNullException(nameof(tail)) : new Duality<Receive<T, C>, Send<T, S>>();
		}

		public static Duality<Select<CL, CR>, Follow<SL, SR>> AtC<CL, CR, SL, SR>(Duality<CL, SL> left, Duality<CR, SR> right) where CL : SessionType where CR : SessionType where SL : SessionType where SR : SessionType
		{
			return left == null ? throw new ArgumentNullException(nameof(left)) : right == null ? throw new ArgumentNullException(nameof(right)) : new Duality<Select<CL, CR>, Follow<SL, SR>>();
		}

		public static Duality<Follow<CL, CR>, Select<SL, SR>> AtS<CL, CR, SL, SR>(Duality<CL, SL> left, Duality<CR, SR> right) where CL : SessionType where CR : SessionType where SL : SessionType where SR : SessionType
		{
			return left == null ? throw new ArgumentNullException(nameof(left)) : right == null ? throw new ArgumentNullException(nameof(right)) : new Duality<Follow<CL, CR>, Select<SL, SR>>();
		}

		public static Duality<Goto0, Goto0> Goto0
		{
			get
			{
				return new Duality<Goto0, Goto0>();
			}
		}

		public static Duality<Goto1, Goto1> Goto1
		{
			get
			{
				return new Duality<Goto1, Goto1>();
			}
		}

		public static Duality<Goto2, Goto2> Goto2
		{
			get
			{
				return new Duality<Goto2, Goto2>();
			}
		}

		public static Duality<Close, Close> End
		{
			get
			{
				return new Duality<Close, Close>();
			}
		}

		public static Duality<Cons<C0, Nil>, Cons<S0, Nil>> SessionList<S0, C0>(Duality<C0, S0> duality0) where S0 : SessionType where C0 : SessionType
		{
			if (duality0 == null) throw new ArgumentNullException(nameof(duality0));
			return new Duality<Cons<C0, Nil>, Cons<S0, Nil>>();
		}

		public static Duality<Cons<C0, Cons<C1, Nil>>, Cons<S0, Cons<S1, Nil>>> SessionList<S0, S1, C0, C1>(Duality<C0, S0> duality0, Duality<C1, S1> duality1) where S0 : SessionType where S1 : SessionType where C0 : SessionType where C1 : SessionType
		{
			if (duality0 == null) throw new ArgumentNullException(nameof(duality0));
			if (duality1 == null) throw new ArgumentNullException(nameof(duality1));
			return new Duality<Cons<C0, Cons<C1, Nil>>, Cons<S0, Cons<S1, Nil>>>();
		}

		public static Duality<Cons<C0, Cons<C1, Cons<C2, Nil>>>, Cons<S0, Cons<S1, Cons<S2, Nil>>>> SessionList<S0, S1, S2, C0, C1, C2>(Duality<C0, S0> duality0, Duality<C1, S1> duality1, Duality<C2, S2> duality2) where S0 : SessionType where S1 : SessionType where S2 : SessionType where C0 : SessionType where C1 : SessionType where C2 : SessionType
		{
			if (duality0 == null) throw new ArgumentNullException(nameof(duality0));
			if (duality1 == null) throw new ArgumentNullException(nameof(duality1));
			if (duality2 == null) throw new ArgumentNullException(nameof(duality2));
			return new Duality<Cons<C0, Cons<C1, Cons<C2, Nil>>>, Cons<S0, Cons<S1, Cons<S2, Nil>>>>();
		}
	}
}
