using System;
using System.Threading.Tasks;

namespace SessionTypes
{
	public static class SessionInterface
	{
		public static Session<S, P> Send<S, P, T>(this Session<Send<T, S>, P> session, T value) where S : SessionType where P : ProtocolType
		{
			session.Send(value);
			return session.ToNext<S>();
		}

		/*
		public static Client<S, P> Send<S, P, T>(this Client<Req<T, S>, P> client, T value) where S : SessionType where P : ProtocolType
		{
			client.Send(value);
			return new Client<S, P>(client);
		}

		public static Server<S, P> Send<S, P, T>(this Server<Resp<T, S>, P> server, T value) where S : SessionType where P : ProtocolType
		{
			server.Send(value);
			return new Server<S, P>(server);
		}

		public static void Send<S, P, T>(this Client<Req<T, S>, P> client, T value, Action<Client<S, P>> action) where S : SessionType where P : ProtocolType
		{
			client.Send(value);
			action(new Client<S, P>(client));
		}

		public static Task Send<S, P, T>(this Client<Req<T, S>, P> client, T value, Func<Client<S, P>, Task> action) where S : SessionType where P : ProtocolType
		{
			client.Send(value);
			return action(new Client<S, P>(client));
		}

		public static void Send<S, P, T>(this Server<Resp<T, S>, P> server, T value, Action<Server<S, P>> action) where S : SessionType where P : ProtocolType
		{
			server.Send(value);
			action(new Server<S, P>(server));
		}

		public static Task Send<S, P, T>(this Server<Resp<T, S>, P> server, T value, Func<Server<S, P>, Task> action) where S : SessionType where P : ProtocolType
		{
			server.Send(value);
			return action(new Server<S, P>(server));
		}
		*/

		public static async Task<Session<S, P>> SendAsync<S, P, T>(this Session<Send<T, S>, P> session, T value) where S : SessionType where P : ProtocolType
		{
			await session.SendAsync(value);
			return session.ToNext<S>();
		}

		/*
		public static async Task<Server<S, P>> SendAsync<S, P, T>(this Server<Resp<T, S>, P> server, T value) where S : SessionType where P : ProtocolType
		{
			await server.SendAsync(value);
			return new Server<S, P>(server);
		}

		public static async Task SendAsync<S, P, T>(this Client<Req<T, S>, P> client, T value, Action<Client<S, P>> action) where S : SessionType where P : ProtocolType
		{
			await client.SendAsync(value);
			action(new Client<S, P>(client));
		}

		public static async Task SendAsync<S, P, T>(this Client<Req<T, S>, P> client, T value, Func<Client<S, P>, Task> action) where S : SessionType where P : ProtocolType
		{
			await client.SendAsync(value);
			await action(new Client<S, P>(client));
		}

		public static async Task SendAsync<S, P, T>(this Server<Resp<T, S>, P> server, T value, Action<Server<S, P>> action) where S : SessionType where P : ProtocolType
		{
			await server.SendAsync(value);
			action(new Server<S, P>(server));
		}

		public static async Task SendAsync<S, P, T>(this Server<Resp<T, S>, P> server, T value, Func<Server<S, P>, Task> action) where S : SessionType where P : ProtocolType
		{
			await server.SendAsync(value);
			await action(new Server<S, P>(server));
		}
		*/

		public static (Session<S, P>, T) Receive<S, P, T>(this Session<Recv<T, S>, P> session) where S : SessionType where P : ProtocolType
		{
			return (session.ToNext<S>(), session.Receive<T>());
		}

		/*
		public static (Server<S, P>, T) Receive<S, P, T>(this Server<Req<T, S>, P> server) where S : SessionType where P : ProtocolType
		{
			return (new Server<S, P>(server), server.Receive<T>());
		}
		*/

		public static Session<S, P> Receive<S, P, T>(this Session<Recv<T, S>, P> session, out T value) where S : SessionType where P : ProtocolType
		{
			value = session.Receive<T>();
			return session.ToNext<S>();
		}

