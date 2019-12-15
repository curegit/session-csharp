using System;
using System.Threading;
using System.Threading.Tasks;
using System.Threading.Channels;

using System.Collections.Generic;

namespace SessionTypes.Threading
{
	public static class TcpO
	{
		public static TcpStarter<T, C> Tcp<T, C, S>(this StreamingProtocol<T, C, S> streamingProtocol) where C : ProtocolType where S : ProtocolType
		{
			return new TcpStarter<T, C>()
			{
				Serializer = streamingProtocol.Serializer,
			};
		}
	}

	public class TcpStarter<T, P> where P : ProtocolType
	{
		internal ISerializer<T> Serializer;

		public void Listen(Action<Session<P, P>> action)
		{

		}

		public Session<P, P> Connect()
		{
			return null;
		}
	}
}
