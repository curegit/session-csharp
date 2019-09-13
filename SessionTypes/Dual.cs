namespace SessionTypes
{
	public sealed class Dual<C, S> where C : ProtocolType where S : ProtocolType
	{
		internal C client;

		internal S server;

		internal Dual() { }
	}
}
