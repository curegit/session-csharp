using System;
using System.Threading.Tasks;

namespace SessionTypes.Binary
{
	public static class BinarySessionInterface
	{
		public static Client<S, P> Send<S, P, T>(this Client<Request<T, S>, P> request, T value) where S : SessionType where P : SessionType
		{
			request.Send(value);
			return new Client<S, P>(request);
		}

		public static Server<S, P> Send<S, P, T>(this Server<Respond<T, S>, P> respond, T value) where S : SessionType where P : SessionType
		{
			respond.Send(value);
			return new Server<S, P>(respond);
		}

		public static void Send<S, P, T>(this Client<Request<T, S>, P> request, T value, Action<Client<S, P>> action) where S : SessionType where P : SessionType
		{
			request.Send(value);
			action(new Client<S, P>(request));
		}

		public static Task Send<S, P, T>(this Client<Request<T, S>, P> request, T value, Func<Client<S, P>, Task> action) where S : SessionType where P : SessionType
		{
			request.Send(value);
			return action(new Client<S, P>(request));
		}

		public static void Send<S, P, T>(this Server<Respond<T, S>, P> respond, T value, Action<Server<S, P>> action) where S : SessionType where P : SessionType
		{
			respond.Send(value);
			action(new Server<S, P>(respond));
		}

		public static Task Send<S, P, T>(this Server<Respond<T, S>, P> respond, T value, Func<Server<S, P>, Task> action) where S : SessionType where P : SessionType
		{
			respond.Send(value);
			return action(new Server<S, P>(respond));
		}

		public static async Task<Client<S, P>> SendAsync<S, P, T>(this Client<Request<T, S>, P> request, T value) where S : SessionType where P : SessionType
		{
			await request.SendAsync(value);
			return new Client<S, P>(request);
		}

		public static async Task<Server<S, P>> SendAsync<S, P, T>(this Server<Respond<T, S>, P> respond, T value) where S : SessionType where P : SessionType
		{
			await respond.SendAsync(value);
			return new Server<S, P>(respond);
		}

		public static async Task SendAsync<S, P, T>(this Client<Request<T, S>, P> request, T value, Action<Client<S, P>> action) where S : SessionType where P : SessionType
		{
			await request.SendAsync(value);
			action(new Client<S, P>(request));
		}

		public static async Task SendAsync<S, P, T>(this Client<Request<T, S>, P> request, T value, Func<Client<S, P>, Task> action) where S : SessionType where P : SessionType
		{
			await request.SendAsync(value);
			await action(new Client<S, P>(request));
		}

		public static async Task SendAsync<S, P, T>(this Server<Respond<T, S>, P> respond, T value, Action<Server<S, P>> action) where S : SessionType where P : SessionType
		{
			await respond.SendAsync(value);
			action(new Server<S, P>(respond));
		}

		public static async Task SendAsync<S, P, T>(this Server<Respond<T, S>, P> respond, T value, Func<Server<S, P>, Task> action) where S : SessionType where P : SessionType
		{
			await respond.SendAsync(value);
			await action(new Server<S, P>(respond));
		}

		public static (Client<S, P>, T) Receive<S, P, T>(this Client<Respond<T, S>, P> respond) where S : SessionType where P : SessionType
		{
			return (new Client<S, P>(respond), respond.Receive<T>());
		}

		public static (Server<S, P>, T) Receive<S, P, T>(this Server<Request<T, S>, P> request) where S : SessionType where P : SessionType
		{
			return (new Server<S, P>(request), request.Receive<T>());
		}

		public static Client<S, P> Receive<S, P, T>(this Client<Respond<T, S>, P> respond, out T value) where S : SessionType where P : SessionType
		{
			value = respond.Receive<T>();
			return new Client<S, P>(respond);
		}

		public static Server<S, P> Receive<S, P, T>(this Server<Request<T, S>, P> request, out T value) where S : SessionType where P : SessionType
		{
			value = request.Receive<T>();
			return new Server<S, P>(request);
		}

		public static void Receive<S, P, T>(this Client<Respond<T, S>, P> respond, Action<Client<S, P>, T> action) where S : SessionType where P : SessionType
		{
			action(new Client<S, P>(respond), respond.Receive<T>());
		}

		public static Task Receive<S, P, T>(this Client<Respond<T, S>, P> respond, Func<Client<S, P>, T, Task> action) where S : SessionType where P : SessionType
		{
			return action(new Client<S, P>(respond), respond.Receive<T>());
		}

		public static void Receive<S, P, T>(this Server<Request<T, S>, P> request, Action<Server<S, P>, T> action) where S : SessionType where P : SessionType
		{
			action(new Server<S, P>(request), request.Receive<T>());
		}

		public static Task Receive<S, P, T>(this Server<Request<T, S>, P> request, Func<Server<S, P>, T, Task> action) where S : SessionType where P : SessionType
		{
			return action(new Server<S, P>(request), request.Receive<T>());
		}

