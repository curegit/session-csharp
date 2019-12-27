namespace SessionTypes
{
	public sealed class Protocol<S, P, O, Q> where S : SessionType where P : ProtocolType where O : SessionType where Q : ProtocolType
	{
		internal Protocol() { }

		public Protocol<O, Q, S, P> Swapped => new Protocol<O, Q, S, P>();
	}
}
