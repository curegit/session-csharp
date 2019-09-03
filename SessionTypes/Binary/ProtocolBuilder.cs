namespace SessionTypes.Binary
{
	public class Proxy<T> { }

	public static class ProtocolBuilder
	{
		public static Proxy<T> Data<T>()
		{
			return null;
		}

		public static Dual<Send<T, S1>, Recv<T, S2>> C2S<T, S1, S2>(Proxy<T> type, Dual<S1, S2> dual) where S1 : SessionType where S2 : SessionType
		{
			return null;
		}

		public static Dual<Recv<T, S1>, Send<T, S2>> S2C<T, S1, S2>(Proxy<T> type, Dual<S1, S2> dual) where S1 : SessionType where S2 : SessionType
		{
			return null;
		}

		public static Dual<Eps, Eps> End()
		{
			return null;
		}
	}
}