		/*
		public static Server<S, P> Receive<S, P, T>(this Server<Req<T, S>, P> server, out T value) where S : SessionType where P : ProtocolType
		{
			value = server.Receive<T>();
			return new Server<S, P>(server);
		}

		public static void Receive<S, P, T>(this Client<Resp<T, S>, P> client, Action<Client<S, P>, T> action) where S : SessionType where P : ProtocolType
		{
			action(new Client<S, P>(client), client.Receive<T>());
		}

		public static Task Receive<S, P, T>(this Client<Resp<T, S>, P> client, Func<Client<S, P>, T, Task> action) where S : SessionType where P : ProtocolType
		{
			return action(new Client<S, P>(client), client.Receive<T>());
		}

		public static void Receive<S, P, T>(this Server<Req<T, S>, P> server, Action<Server<S, P>, T> action) where S : SessionType where P : ProtocolType
		{
			action(new Server<S, P>(server), server.Receive<T>());
		}

		public static Task Receive<S, P, T>(this Server<Req<T, S>, P> server, Func<Server<S, P>, T, Task> action) where S : SessionType where P : ProtocolType
		{
			return action(new Server<S, P>(server), server.Receive<T>());
		}
		*/

		public static async Task<(Session<S, P>, T)> ReceiveAsync<S, P, T>(this Session<Recv<T, S>, P> session) where S : SessionType where P : ProtocolType
		{
			return (session.ToNext<S>(), await session.ReceiveAsync<T>());
		}

		/*
		public static async Task<(Server<S, P>, T)> ReceiveAsync<S, P, T>(this Server<Req<T, S>, P> server) where S : SessionType where P : ProtocolType
		{
			return (new Server<S, P>(server), await server.ReceiveAsync<T>());
		}

		public static async Task ReceiveAsync<S, P, T>(this Client<Resp<T, S>, P> client, Action<Client<S, P>, T> action) where S : SessionType where P : ProtocolType
		{
			action(new Client<S, P>(client), await client.ReceiveAsync<T>());
		}

		public static async Task ReceiveAsync<S, P, T>(this Client<Resp<T, S>, P> client, Func<Client<S, P>, T, Task> action) where S : SessionType where P : ProtocolType
		{
			await action(new Client<S, P>(client), await client.ReceiveAsync<T>());
		}

		public static async Task ReceiveAsync<S, P, T>(this Server<Req<T, S>, P> server, Action<Server<S, P>, T> action) where S : SessionType where P : ProtocolType
		{
			action(new Server<S, P>(server), await server.ReceiveAsync<T>());
		}

		public static async Task ReceiveAsync<S, P, T>(this Server<Req<T, S>, P> server, Func<Server<S, P>, T, Task> action) where S : SessionType where P : ProtocolType
		{
			await action(new Server<S, P>(server), await server.ReceiveAsync<T>());
		}
		*/

		public static Session<L, P> ChooseLeft<L, R, P>(this Session<Selc<L, R>, P> session) where L : SessionType where R : SessionType where P : ProtocolType
		{
			session.Choose(Choice.Left);
			return session.ToNext<L>();
		}

		public static Session<R, P> ChooseRight<L, R, P>(this Session<Selc<L, R>, P> session) where L : SessionType where R : SessionType where P : ProtocolType
		{
			session.Choose(Choice.Right);
			return session.ToNext<R>();
		}

		/*
		public static Session<L, P> ChooseLeft<L, R, P>(this Server<RespChoice<L, R>, P> server) where L : SessionType where R : SessionType where P : ProtocolType
		{
			server.Choose(BinaryChoice.Left);
			return new Server<L, P>(server);
		}

		public static Session<R, P> ChooseRight<L, R, P>(this Server<RespChoice<L, R>, P> server) where L : SessionType where R : SessionType where P : ProtocolType
		{
			server.Choose(BinaryChoice.Right);
			return new Server<R, P>(server);
		}
		*/

