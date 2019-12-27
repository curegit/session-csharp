namespace SessionTypes
{
	public abstract class ProtocolType
	{
		private protected ProtocolType() { }
	}

	public abstract class SessionType : ProtocolType
	{
		private protected SessionType() { }
	}

	public sealed class Send<S> : SessionType where S : SessionType
	{
		private Send() { }
	}

	public sealed class Send<T, S> : SessionType where S : SessionType
	{
		private Send() { }
	}

	public sealed class Receive<S> : SessionType where S : SessionType
	{
		private Receive() { }
	}

	public sealed class Receive<T, S> : SessionType where S : SessionType
	{
		private Receive() { }
	}

	public sealed class Select<L, R> : SessionType where L : SessionType where R : SessionType
	{
		private Select() { }
	}

	public sealed class Select<L, C, R> : SessionType where L : SessionType where C : SessionType where R : SessionType
	{
		private Select() { }
	}

	public sealed class Follow<L, R> : SessionType where L : SessionType where R : SessionType
	{
		private Follow() { }
	}

	public sealed class Follow<L, C, R> : SessionType where L : SessionType where C : SessionType where R : SessionType
	{
		private Follow() { }
	}

	public sealed class Call0 : SessionType
	{
		private Call0() { }
	}

	public sealed class Call0<S> : SessionType where S : SessionType
	{
		private Call0() { }
	}

	public sealed class Call1 : SessionType
	{
		private Call1() { }
	}

	public sealed class Call1<S> : SessionType where S : SessionType
	{
		private Call1() { }
	}

	public sealed class Call2 : SessionType
	{
		private Call2() { }
	}

	public sealed class Call2<S> : SessionType where S : SessionType
	{
		private Call2() { }
	}

	public sealed class Eps : SessionType
	{
		private Eps() { }
	}

	public abstract class SessionStack
	{
		private protected SessionStack() { }
	}

	public sealed class Push<F, E> : SessionStack where F : SessionType where E : SessionStack
	{
		private Push() { }
	}

	public sealed class Any : SessionStack
	{
		private Any() { }
	}

	public sealed class Empty : SessionStack
	{
		private Empty() { }
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
}
