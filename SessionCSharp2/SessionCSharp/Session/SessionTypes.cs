namespace Session
{
	public class Send<S> : Session where S : Session
	{
		internal Send() { }

		protected Send(Send<S> copy) : base(copy) { }
	}

	public class Send<T, S> : Session where S : Session
	{
		internal Send() { }

		internal protected Send(Send<T, S> copy) : base(copy) { }
	}

	public class Receive<S> : Session where S : Session
	{
		internal Receive() { }

		protected Receive(Receive<S> copy) : base(copy) { }
	}

	public class Receive<T, S> : Session where S : Session
	{
		internal Receive() { }

		protected Receive(Receive<T, S> copy) : base(copy) { }
	}

	public class Select<L, R> : Session where L : Session where R : Session
	{
		internal Select() { }

		protected Select(Select<L, R> copy) : base(copy) { }
	}

	public class Select<L, C, R> : Session where L : Session where C : Session where R : Session
	{
		internal Select() { }

		protected Select(Select<L, C, R> copy) : base(copy) { }
	}

	public class Offer<L, R> : Session where L : Session where R : Session
	{
		internal Offer() { }

		protected Offer(Offer<L, R> copy) : base(copy) { }
	}

	public class Offer<L, C, R> : Session where L : Session where C : Session where R : Session
	{
		internal Offer() { }

		protected Offer(Offer<L, C, R> copy) : base(copy) { }
	}

	/// <summary>
	/// Protocol combinator to declare a delegation of session P then continue to S. Q is dual to P
	/// </summary>
	/// <typeparam name="P">Type of delegated session(s)</typeparam>
	/// <typeparam name="Q">Dual of the delegated session</typeparam>
	/// <typeparam name="S">Continuation</typeparam>
	public class DelegSend<P, Q, S> : Session where P : Session where Q : Session where S : Session
	{
		internal DelegSend() { }

		internal protected DelegSend(DelegSend<P, Q, S> copy) : base(copy) { }
	}

	/// <summary>
	/// Protocol combinator to declare an acceptance of delegation of session P then continue to S. Q is dual to P
	/// </summary>
	/// <typeparam name="P">Type of delegated session(s)</typeparam>
	/// <typeparam name="S">Continuation</typeparam>
	public class DelegRecv<P, S> : Session where P : Session where S : Session
	{
		internal DelegRecv() { }

		internal protected DelegRecv(DelegRecv<P, S> copy) : base(copy) { }
	}

	/*
	public sealed class ThrowNewChannel<X, P, S> : SessionType where X : SessionType where P : SessionList where S : SessionType
	{
		private ThrowNewChannel() { }
	}

	public sealed class CatchNewChannel<X, P, S> : SessionType where X : SessionType where P : SessionList where S : SessionType
	{
		private CatchNewChannel() { }
	}*/

	public class Eps : Session
	{
		internal Eps() { }

		internal Eps(Session s) : base(s) { }
	}
}
