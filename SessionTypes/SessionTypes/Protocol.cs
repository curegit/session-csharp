namespace SessionTypes
{
	public abstract class Protocol<C, S> where C : ProtocolType where S : ProtocolType
	{
		// TODO: pretty print
		public override string ToString()
		{
			return base.ToString();
		}
	}

	public sealed class Protocol<C, S> where C : ProtocolType where S : ProtocolType
	{
		internal Protocol() { }

		public Protocol<S, C> Swapped => new Protocol<S, C>();

		public Protocol<C, S, F> UseSeri<F>()
		{
			// 方検査できん
		}

		public int A()
		{
			return Combinator.S2C(P<>, Combinator.End);
		}
	}

	public sealed class Protocol<C, S, F> : Protocol<C, S>
	{

	}
}
