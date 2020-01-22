using System;

namespace Session
{
	public class Protocol<S, P, Z, Q> where S : SessionType where P : ProtocolType where Z : SessionType where Q : ProtocolType
	{
		internal Protocol() { }

		public Protocol<Z, Q, S, P> Swapped => new Protocol<Z, Q, S, P>();
	}

	public class Protocol<S, Z> : Protocol<S, S, Z, Z> where S : SessionType where Z : SessionType
	{
		internal Protocol() { }

		public new Protocol<Z, S> Swapped => new Protocol<Z, S>();

		public static Protocol<Send<S>, Receive<Z>> operator !(Protocol<S, Z> protocol)
		{
			if (protocol is null) throw new ArgumentNullException(nameof(protocol));
			return new Protocol<Send<S>, Receive<Z>>();
		}

		public static Protocol<Receive<S>, Send<Z>> operator ~(Protocol<S, Z> protocol)
		{
			if (protocol is null) throw new ArgumentNullException(nameof(protocol));
			return new Protocol<Receive<S>, Send<Z>>();
		}
	}
}
