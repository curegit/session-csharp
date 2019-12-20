namespace SessionTypes
{
	public static class SessionInterfaceHelper
	{
		public static Session<S, E, P> Let<S, E, P, T>(this Session<S, E, P> session, out T variable, T value) where S : SessionType where E : SessionStack where P : ProtocolType
		{
			variable = value;
			return session;
		}

		public static Session<S,E, P> Bind<S,E, P, T>(this (Session<S,E, P> session, T value) received, out T variable) where S : SessionType where E : SessionStack where P : ProtocolType
		{
			variable = received.value;
			return received.session;
		}
	}
}