		public static async Task<Session<L, P>> ChooseLeftAsync<L, R, P>(this Session<Selc<L, R>, P> session) where L : SessionType where R : SessionType where P : ProtocolType
		{
			await session.ChooseAsync(Choice.Left);
			return session.ToNext<L>();
		}

		public static async Task<Session<R, P>> ChooseRightAsync<L, R, P>(this Session<Selc<L, R>, P> session) where L : SessionType where R : SessionType where P : ProtocolType
		{
			await session.ChooseAsync(Choice.Right);
			return session.ToNext<R>();
		}

		/*
		public static async Task<Server<L, P>> ChooseLeftAsync<L, R, P>(this Server<RespChoice<L, R>, P> server) where L : SessionType where R : SessionType where P : ProtocolType
		{
			await server.ChooseAsync(BinaryChoice.Left);
			return new Server<L, P>(server);
		}

		public static async Task<Server<R, P>> ChooseRightAsync<L, R, P>(this Server<RespChoice<L, R>, P> server) where L : SessionType where R : SessionType where P : ProtocolType
		{
			await server.ChooseAsync(BinaryChoice.Right);
			return new Server<R, P>(server);
		}
		*/

		public static void Follow<L, R, P>(this Session<Foll<L, R>, P> session, Action<Session<L, P>> leftAction, Action<Session<R, P>> rightAction) where L : SessionType where R : SessionType where P : ProtocolType
		{
			switch (session.Follow())
			{
				case Choice.Left:
					leftAction(session.ToNext<L>());
					break;
				case Choice.Right:
					rightAction(session.ToNext<R>());
					break;
				default:
					throw new UnknownChoiceException();
			}
		}

		public static async Task Follow<L, R, P>(this Session<Foll<L, R>, P> session, Func<Session<L, P>, Task> leftAction, Action<Session<R, P>> rightAction) where L : SessionType where R : SessionType where P : ProtocolType
		{
			switch (session.Follow())
			{
				case Choice.Left:
					await leftAction(session.ToNext<L>());
					break;
				case Choice.Right:
					rightAction(session.ToNext<R>());
					break;
				default:
					throw new UnknownChoiceException();
			}
		}

		public static async Task Follow<L, R, P>(this Session<Foll<L, R>, P> session, Action<Session<L, P>> leftAction, Func<Session<R, P>, Task> rightAction) where L : SessionType where R : SessionType where P : ProtocolType
		{
			switch (session.Follow())
			{
				case Choice.Left:
					leftAction(session.ToNext<L>());
					break;
				case Choice.Right:
					await rightAction(session.ToNext<R>());
					break;
				default:
					throw new UnknownChoiceException();
			}
		}

		public static async Task Follow<L, R, P>(this Session<Foll<L, R>, P> session, Func<Session<L, P>, Task> leftAction, Func<Session<R, P>, Task> rightAction) where L : SessionType where R : SessionType where P : ProtocolType
		{
			switch (session.Follow())
			{
				case Choice.Left:
					await leftAction(session.ToNext<L>());
					break;
				case Choice.Right:
					await rightAction(session.ToNext<R>());
					break;
				default:
					throw new UnknownChoiceException();
			}
		}

