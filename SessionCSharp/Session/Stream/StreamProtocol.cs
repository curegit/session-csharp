namespace Session.Streaming
{
	public sealed class StreamingProtocol<S, P, O, Q> where S : SessionType where P : ProtocolType where O : SessionType where Q : ProtocolType
	{
		internal ISerializer Serializer { get; private set; }

		internal IConverter Converter { get; private set; }

		internal StreamingProtocol(ISerializer serializer, IConverter? converter = null)
		{
			Serializer = serializer ?? throw new ArgumentNullException(nameof(serializer));
			Converter = converter ?? new LinearConverter();
		}

		public Protocol<S, P, O, Q> Protocol => new Protocol<S, P, O, Q>();

		public StreamingProtocol<S, P, O, Q> Swapped => new StreamingProtocol<S, P, O, Q>(Serializer, Converter);
	}

	public static class StreamingProtocol
	{
		public static StreamingProtocol<T, C, S> OnStream<T, C, S>(this Protocol<T, C, S> protocol, ISerializer<T> serializer) where C : ProtocolType where S : ProtocolType
		{
			return new StreamingProtocol<T, C, S>(serializer);
		}
	}
}
