using System;

namespace Session.Streaming
{
	public static class StreamedProtocol
	{
		public static StreamedProtocol<S, Z> OnStream<S, Z>(this Protocol<S, Z> protocol, ISerializer serializer) where S : ProtocolType where Z : ProtocolType
		{
			if (protocol is null) throw new ArgumentNullException(nameof(protocol));
			if (serializer is null) throw new ArgumentNullException(nameof(serializer));
			return new StreamedProtocol<S, Z>(serializer);
		}
	}

	public sealed class StreamedProtocol<S, Z> where S : ProtocolType where Z : ProtocolType
	{
		internal ISerializer Serializer { get; private set; }

		internal StreamedProtocol(ISerializer serializer)
		{
			Serializer = serializer;
		}

		public StreamedProtocol<Z, S> Swapped => new StreamedProtocol<Z, S>(Serializer);
	}
}
