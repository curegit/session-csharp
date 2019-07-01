using System;
using System.Threading.Tasks;

namespace SessionTypes.Binary
{
	public static class BinarySessionInterface
	{
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

		public static async Task<Client<S, P>> SendAsync<S, P, T>(this Client<Req<T, S>, P> client, T value) where S : SessionType where P : ProtocolType
		{
			await client.SendAsync(value);
			return new Client<S, P>(client);
		}

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

		public static (Client<S, P>, T) Receive<S, P, T>(this Client<Resp<T, S>, P> client) where S : SessionType where P : ProtocolType
		{
			return (new Client<S, P>(client), client.Receive<T>());
		}

		public static (Server<S, P>, T) Receive<S, P, T>(this Server<Req<T, S>, P> server) where S : SessionType where P : ProtocolType
		{
			return (new Server<S, P>(server), server.Receive<T>());
		}

		public static Client<S, P> Receive<S, P, T>(this Client<Resp<T, S>, P> client, out T value) where S : SessionType where P : ProtocolType
		{
			value = client.Receive<T>();
			return new Client<S, P>(client);
		}

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

		public static async Task<(Client<S, P>, T)> ReceiveAsync<S, P, T>(this Client<Resp<T, S>, P> client) where S : SessionType where P : ProtocolType
		{
			return (new Client<S, P>(client), await client.ReceiveAsync<T>());
		}

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

		public static Client<L, P> ChooseLeft<L, R, P>(this Client<RequestChoice<L, R>, P> client) where L : SessionType where R : SessionType where P : ProtocolType
		{
			client.Choose(BinaryChoice.Left);
			return new Client<L, P>(client);
		}

		public static Client<R, P> ChooseRight<L, R, P>(this Client<RequestChoice<L, R>, P> client) where L : SessionType where R : SessionType where P : ProtocolType
		{
			client.Choose(BinaryChoice.Right);
			return new Client<R, P>(client);
		}

		public static Server<L, P> ChooseLeft<L, R, P>(this Server<RespondChoice<L, R>, P> server) where L : SessionType where R : SessionType where P : ProtocolType
		{
			server.Choose(BinaryChoice.Left);
			return new Server<L, P>(server);
		}

		public static Server<R, P> ChooseRight<L, R, P>(this Server<RespondChoice<L, R>, P> server) where L : SessionType where R : SessionType where P : ProtocolType
		{
			server.Choose(BinaryChoice.Right);
			return new Server<R, P>(server);
		}

		public static async Task<Client<L, P>> ChooseLeftAsync<L, R, P>(this Client<RequestChoice<L, R>, P> client) where L : SessionType where R : SessionType where P : ProtocolType
		{
			await client.ChooseAsync(BinaryChoice.Left);
			return new Client<L, P>(client);
		}

		public static async Task<Client<R, P>> ChooseRightAsync<L, R, P>(this Client<RequestChoice<L, R>, P> client) where L : SessionType where R : SessionType where P : ProtocolType
		{
			await client.ChooseAsync(BinaryChoice.Right);
			return new Client<R, P>(client);
		}

		public static async Task<Server<L, P>> ChooseLeftAsync<L, R, P>(this Server<RespondChoice<L, R>, P> server) where L : SessionType where R : SessionType where P : ProtocolType
		{
			await server.ChooseAsync(BinaryChoice.Left);
			return new Server<L, P>(server);
		}

		public static async Task<Server<R, P>> ChooseRightAsync<L, R, P>(this Server<RespondChoice<L, R>, P> server) where L : SessionType where R : SessionType where P : ProtocolType
		{
			await server.ChooseAsync(BinaryChoice.Right);
			return new Server<R, P>(server);
		}

		public static void Follow<L, R, P>(this Client<RespondChoice<L, R>, P> client, Action<Client<L, P>> leftAction, Action<Client<R, P>> rightAction) where L : SessionType where R : SessionType where P : ProtocolType
		{
			switch (client.Follow())
			{
				case BinaryChoice.Left:
					leftAction(new Client<L, P>(client));
					break;
				case BinaryChoice.Right:
					rightAction(new Client<R, P>(client));
					break;
				default:
					break;
			}
		}

