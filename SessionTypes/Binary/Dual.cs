namespace SessionTypes.Binary
{
	public sealed class Dual<C, S> where C : ProtocolType where S : ProtocolType
	{
		internal C Client;

		internal S Server;

		internal Dual() { }
	}
}
