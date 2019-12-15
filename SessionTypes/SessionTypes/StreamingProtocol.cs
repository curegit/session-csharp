namespace SessionTypes
{
	public sealed class StreamingProtocol<T, C, S> where C : ProtocolType where S : ProtocolType
	{
		public ISerializer<T> Serializer { get; }

		public IStreamChain? StreamLink { get; }

		internal StreamingProtocol(ISerializer<T> serializer)
		{
			Serializer = serializer;
		}

		internal StreamingProtocol(ISerializer<T> serializer, params IStreamChain[] chains)
		{
			Serializer = serializer;
		}

		public StreamingProtocol<T, S, C> Swapped => new StreamingProtocol<T, S, C>(Serializer);

		public Protocol<T, C, S> Protocol => new Protocol<T, C, S>();
	}

	public static class StreamingProtocol
	{
		public static StreamingProtocol<T, C, S> OnStream<T, C, S>(this Protocol<T, C, S> protocol, ISerializer<T> serializer) where C : ProtocolType where S : ProtocolType
		{
			return new StreamingProtocol<T, C, S>(serializer);
		}
	}
}
