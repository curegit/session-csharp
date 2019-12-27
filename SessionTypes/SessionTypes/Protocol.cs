namespace SessionTypes
{
	public sealed class Protocol<S, P, X, Q> where S : SessionType where P : ProtocolType where X : SessionType where Q : ProtocolType
	{
		internal Protocol() { }

		public Protocol<X, Q, S, P> Swapped => new Protocol<X, Q, S, P>();
	}
}