		public static async Task Follow<L, R, P>(this Client<RespondChoice<L, R>, P> client, Func<Client<L, P>, Task> leftAction, Action<Client<R, P>> rightAction) where L : SessionType where R : SessionType where P : ProtocolType
		{
			switch (client.Follow())
			{
				case BinaryChoice.Left:
					await leftAction(new Client<L, P>(client));
					break;
				case BinaryChoice.Right:
					rightAction(new Client<R, P>(client));
					break;
				default:
					break;
			}
		}

		public static async Task Follow<L, R, P>(this Client<RespondChoice<L, R>, P> client, Action<Client<L, P>> leftAction, Func<Client<R, P>, Task> rightAction) where L : SessionType where R : SessionType where P : ProtocolType
		{
			switch (client.Follow())
			{
				case BinaryChoice.Left:
					leftAction(new Client<L, P>(client));
					break;
				case BinaryChoice.Right:
					await rightAction(new Client<R, P>(client));
					break;
				default:
					break;
			}
		}

		public static async Task Follow<L, R, P>(this Client<RespondChoice<L, R>, P> client, Func<Client<L, P>, Task> leftAction, Func<Client<R, P>, Task> rightAction) where L : SessionType where R : SessionType where P : ProtocolType
		{
			switch (client.Follow())
			{
				case BinaryChoice.Left:
					await leftAction(new Client<L, P>(client));
					break;
				case BinaryChoice.Right:
					await rightAction(new Client<R, P>(client));
					break;
				default:
					break;
			}
		}

		public static void Follow<L, R, P>(this Server<RequestChoice<L, R>, P> server, Action<Server<L, P>> leftAction, Action<Server<R, P>> rightAction) where L : SessionType where R : SessionType where P : ProtocolType
		{
			switch (server.Follow())
			{
				case BinaryChoice.Left:
					leftAction(new Server<L, P>(server));
					break;
				case BinaryChoice.Right:
					rightAction(new Server<R, P>(server));
					break;
				default:
					break;
			}
		}

		public static async Task Follow<L, R, P>(this Server<RequestChoice<L, R>, P> server, Func<Server<L, P>, Task> leftAction, Action<Server<R, P>> rightAction) where L : SessionType where R : SessionType where P : ProtocolType
		{
			switch (server.Follow())
			{
				case BinaryChoice.Left:
					await leftAction(new Server<L, P>(server));
					break;
				case BinaryChoice.Right:
					rightAction(new Server<R, P>(server));
					break;
				default:
					break;
			}
		}

		public static async Task Follow<L, R, P>(this Server<RequestChoice<L, R>, P> server, Action<Server<L, P>> leftAction, Func<Server<R, P>, Task> rightAction) where L : SessionType where R : SessionType where P : ProtocolType
		{
			switch (server.Follow())
			{
				case BinaryChoice.Left:
					leftAction(new Server<L, P>(server));
					break;
				case BinaryChoice.Right:
					await rightAction(new Server<R, P>(server));
					break;
				default:
					break;
			}
		}

		public static async Task Follow<L, R, P>(this Server<RequestChoice<L, R>, P> server, Func<Server<L, P>, Task> leftAction, Func<Server<R, P>, Task> rightAction) where L : SessionType where R : SessionType where P : ProtocolType
		{
			switch (server.Follow())
			{
				case BinaryChoice.Left:
					await leftAction(new Server<L, P>(server));
					break;
				case BinaryChoice.Right:
					await rightAction(new Server<R, P>(server));
					break;
				default:
					break;
			}
		}

		public static async Task FollowAsync<L, R, P>(this Client<RespondChoice<L, R>, P> client, Action<Client<L, P>> leftAction, Action<Client<R, P>> rightAction) where L : SessionType where R : SessionType where P : ProtocolType
		{
			switch (await client.FollowAsync())
			{
				case BinaryChoice.Left:
					leftAction(new Client<L, P>(client));
					break;
				case BinaryChoice.Right:
					rightAction(new Client<R, P>(client));
					break;
				default:
					break;
			}
		}

		public static async Task FollowAsync<L, R, P>(this Client<RespondChoice<L, R>, P> client, Func<Client<L, P>, Task> leftAction, Action<Client<R, P>> rightAction) where L : SessionType where R : SessionType where P : ProtocolType
		{
			switch (await client.FollowAsync())
			{
				case BinaryChoice.Left:
					await leftAction(new Client<L, P>(client));
					break;
				case BinaryChoice.Right:
					rightAction(new Client<R, P>(client));
					break;
				default:
					break;
			}
		}

