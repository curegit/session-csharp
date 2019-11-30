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

	public sealed class Send<T, S> : SessionType where S : SessionType
	{
		private Send() { }
	}

	public sealed class Receive<T, S> : SessionType where S : SessionType
	{
		private Receive() { }
	}

	public sealed class AddSend<P, O, S> : SessionType where P : ProtocolType where O : ProtocolType where S : SessionType
	{
		private AddSend() { }
	}

	public sealed class AddReceive<P, S> : SessionType where P : ProtocolType where S : SessionType
	{
		private AddReceive() { }
	}

	public sealed class Select<L, R> : SessionType where L : SessionType where R : SessionType
	{
		private Select() { }
	}

	public sealed class Follow<L, R> : SessionType where L : SessionType where R : SessionType
	{
		private Follow() { }
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

	public sealed class Close : SessionType
	{
		private Close() { }
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
