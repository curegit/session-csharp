using System;
using System.Threading;
using System.Threading.Tasks;
using System.Threading.Channels;
using System.Collections.Generic;

namespace SessionTypes.Threading.Tasks
{
	public static class TaskChannel
	{
		public static Session<S, Empty, P> Fork<S, P, Z, Q>(this Protocol<S, P, Z, Q> protocol, Action<Session<Z, Empty, Q>> threadFunc) where S : SessionType where P : ProtocolType where Z : SessionType where Q : ProtocolType
		{
			if (protocol is null) throw new ArgumentNullException(nameof(protocol));
			if (threadFunc is null) throw new ArgumentNullException(nameof(threadFunc));
			var (client, server) = ChannelFactory.CreateWithSession<S, P, Z, Q>();
			Task.Run(() => threadFunc(server));
			return client;
		}

		/*
		public static IEnumerable<Session<C, C>> DistributeTask<T, C, S, A>(this Protocol<T, C, S> protocol, Action<Session<S, S>, A> threadFunction, A[] args) where C : ProtocolType where S : ProtocolType
		{
			int n = args.Length;

			List<Task> running = new List<Task>();

			while (true)
			{
				if (running.Count < n)
				{
					var (c, s) = NewChannel<C, S>();
					var t = Task.Run(() =>
					{
						threadFunction(s, args[0]);
					});
					running.Add(t);
					yield return c;
				}
				else
				{
					Task.WaitAny(running.ToArray());
				}
			}
		}

		public static Session<C, C>[] Distribute<T, C, S, A>(this Protocol<T, C, S> protocol, Action<Session<S, S>, A> threadFunction, A[] args) where C : ProtocolType where S : ProtocolType
		{
			int n = args.Length;
			var clients = new Session<C, C>[n];
			var servers = new Session<S, S>[n];
			for (int i = 0; i < n; i++)
			{
				var threadNumber = i;
				var (c, s) = NewChannel<C, S>();
				clients[threadNumber] = c;
				servers[threadNumber] = s;
				var threadStart = new ThreadStart(() => threadFunction(servers[threadNumber], args[threadNumber]));
				var thread = new Thread(threadStart);
				thread.Start();
			}
			return clients;
		}

		public static (Session<C, C>, Session<S, S>) Pipeline<T, C, S, A>(this Protocol<T, C, S> protocol, Action<Session<S, S>, Session<C, C>, A> threadFunction, A[] args) where C : ProtocolType where S : ProtocolType
		{
			int n = args.Length + 1;
			Session<C, C>[] clients = new Session<C, C>[n];
			Session<S, S>[] servers = new Session<S, S>[n];
			for (int i = 0; i < n; i++)
			{
				var (c, s) = NewChannel<C, S>();
				clients[i] = c;
				servers[(i + 1) % n] = s;
			}
			for (int i = 1; i < n; i++)
			{
				var threadNumber = i;
				var threadStart = new ThreadStart(() => threadFunction(servers[threadNumber], clients[threadNumber], args[threadNumber - 1]));
				var thread = new Thread(threadStart);
				thread.Start();
			}
			return (clients[0], servers[0]);
		}
		*/
	}
}