		public static async Task FollowAsync<L, R, P>(this Client<RespondChoice<L, R>, P> client, Action<Client<L, P>> leftAction, Func<Client<R, P>, Task> rightAction) where L : SessionType where R : SessionType where P : ProtocolType
		{
			switch (await client.FollowAsync())
			{
				case BinaryChoice.Left:
					leftAction(new Client<L, P>(client));
					break;
				case BinaryChoice.Right:
					await rightAction(new Client<R, P>(client));
					break;
				default:
					break;
			}
		}

		public static async Task FollowAsync<L, R, P>(this Client<RespondChoice<L, R>, P> client, Func<Client<L, P>, Task> leftAction, Func<Client<R, P>, Task> rightAction) where L : SessionType where R : SessionType where P : ProtocolType
		{
			switch (await client.FollowAsync())
			{
				case BinaryChoice.Left:
					await leftAction(new Client<L, P>(client));
					break;
				case BinaryChoice.Right:
					await rightAction(new Client<R, P>(client));
					break;
				default:
					break;
			}
		}

		public static async Task FollowAsync<L, R, P>(this Server<RequestChoice<L, R>, P> server, Action<Server<L, P>> leftAction, Action<Server<R, P>> rightAction) where L : SessionType where R : SessionType where P : ProtocolType
		{
			switch (await server.FollowAsync())
			{
				case BinaryChoice.Left:
					leftAction(new Server<L, P>(server));
					break;
				case BinaryChoice.Right:
					rightAction(new Server<R, P>(server));
					break;
				default:
					break;
			}
		}

		public static async Task FollowAsync<L, R, P>(this Server<RequestChoice<L, R>, P> server, Func<Server<L, P>, Task> leftAction, Action<Server<R, P>> rightAction) where L : SessionType where R : SessionType where P : ProtocolType
		{
			switch (await server.FollowAsync())
			{
				case BinaryChoice.Left:
					await leftAction(new Server<L, P>(server));
					break;
				case BinaryChoice.Right:
					rightAction(new Server<R, P>(server));
					break;
				default:
					break;
			}
		}

		public static async Task FollowAsync<L, R, P>(this Server<RequestChoice<L, R>, P> server, Action<Server<L, P>> leftAction, Func<Server<R, P>, Task> rightAction) where L : SessionType where R : SessionType where P : ProtocolType
		{
			switch (await server.FollowAsync())
			{
				case BinaryChoice.Left:
					leftAction(new Server<L, P>(server));
					break;
				case BinaryChoice.Right:
					await rightAction(new Server<R, P>(server));
					break;
				default:
					break;
			}
		}

		public static async Task FollowAsync<L, R, P>(this Server<RequestChoice<L, R>, P> server, Func<Server<L, P>, Task> leftAction, Func<Server<R, P>, Task> rightAction) where L : SessionType where R : SessionType where P : ProtocolType
		{
			switch (await server.FollowAsync())
			{
				case BinaryChoice.Left:
					await leftAction(new Server<L, P>(server));
					break;
				case BinaryChoice.Right:
					await rightAction(new Server<R, P>(server));
					break;
				default:
					break;
			}
		}

		public static Client<S, P> Enter<S, L, P>(this Client<Cons<S, L>, P> client) where S : SessionType where L : SessionList where P : ProtocolType
		{
			return new Client<S, P>(client);
		}

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

		public static Client<S, Cons<S, L>> Zero<S, L>(this Client<Jump<Zero>, Cons<S, L>> client) where S : SessionType where L : SessionList
		{
			return new Client<S, Cons<S, L>>(client);
		}

		public static Server<S, Cons<S, L>> Zero<S, L>(this Server<Jump<Zero>, Cons<S, L>> server) where S : SessionType where L : SessionList
		{
			return new Server<S, Cons<S, L>>(server);
		}

		public static void Close<P>(this Client<Eps, P> client) where P : ProtocolType
		{
			client.Close();
		}

		public static void Close<P>(this Server<Eps, P> server) where P : ProtocolType
		{
			server.Close();
		}
	}
}
