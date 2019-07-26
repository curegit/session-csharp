namespace SessionTypes.Binary
{
	public abstract class ProtocolType
	{
		private protected ProtocolType() { }
	}

	public abstract class SessionType : ProtocolType
	{
		private protected SessionType() { }
	}

	public sealed class Send<T, S> : SessionType where S : SessionType
	{
		private Send() { }
	}

	public sealed class Recv<T, S> : SessionType where S : SessionType
	{
		private Recv() { }
	}

	public sealed class Selc<L, R> : SessionType where L : SessionType where R : SessionType
	{
		private Selc() { }
	}

	public sealed class Foll<L, R> : SessionType where L : SessionType where R : SessionType
	{
		private Foll() { }
	}

	public sealed class Goto0 : SessionType
	{
		private Goto0() { }
	}

	public sealed class Goto1 : SessionType
	{
		private Goto1() { }
	}

	public sealed class Goto2 : SessionType
	{
		private Goto2() { }
	}

	public sealed class Eps : SessionType
	{
		private Eps() { }
	}

	public abstract class SessionList : ProtocolType
	{
		private protected SessionList() { }
	}

	public sealed class Cons<S, L> : SessionList where S : SessionType where L : SessionList
	{
		private Cons() { }
	}

	public sealed class Nil : SessionList
	{
		private Nil() { }
	}

	public sealed class Dual<C, S>
	{
		internal C Client;

		internal S Server;

		internal Dual() { }
	}

	public sealed class Endpoint<S, P> : BinarySession
	{
		internal Endpoint(BinarySession session) : base(session) { }

		internal Endpoint(BinaryCommunicator communicator) : base(communicator) { }
	}

	public class Proxy<T> { }

	public class Pro
	{
		public static Proxy<T> typ<T>()
		{
			return null;
		}

		public static Dual<Send<T, S1>, Recv<T, S2>> s2c<T, S1, S2>(Proxy<T> t, Dual<S1, S2> s) where S1 : SessionType where S2 : SessionType
		{
			return null;
		}
		public static Dual<Recv<T, S1>, Send<T, S2>> c2s<T, S1, S2>(Proxy<T> t, Dual<S1, S2> s) where S1 : SessionType where S2 : SessionType
		{
			return null;
		}
		public static Dual<Eps, Eps> Finish()
		{
			return null;
		}
	}
}
