using System.IO;

namespace Session.Streaming
{
	public interface ITransform
	{
		public Stream DecorateIncomingStream(Stream readableStream);

		public Stream DecorateOutgoingStream(Stream writableStream);
	}
}
