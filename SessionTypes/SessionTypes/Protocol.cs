namespace SessionTypes
{
	public sealed class Protocol<C, S> where C : ProtocolType where S : ProtocolType
	{
		internal Protocol() { }

		public Protocol<S, C> Swapped => new Protocol<S, C>();

		// TODO: Pretty print
		public override string ToString()
		{
			return base.ToString();
		}
	}

	public sealed class Protocol<C, S, T> where C : ProtocolType where S : ProtocolType
	{
		internal Protocol() { }

		public Protocol<S, C, T> Swapped => new Protocol<S, C, T>();

		public StreamProtocol<C, S> ForStream(IFormatter<T> formatter)
		{

		}

		// TODO: Pretty print
		public override string ToString()
		{
			return base.ToString();
		}
	}

	public sealed class StreamProtocol<C, S, T>
	{
		internal StreamProtocol() { }

		public IFormatter<T> Formatter { get; }
	}
}
