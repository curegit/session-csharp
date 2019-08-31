namespace SessionTypes.Binary
{
	public static class BinarySessionInterfaceHelper
	{
		public static S Let<S, T>(this S session, out T variable, T value) where S : BinarySession
		{
			variable = value;
			return session;
		}

		public static S Bind<S, T>(this (S session, T value) received, out T variable) where S : BinarySession
		{
			variable = received.value;
			return received.session;
		}
	}
}
