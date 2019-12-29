using System.Threading.Channels;

namespace SessionTypes.Threading
{
	internal static class ChannelFactory
	{
		public static (ChannelCommunicator client, ChannelCommunicator server) Create()
		{
			var option = new UnboundedChannelOptions
			{
				SingleReader = true,
				SingleWriter = true,
			};
			var upstream = Channel.CreateUnbounded<object>(option);
			var downstream = Channel.CreateUnbounded<object>(option);
			return (new ChannelCommunicator(downstream.Reader, upstream.Writer), new ChannelCommunicator(upstream.Reader, downstream.Writer));
		}

		public static (Session<S, Empty, P> client, Session<Z, Empty, Q> server) CreateWithSession<S, P, Z, Q>() where S : SessionType where P : ProtocolType where Z : SessionType where Q : ProtocolType
		{
			var (client, server) = Create();
			return (new Session<S, Empty, P>(client), new Session<Z, Empty, Q>(server));
		}
	}
}