		/*
		public static void Follow<L, R, P>(this Server<ReqChoice<L, R>, P> server, Action<Server<L, P>> leftAction, Action<Server<R, P>> rightAction) where L : SessionType where R : SessionType where P : ProtocolType
		{
			switch (server.Follow())
			{
				case Choice.Left:
					leftAction(new Server<L, P>(server));
					break;
				case Choice.Right:
					rightAction(new Server<R, P>(server));
					break;
				default:
					break;
			}
		}

		public static async Task Follow<L, R, P>(this Server<ReqChoice<L, R>, P> server, Func<Server<L, P>, Task> leftAction, Action<Server<R, P>> rightAction) where L : SessionType where R : SessionType where P : ProtocolType
		{
			switch (server.Follow())
			{
				case Choice.Left:
					await leftAction(new Server<L, P>(server));
					break;
				case Choice.Right:
					rightAction(new Server<R, P>(server));
					break;
				default:
					break;
			}
		}

		public static async Task Follow<L, R, P>(this Server<ReqChoice<L, R>, P> server, Action<Server<L, P>> leftAction, Func<Server<R, P>, Task> rightAction) where L : SessionType where R : SessionType where P : ProtocolType
		{
			switch (server.Follow())
			{
				case Choice.Left:
					leftAction(new Server<L, P>(server));
					break;
				case Choice.Right:
					await rightAction(new Server<R, P>(server));
					break;
				default:
					break;
			}
		}

		public static async Task Follow<L, R, P>(this Server<ReqChoice<L, R>, P> server, Func<Server<L, P>, Task> leftAction, Func<Server<R, P>, Task> rightAction) where L : SessionType where R : SessionType where P : ProtocolType
		{
			switch (server.Follow())
			{
				case Choice.Left:
					await leftAction(new Server<L, P>(server));
					break;
				case Choice.Right:
					await rightAction(new Server<R, P>(server));
					break;
				default:
					break;
			}
		}
		*/

		public static async Task FollowAsync<L, R, P>(this Session<Foll<L, R>, P> session, Action<Session<L, P>> leftAction, Action<Session<R, P>> rightAction) where L : SessionType where R : SessionType where P : ProtocolType
		{
			switch (await session.FollowAsync())
			{
				case Choice.Left:
					leftAction(session.ToNext<L>());
					break;
				case Choice.Right:
					rightAction(session.ToNext<R>());
					break;
				default:
					throw new UnknownChoiceException();
			}
		}

		public static async Task FollowAsync<L, R, P>(this Session<Foll<L, R>, P> session, Func<Session<L, P>, Task> leftAction, Action<Session<R, P>> rightAction) where L : SessionType where R : SessionType where P : ProtocolType
		{
			switch (await session.FollowAsync())
			{
				case Choice.Left:
					await leftAction(session.ToNext<L>());
					break;
				case Choice.Right:
					rightAction(session.ToNext<R>());
					break;
				default:
					throw new UnknownChoiceException();
			}
		}

		public static async Task FollowAsync<L, R, P>(this Session<Foll<L, R>, P> session, Action<Session<L, P>> leftAction, Func<Session<R, P>, Task> rightAction) where L : SessionType where R : SessionType where P : ProtocolType
		{
			switch (await session.FollowAsync())
			{
				case Choice.Left:
					leftAction(session.ToNext<L>());
					break;
				case Choice.Right:
					await rightAction(session.ToNext<R>());
					break;
				default:
					throw new UnknownChoiceException();
			}
		}

		public static async Task FollowAsync<L, R, P>(this Session<Foll<L, R>, P> session, Func<Session<L, P>, Task> leftAction, Func<Session<R, P>, Task> rightAction) where L : SessionType where R : SessionType where P : ProtocolType
		{
			switch (await session.FollowAsync())
			{
				case Choice.Left:
					await leftAction(session.ToNext<L>());
					break;
				case Choice.Right:
					await rightAction(session.ToNext<R>());
					break;
				default:
					throw new UnknownChoiceException();
			}
		}

