using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Threading.Channels;
using System.Collections.Generic;

namespace Session.Threading
{
	using static ProtocolCombinator;

	public static class ThreadChannel
	{
		public static Session<S, Empty, Cons<S, Nil>> ForkThread<S, Z>(this Protocol<S, Z> protocol, Action<Session<Z, Empty, Cons<Z, Nil>>> threadFunc) where S : SessionType where Z : SessionType
		{
			return ForkThread<S, Nil, Z, Nil>(Arrange<S, Z>(protocol), threadFunc);
		}

		public static Session<S, Empty, Cons<S, SS>> ForkThread<S, SS, Z, ZZ>(this Protocol<Cons<S, SS>, Cons<Z, ZZ>> protocol, Action<Session<Z, Empty, Cons<Z, ZZ>>> threadFunc) where S : SessionType where SS : SessionList where Z : SessionType where ZZ : SessionList
		{
			if (protocol is null) throw new ArgumentNullException(nameof(protocol));
			if (threadFunc is null) throw new ArgumentNullException(nameof(threadFunc));
			var (client, server) = ChannelFactory.CreateWithSession<S, Cons<S, SS>, Z, Cons<Z, ZZ>>();
			var threadStart = new ThreadStart(() => threadFunc(server));
			var serverThread = new Thread(threadStart);
			serverThread.Start();
			return client;
		}

		public static IEnumerable<Session<S, Empty, Cons<S, Nil>>> Parallel<S, Z>(this Protocol<S, Z> protocol, int n, Action<Session<Z, Empty, Cons<Z, Nil>>> threadFunction) where S : SessionType where Z : SessionType
		{
			return Parallel<S, Nil, Z, Nil>(Arrange<S, Z>(protocol), n, threadFunction);
		}

		public static IEnumerable<Session<S, Empty, Cons<S, SS>>> Parallel<S, SS, Z, ZZ>(this Protocol<Cons<S, SS>, Cons<Z, ZZ>> protocol, int n, Action<Session<Z, Empty, Cons<Z, ZZ>>> threadFunction) where S : SessionType where SS : SessionList where Z : SessionType where ZZ : SessionList
		{
			var clients = new Session<S, Empty, Cons<S, SS>>[n];
			var servers = new Session<Z, Empty, Cons<Z, ZZ>>[n];
			for (int i = 0; i < n; i++)
			{
				var threadNumber = i;
				var (c, s) = ChannelFactory.CreateWithSession<S, Cons<S, SS>, Z, Cons<Z, ZZ>>();
				clients[threadNumber] = c;
				servers[threadNumber] = s;
				var threadStart = new ThreadStart(() => threadFunction(servers[threadNumber]));
				var thread = new Thread(threadStart);
				thread.Start();
			}
			return clients;
		}

		public static IEnumerable<Session<S, Empty, Cons<S, Nil>>> Parallel<S, Z, T>(this Protocol<S, Z> protocol, IEnumerable<T> args, Action<Session<Z, Empty, Cons<Z, Nil>>, T> threadFunction) where S : SessionType where Z : SessionType
		{
			return Parallel<S, Nil, Z, Nil, T>(Arrange(protocol), args, threadFunction);
		}

		public static IEnumerable<Session<S, Empty, Cons<S, SS>>> Parallel<S, SS, Z, ZZ, T>(this Protocol<Cons<S, SS>, Cons<Z, ZZ>> protocol, IEnumerable<T> args, Action<Session<Z, Empty, Cons<Z, ZZ>>, T> threadFunction) where S : SessionType where SS : SessionList where Z : SessionType where ZZ : SessionList
		{
			var clients = new List<Session<S, Empty, Cons<S, SS>>>();
			var servers = new List<Session<Z, Empty, Cons<Z, ZZ>>>();
			int i = 0;
			foreach (var arg in args)
			{
				var threadNumber = i;
				var (c, s) = ChannelFactory.CreateWithSession<S, Cons<S, SS>, Z, Cons<Z, ZZ>>();
				clients.Add(c);
				servers.Add(s);
				var threadStart = new ThreadStart(() => threadFunction(servers[threadNumber], arg));
				var thread = new Thread(threadStart);
				thread.Start();
				i++;
			}
			return clients;
		}

		public static (Session<S, Empty, Cons<S, Nil>>, Session<Z, Empty, Cons<Z, Nil>>) Pipeline<S, Z, T>(this Protocol<S, Z> protocol, IEnumerable<T> args, Action<Session<Z, Empty, Cons<Z, Nil>>, Session<S, Empty, Cons<S, Nil>>, T> threadFunction) where S : SessionType where Z : SessionType
		{
			return Pipeline(Arrange(protocol), args, threadFunction);
		}

		public static (Session<S, Empty, Cons<S, SS>>, Session<Z, Empty, Cons<Z, ZZ>>) Pipeline<S, SS, Z, ZZ, T>(this Protocol<Cons<S, SS>, Cons<Z, ZZ>> protocol, IEnumerable<T> args, Action<Session<Z, Empty, Cons<Z, ZZ>>, Session<S, Empty, Cons<S, SS>>, T> threadFunction) where S : SessionType where SS : SessionList where Z : SessionType where ZZ : SessionList
		{
			var argArray = args.ToArray();
			var n = argArray.Length;
			var clients = new Session<S, Empty, Cons<S, SS>>[n];
			var servers = new Session<Z, Empty, Cons<Z, ZZ>>[n];
			for (int i = 0; i < n; i++)
			{
				var (c, s) = ChannelFactory.CreateWithSession<S, Cons<S, SS>, Z, Cons<Z, ZZ>>();
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
