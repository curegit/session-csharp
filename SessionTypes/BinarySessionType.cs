namespace SessionTypes.Binary
{
	public abstract class SessionType
	{
		private protected SessionType() { }
	}

	public sealed class Request<T, S> : SessionType where S : SessionType
	{
		private Request() { }
	}

	public sealed class Respond<T, S> : SessionType where S : SessionType
	{
		private Respond() { }
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

	public sealed class Close : SessionType
	{
		private Close() { }
	}

	public abstract class SessionList : SessionType
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
