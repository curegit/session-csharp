using System.IO;

namespace Session.Streaming
{
	public interface ITransform
	{
		public Stream DecorateIncomingStream(Stream stream);

		public Stream DecorateOutgoingStream(Stream stream);
	}
}
