namespace Session
{
	public sealed class Protocol<S, P, Z, Q> where S : SessionType where P : ProtocolType where Z : SessionType where Q : ProtocolType
	{
		internal Protocol() { }

		public Protocol<Z, Q, S, P> Swapped => new Protocol<Z, Q, S, P>();
	}
}
