using System.Threading.Tasks;

namespace SessionTypes.Binary
{
	public class Proxy<T> { }

	public class Pro
	{
		public static Proxy<T> typ<T>()
		{
			return null;
		}

		public static Dual<Send<T, S1>, Recv<T, S2>> s2c<T, S1, S2>(Proxy<T> t, Dual<S1, S2> s) where S1 : SessionType where S2 : SessionType
		{
			return null;
		}
		public static Dual<Recv<T, S1>, Send<T, S2>> c2s<T, S1, S2>(Proxy<T> t, Dual<S1, S2> s) where S1 : SessionType where S2 : SessionType
		{
			return null;
		}
		public static Dual<Eps, Eps> Finish()
		{
			return null;
		}
	}
}
