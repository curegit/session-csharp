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

	public sealed class RequestChoice<L, R> : SessionType where L : SessionType where R : SessionType
	{
		private RequestChoice() { }
	}

	public sealed class RespondChoice<L, R> : SessionType where L : SessionType where R : SessionType
	{
		private RespondChoice() { }
	}

	public sealed class Jump<N> : SessionType where N : TypeLevelNatural
	{
		private Jump() { }
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
