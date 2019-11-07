using System;

namespace SessionTypes
{
	public class Proxy<T>
	{
		private Proxy() { }
	}

	public static class ProtocolBuilder
	{
		public static Proxy<T> P<T>()
		{
			throw new Exception();
		}

		public static Dual<Send<T, S1>, Receive<T, S2>> C2S<T, S1, S2>(Func<Proxy<T>> type, Dual<S1, S2> dual) where S1 : SessionType where S2 : SessionType
		{
			return null;
		}

		public static Dual<Receive<T, S1>, Send<T, S2>> S2C<T, S1, S2>(Func<Proxy<T>> type, Dual<S1, S2> dual) where S1 : SessionType where S2 : SessionType
		{
			return null;
		}

		public static Dual<Select<CL, CR>, Follow<SL, SR>> CC<CL, CR, SL, SR>(Dual<CL, SL> l, Dual<CR, SR> r) where CL : SessionType where CR : SessionType where SL : SessionType where SR : SessionType
		{
			return null;
		}

		public static Dual<Follow<CL, CR>, Select<SL, SR>> SS<CL, CR, SL, SR>(Dual<CL, SL> l, Dual<CR, SR> r) where CL : SessionType where CR : SessionType where SL :SessionType where SR: SessionType
		{
			return null;
		}

		public static Dual<Goto0, Goto0> Goto0()
		{
			return null;
		}

		public static Dual<Cons<C1, Nil>, Cons<S1, Nil>> SessionList<S1, C1>(Dual<C1, S1> s1) where S1 : SessionType where C1 : SessionType
		{
			return null;
		}

		public static Dual<Cons<C1, Cons<C2,Nil>>, Cons<S1, Cons<S2, Nil>>> SessionList<S1, S2, C1, C2>(Dual<C1, S1> s1, Dual<C2, S2> s2) where S2 : SessionType where S1 : SessionType where C2 : SessionType where C1 : SessionType
		{
			return null;
		}

		public static Dual<Cons<C1, Cons<C2, Cons<C3, Nil>>>, Cons<S1, Cons<S2, Cons<S3, Nil>>>> SessionList<S1, S2, S3, C1, C2, C3>(Dual<C1, S1> s1, Dual<C2, S2> s2, Dual<C3, S3> s3) where S3 : SessionType where S2 : SessionType where S1 : SessionType where C3 : SessionType where C2 : SessionType where C1: SessionType
		{
			return null;
		}

		public static Dual<Close, Close> End()
		{
			return new Dual<Close, Close>();
		}
	}
}