		public static async Task<(Client<S, P>, T)> ReceiveAsync<S, P, T>(this Client<Respond<T, S>, P> respond) where S : SessionType where P : SessionType
		{
			return (new Client<S, P>(respond), await respond.ReceiveAsync<T>());
		}

		public static async Task<(Server<S, P>, T)> ReceiveAsync<S, P, T>(this Server<Request<T, S>, P> request) where S : SessionType where P : SessionType
		{
			return (new Server<S, P>(request), await request.ReceiveAsync<T>());
		}

		public static async Task ReceiveAsync<S, P, T>(this Client<Respond<T, S>, P> respond, Action<Client<S, P>, T> action) where S : SessionType where P : SessionType
		{
			action(new Client<S, P>(respond), await respond.ReceiveAsync<T>());
		}

		public static async Task ReceiveAsync<S, P, T>(this Client<Respond<T, S>, P> respond, Func<Client<S, P>, T, Task> action) where S : SessionType where P : SessionType
		{
			await action(new Client<S, P>(respond), await respond.ReceiveAsync<T>());
		}

		public static async Task ReceiveAsync<S, P, T>(this Server<Request<T, S>, P> request, Action<Server<S, P>, T> action) where S : SessionType where P : SessionType
		{
			action(new Server<S, P>(request), await request.ReceiveAsync<T>());
		}

		public static async Task ReceiveAsync<S, P, T>(this Server<Request<T, S>, P> request, Func<Server<S, P>, T, Task> action) where S : SessionType where P : SessionType
		{
			await action(new Server<S, P>(request), await request.ReceiveAsync<T>());
		}

		public static Client<L, P> ChooseLeft<L, R, P>(this Client<RequestChoice<L, R>, P> requestChoice) where L : SessionType where R : SessionType where P : SessionType
		{
			requestChoice.Choose(BinaryChoice.Left);
			return new Client<L, P>(requestChoice);
		}

		public static Client<R, P> ChooseRight<L, R, P>(this Client<RequestChoice<L, R>, P> requestChoice) where L : SessionType where R : SessionType where P : SessionType
		{
			requestChoice.Choose(BinaryChoice.Right);
			return new Client<R, P>(requestChoice);
		}

		public static Server<L, P> ChooseLeft<L, R, P>(this Server<RespondChoice<L, R>, P> respondChoice) where L : SessionType where R : SessionType where P : SessionType
		{
			respondChoice.Choose(BinaryChoice.Left);
			return new Server<L, P>(respondChoice);
		}

		public static Server<R, P> ChooseRight<L, R, P>(this Server<RespondChoice<L, R>, P> respondChoice) where L : SessionType where R : SessionType where P : SessionType
		{
			respondChoice.Choose(BinaryChoice.Right);
			return new Server<R, P>(respondChoice);
		}

		public static async Task<Client<L, P>> ChooseLeftAsync<L, R, P>(this Client<RequestChoice<L, R>, P> requestChoice) where L : SessionType where R : SessionType where P : SessionType
		{
			await requestChoice.ChooseAsync(BinaryChoice.Left);
			return new Client<L, P>(requestChoice);
		}

		public static async Task<Client<R, P>> ChooseRightAsync<L, R, P>(this Client<RequestChoice<L, R>, P> requestChoice) where L : SessionType where R : SessionType where P : SessionType
		{
			await requestChoice.ChooseAsync(BinaryChoice.Right);
			return new Client<R, P>(requestChoice);
		}

		public static async Task<Server<L, P>> ChooseLeftAsync<L, R, P>(this Server<RespondChoice<L, R>, P> respondChoice) where L : SessionType where R : SessionType where P : SessionType
		{
			await respondChoice.ChooseAsync(BinaryChoice.Left);
			return new Server<L, P>(respondChoice);
		}

		public static async Task<Server<R, P>> ChooseRightAsync<L, R, P>(this Server<RespondChoice<L, R>, P> respondChoice) where L : SessionType where R : SessionType where P : SessionType
		{
			await respondChoice.ChooseAsync(BinaryChoice.Right);
			return new Server<R, P>(respondChoice);
		}

		public static async Task FollowAsync<L, R, P>(this Client<RespondChoice<L, R>, P> respondChoice, Action<Client<L, P>> leftAction, Action<Client<R, P>> rightAction) where L : SessionType where R : SessionType where P : SessionType
		{
			switch (await respondChoice.FollowAsync())
			{
				case BinaryChoice.Left:
					leftAction(new Client<L, P>(respondChoice));
					break;
				case BinaryChoice.Right:
					rightAction(new Client<R, P>(respondChoice));
					break;
				default:
					break;
			}
		}

