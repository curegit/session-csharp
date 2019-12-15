namespace SessionTypes
{
	public sealed class Protocol<T, C, S> where C : ProtocolType where S : ProtocolType
	{
		internal Protocol() { }

		public Protocol<T, S, C> Swapped => new Protocol<T, S, C>();
	}
}
