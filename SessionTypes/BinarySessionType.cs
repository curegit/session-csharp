namespace SessionTypes.Binary
{
	public abstract class SessionType
	{
		internal SessionType() { }
	}

	public sealed class Request<T, S> : SessionType where S : SessionType
	{
		internal Request() { }
	}

	public sealed class Respond<T, S> : SessionType where S : SessionType
	{
		internal Respond() { }
	}

	public sealed class RequestChoice<L, R> : SessionType where L : SessionType where R : SessionType
	{
		internal RequestChoice() { }
	}

	public sealed class RespondChoice<L, R> : SessionType where L : SessionType where R : SessionType
	{
		internal RespondChoice() { }
	}

	public sealed class Jump<N> : SessionType where N : TypeLevelNatural
	{
		internal Jump() { }
	}

	public sealed class Close : SessionType
	{
		internal Close() { }
	}

	public abstract class SessionList : SessionType
	{
		internal SessionList() { }
	}

	public sealed class Cons<S, L> : SessionList where S : SessionType where L : SessionList
	{
		internal Cons() { }
	}

	public sealed class Nil : SessionList
	{
		internal Nil() { }
	}
}
