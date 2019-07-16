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

	public sealed class Req<T, S> : SessionType where S : SessionType
	{
		private Req() { }
	}

	public sealed class Resp<T, S> : SessionType where S : SessionType
	{
		private Resp() { }
	}

	public sealed class ReqChoice<L, R> : SessionType where L : SessionType where R : SessionType
	{
		private ReqChoice() { }
	}

	public sealed class RespChoice<L, R> : SessionType where L : SessionType where R : SessionType
	{
		private RespChoice() { }
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
}
