namespace SessionTypes.Binary
{
	public static class BinaryInterfaceHelper
	{
		public static Session<S, P> Let<S, P, T>(this Session<S, P> session, out T variable, T value) where S : SessionType where P : ProtocolType
		{
			variable = value;
			return session;
		}

		public static Session<S, P> Bind<S, P, T>(this (Session<S, P> session, T value) received, out T variable) where S : SessionType where P : ProtocolType
		{
			variable = received.value;
			return received.session;
		}
	}
}