		public static async Task FollowAsync<L, R, P>(this Client<RespondChoice<L, R>, P> respondChoice, Func<Client<L, P>, Task> leftAction, Action<Client<R, P>> rightAction) where L : SessionType where R : SessionType where P : SessionType
		{
			switch (await respondChoice.FollowAsync())
			{
				case BinaryChoice.Left:
					await leftAction(new Client<L, P>(respondChoice));
					break;
				case BinaryChoice.Right:
					rightAction(new Client<R, P>(respondChoice));
					break;
				default:
					break;
			}
		}

		public static async Task FollowAsync<L, R, P>(this Client<RespondChoice<L, R>, P> respondChoice, Action<Client<L, P>> leftAction, Func<Client<R, P>, Task> rightAction) where L : SessionType where R : SessionType where P : SessionType
		{
			switch (await respondChoice.FollowAsync())
			{
				case BinaryChoice.Left:
					leftAction(new Client<L, P>(respondChoice));
					break;
				case BinaryChoice.Right:
					await rightAction(new Client<R, P>(respondChoice));
					break;
				default:
					break;
			}
		}

		public static async Task FollowAsync<L, R, P>(this Client<RespondChoice<L, R>, P> respondChoice, Func<Client<L, P>, Task> leftAction, Func<Client<R, P>, Task> rightAction) where L : SessionType where R : SessionType where P : SessionType
		{
			switch (await respondChoice.FollowAsync())
			{
				case BinaryChoice.Left:
					await leftAction(new Client<L, P>(respondChoice));
					break;
				case BinaryChoice.Right:
					await rightAction(new Client<R, P>(respondChoice));
					break;
				default:
					break;
			}
		}

		public static async Task FollowAsync<L, R, P>(this Server<RequestChoice<L, R>, P> requestChoice, Action<Server<L, P>> leftAction, Action<Server<R, P>> rightAction) where L : SessionType where R : SessionType where P : SessionType
		{
			switch (await requestChoice.FollowAsync())
			{
				case BinaryChoice.Left:
					leftAction(new Server<L, P>(requestChoice));
					break;
				case BinaryChoice.Right:
					rightAction(new Server<R, P>(requestChoice));
					break;
				default:
					break;
			}
		}

		public static async Task FollowAsync<L, R, P>(this Server<RequestChoice<L, R>, P> requestChoice, Func<Server<L, P>, Task> leftAction, Action<Server<R, P>> rightAction) where L : SessionType where R : SessionType where P : SessionType
		{
			switch (await requestChoice.FollowAsync())
			{
				case BinaryChoice.Left:
					await leftAction(new Server<L, P>(requestChoice));
					break;
				case BinaryChoice.Right:
					rightAction(new Server<R, P>(requestChoice));
					break;
				default:
					break;
			}
		}

		public static async Task FollowAsync<L, R, P>(this Server<RequestChoice<L, R>, P> requestChoice, Action<Server<L, P>> leftAction, Func<Server<R, P>, Task> rightAction) where L : SessionType where R : SessionType where P : SessionType
		{
			switch (await requestChoice.FollowAsync())
			{
				case BinaryChoice.Left:
					leftAction(new Server<L, P>(requestChoice));
					break;
				case BinaryChoice.Right:
					await rightAction(new Server<R, P>(requestChoice));
					break;
				default:
					break;
			}
		}

		public static async Task FollowAsync<L, R, P>(this Server<RequestChoice<L, R>, P> requestChoice, Func<Server<L, P>, Task> leftAction, Func<Server<R, P>, Task> rightAction) where L : SessionType where R : SessionType where P : SessionType
		{
			switch (await requestChoice.FollowAsync())
			{
				case BinaryChoice.Left:
					await leftAction(new Server<L, P>(requestChoice));
					break;
				case BinaryChoice.Right:
					await rightAction(new Server<R, P>(requestChoice));
					break;
				default:
					break;
			}
		}

		public static Server<S, P> Enter<S, B, P>(this Server<Cons<S, B>, P> label) where S : SessionType where B : SessionList where P : SessionType
		{
			return new Server<S, P>(label);
		}

		public static Server<S, Cons<S, B>> Zero<S, B>(this Server<Jump<Zero>, Cons<S, B>> jump) where S : SessionType where B : SessionList
		{
			return new Server<S, Cons<S, B>>(jump);
		}

		public static Client<S, P> Enter<S, B, P>(this Client<Cons<S, B>, P> label) where S : SessionType where B : SessionList where P : SessionType
		{
			return new Client<S, P>(label);
		}

		public static Client<S, Cons<S, B>> Zero<S, B>(this Client<Jump<Zero>, Cons<S, B>> jump) where S : SessionType where B : SessionList
		{
			return new Client<S, Cons<S, B>>(jump);
		}

		public static void Close<P>(this Client<Close, P> client) where P : SessionType
		{
			client.Close();
		}

		public static void Close<P>(this Server<Close, P> server) where P : SessionType
		{
			server.Close();
		}
	}
}
