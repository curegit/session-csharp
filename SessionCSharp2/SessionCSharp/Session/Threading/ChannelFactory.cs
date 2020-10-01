using System.Threading.Channels;

namespace Session.Threading
{
	internal static class ChannelFactory
	{
		public static (ChannelCommunicator client, ChannelCommunicator server) Create()
		{
			var options = new UnboundedChannelOptions
			{
				SingleReader = true,
				SingleWriter = true,
			};
			var upstream = Channel.CreateUnbounded<object>(options);
			var downstream = Channel.CreateUnbounded<object>(options);
			
			return (new ChannelCommunicator(downstream.Reader, upstream.Writer), new ChannelCommunicator(upstream.Reader, downstream.Writer));
		}

		public static (S client, Z server) CreateWithSession<S, Z>() where S : Session, new() where Z : Session, new()
		{
			var (client, server) = Create();
			return (Session.Create<S>(client), Session.Create<Z>(server));
		}
	}
}
