using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Threading.Channels;
using System.Collections.Generic;

namespace Session.Threading
{
	public static class ThreadChannel
	{
		public static Session<S, Empty, P> ForkThread<S, P, Z, Q>(this Protocol<S, P, Z, Q> protocol, Action<Session<Z, Empty, Q>> threadFunc) where S : SessionType where P : ProtocolType where Z : SessionType where Q : ProtocolType
		{
			if (protocol is null) throw new ArgumentNullException(nameof(protocol));
			if (threadFunc is null) throw new ArgumentNullException(nameof(threadFunc));
			var (client, server) = ChannelFactory.CreateWithSession<S, P, Z, Q>();
			var threadStart = new ThreadStart(() => threadFunc(server));
			var serverThread = new Thread(threadStart);
			serverThread.Start();
			return client;
		}

		public static Session<S, Empty, P>[] Parallel<S, P, Z, Q>(this Protocol<S, P, Z, Q> protocol, int n, Action<Session<Z, Empty, Q>> threadFunction) where S : SessionType where P : ProtocolType where Z : SessionType where Q : ProtocolType
		{
			var clients = new Session<S, Empty, P>[n];
			var servers = new Session<Z, Empty, Q>[n];
			for (int i = 0; i < n; i++)
			{
				var threadNumber = i;
				var (c, s) = ChannelFactory.CreateWithSession<S, P, Z, Q>();
				clients[threadNumber] = c;
				servers[threadNumber] = s;
				var threadStart = new ThreadStart(() => threadFunction(servers[threadNumber]));
				var thread = new Thread(threadStart);
				thread.Start();
			}
			return clients;
		}


		public static IEnumerable<Session<S, Empty, P>> Parallel<S, P, Z, Q, T>(this Protocol<S, P, Z, Q> protocol, IEnumerable<T> args, Action<Session<Z, Empty, Q>, T> threadFunction) where S : SessionType where P : ProtocolType where Z : SessionType where Q : ProtocolType
		{
			var clients = new List<Session<S, Empty, P>>();
			var servers = new List<Session<Z, Empty, Q>>();
			int i = 0;
			foreach (var arg in args)
			{
				var threadNumber = i;
				var (c, s) = ChannelFactory.CreateWithSession<S, P, Z, Q>();
				clients.Add(c);
				servers.Add(s);
				var threadStart = new ThreadStart(() => threadFunction(servers[threadNumber], arg));
				var thread = new Thread(threadStart);
				thread.Start();
				i++;
			}
			return clients;
		}

		public static (Session<S, Empty, P>, Session<Z, Empty, Q>) Pipeline<S, P, Z, Q, T>(this Protocol<S, P, Z, Q> protocol, IEnumerable<T> args, Action<Session<Z, Empty, Q>, Session<S, Empty, P>, T> threadFunction) where S : SessionType where P : ProtocolType where Z : SessionType where Q : ProtocolType
		{
			var argArray = args.ToArray();
			var n = argArray.Length;
			var clients = new Session<S, Empty, P>[n];
			var servers = new Session<Z, Empty, Q>[n];
			for (int i = 0; i < n; i++)
			{
				var (c, s) = ChannelFactory.CreateWithSession<S, P, Z, Q>();
				clients[i] = c;
				servers[(i + 1) % n] = s;
			}
			for (int i = 1; i < n; i++)
			{
				var threadNumber = i;
				var threadStart = new ThreadStart(() => threadFunction(servers[threadNumber], clients[threadNumber], argArray[threadNumber - 1]));
				var thread = new Thread(threadStart);
				thread.Start();
			}
			return (clients[0], servers[0]);
		}
	}
}
