using System;

namespace Session
{
	public class Protocol<S, Z> where S : ProtocolType where Z : ProtocolType
	{
		internal Protocol() { }

		public new Protocol<Z, S> Swapped => new Protocol<Z, S>();

		/*public static Protocol<Send<S>, Receive<Z>> operator !(Protocol<S, Z> protocol)
		{
			if (protocol is null) throw new ArgumentNullException(nameof(protocol));
			return new Protocol<Send<S>, Receive<Z>>();
		}

		public static Protocol<Receive<S>, Send<Z>> operator ~(Protocol<S, Z> protocol)
		{
			if (protocol is null) throw new ArgumentNullException(nameof(protocol));
			return new Protocol<Receive<S>, Send<Z>>();
		}*/
	}
}
