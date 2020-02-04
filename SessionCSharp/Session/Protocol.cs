namespace Session
{
	public class Protocol<S, Z> where S : ProtocolType where Z : ProtocolType
	{
		internal Protocol() { }

		public Protocol<Z, S> Swapped => new Protocol<Z, S>();
	}
}
