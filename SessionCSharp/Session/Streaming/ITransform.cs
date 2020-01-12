using System.IO;

namespace Session.Streaming
{
	public interface ITransform
	{
		public Stream DecorateIncomingStream(Stream incomingStream);

		public Stream DecorateOutgoingStream(Stream outgoingStream);
	}
}
