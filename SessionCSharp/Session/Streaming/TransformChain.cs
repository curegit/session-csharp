using System.IO;

namespace Session.Streaming
{

	public sealed class TransformChain : ITransform
	{
		public TransformChain(/*ITransform[] transforms*/)
		{

		}

		public Stream DecorateIncomingStream(Stream readableStream)
		{
			return readableStream;
		}

		public Stream DecorateOutgoingStream(Stream writableStream)
		{
			return writableStream;
		}
	}

}