		/*
		public static async Task FollowAsync<L, R, P>(this Server<ReqChoice<L, R>, P> server, Action<Server<L, P>> leftAction, Action<Server<R, P>> rightAction) where L : SessionType where R : SessionType where P : ProtocolType
		{
			switch (await server.FollowAsync())
			{
				case Choice.Left:
					leftAction(new Server<L, P>(server));
					break;
				case Choice.Right:
					rightAction(new Server<R, P>(server));
					break;
				default:
					break;
			}
		}

		public static async Task FollowAsync<L, R, P>(this Server<ReqChoice<L, R>, P> server, Func<Server<L, P>, Task> leftAction, Action<Server<R, P>> rightAction) where L : SessionType where R : SessionType where P : ProtocolType
		{
			switch (await server.FollowAsync())
			{
				case Choice.Left:
					await leftAction(new Server<L, P>(server));
					break;
				case Choice.Right:
					rightAction(new Server<R, P>(server));
					break;
				default:
					break;
			}
		}

		public static async Task FollowAsync<L, R, P>(this Server<ReqChoice<L, R>, P> server, Action<Server<L, P>> leftAction, Func<Server<R, P>, Task> rightAction) where L : SessionType where R : SessionType where P : ProtocolType
		{
			switch (await server.FollowAsync())
			{
				case Choice.Left:
					leftAction(new Server<L, P>(server));
					break;
				case Choice.Right:
					await rightAction(new Server<R, P>(server));
					break;
				default:
					break;
			}
		}

		public static async Task FollowAsync<L, R, P>(this Server<ReqChoice<L, R>, P> server, Func<Server<L, P>, Task> leftAction, Func<Server<R, P>, Task> rightAction) where L : SessionType where R : SessionType where P : ProtocolType
		{
			switch (await server.FollowAsync())
			{
				case Choice.Left:
					await leftAction(new Server<L, P>(server));
					break;
				case Choice.Right:
					await rightAction(new Server<R, P>(server));
					break;
				default:
					break;
			}
		}
		*/

		public static Session<S, P> Enter<S, L, P>(this Session<Cons<S, L>, P> session) where S : SessionType where L : SessionList where P : ProtocolType
		{
			return session.ToNext<S>();
		}

		/*
		public static Server<S, P> Enter<S, L, P>(this Server<Cons<S, L>, P> server) where S : SessionType where L : SessionList where P : ProtocolType
		{
			return new Server<S, P>(server);
		}

		public static void Enter<S, L, P>(this Client<Cons<S, L>, P> client, Action<Client<S, P>> action) where S : SessionType where L : SessionList where P : ProtocolType
		{
			action(new Client<S, P>(client));
		}

		public static Task Enter<S, L, P>(this Client<Cons<S, L>, P> client, Func<Client<S, P>, Task> action) where S : SessionType where L : SessionList where P : ProtocolType
		{
			return action(new Client<S, P>(client));
		}

		public static void Enter<S, L, P>(this Server<Cons<S, L>, P> server, Action<Server<S, P>> action) where S : SessionType where L : SessionList where P : ProtocolType
		{
			action(new Server<S, P>(server));
		}

		public static Task Enter<S, L, P>(this Server<Cons<S, L>, P> server, Func<Server<S, P>, Task> action) where S : SessionType where L : SessionList where P : ProtocolType
		{
			return action(new Server<S, P>(server));
		}
		*/

		public static Session<S, S> Jump<S>(this Session<Goto0, S> session) where S : SessionType
		{
			return session.ToNext<S>();
		}

		/*
		public static Server<S, S> Jump<S>(this Server<Goto0, S> server) where S : SessionType
		{
			return new Server<S, S>(server);
		}
		*/

		public static Session<S, Cons<S, L>> Jump<S, L>(this Session<Goto0, Cons<S, L>> session) where S : SessionType where L : SessionList
		{
			return session.ToNext<S>();
		}

		/*
		public static Server<S, Cons<S, L>> Jump<S, L>(this Server<Goto0, Cons<S, L>> server) where S : SessionType where L : SessionList
		{
			return new Server<S, Cons<S, L>>(server);
		}
		*/

		public static void Close<P>(this Session<Eps, P> session) where P : ProtocolType
		{
			session.Close();
		}

		/*
		public static void Close<P>(this Server<Eps, P> server) where P : ProtocolType
		{
			server.Close();
		}
		*/
	}
}
