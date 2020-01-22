using System;

namespace Session.Streaming
{
	public static class StreamedProtocol
	{
		public static StreamedProtocol<S, P, Z, Q> OnStream<S, P, Z, Q>(this Protocol<S, P, Z, Q> protocol, ISerializer serializer) where S : SessionType where P : ProtocolType where Z : SessionType where Q : ProtocolType
		{
			if (protocol is null) throw new ArgumentNullException(nameof(protocol));
			if (serializer is null) throw new ArgumentNullException(nameof(serializer));
			//if (transforms is null) throw new ArgumentNullException(nameof(transforms));
			return new StreamedProtocol<S, P, Z, Q>(serializer, null);
		}
	}

	public sealed class StreamedProtocol<S, P, Z, Q> where S : SessionType where P : ProtocolType where Z : SessionType where Q : ProtocolType
	{
		internal ISerializer Serializer { get; private set; }

		internal ITransform Transform { get; private set; }

		internal StreamedProtocol(ISerializer serializer, ITransform transform)
		{
			Serializer = serializer;
			Transform = transform;
		}

		public Protocol<S, P, Z, Q> BaseProtocol => new Protocol<S, P, Z, Q>();

		public StreamedProtocol<S, P, Z, Q> Swapped => new StreamedProtocol<S, P, Z, Q>(Serializer, Transform);
	}
}
