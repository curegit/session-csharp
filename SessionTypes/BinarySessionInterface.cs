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

		public static void Send<S, P, T>(this Server<Respond<T, S>, P> respond, T value, Action<Server<S, P>> action) where S : SessionType where P : SessionType
		{
			respond.Send(value);
			action(new Server<S, P>(respond));
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

		public static async Task SendAsync<S, P, T>(this Server<Respond<T, S>, P> respond, T value, Action<Server<S, P>> action) where S : SessionType where P : SessionType
		{
			await respond.SendAsync(value);
			action(new Server<S, P>(respond));
		}

		public static async Task SendAsync<S, P, T>(this Client<Request<T, S>, P> request, T value, Func<Client<S, P>, Task> action) where S : SessionType where P : SessionType
		{
			await request.SendAsync(value);
			await action(new Client<S, P>(request));
		}

		public static async Task SendAsync<S, P, T>(this Server<Respond<T, S>, P> respond, T value, Func<Server<S, P>, Task> action) where S : SessionType where P : SessionType
		{
			await respond.SendAsync(value);
			await action(new Server<S, P>(respond));
		}

		public static (Client<S, P>, T) Receive<S, P, T>(this Client<Respond<T, S>, P> respond) where S : SessionType where P : SessionType
		{
			return (new Client<S, P>(respond), respond.ReceiveAsync<T>().Result);
		}

		public static (Server<S, P>, T) Receive<S, P, T>(this Server<Request<T, S>, P> request) where S : SessionType where P : SessionType
		{
			return (new Server<S, P>(request), request.ReceiveAsync<T>().Result);
		}

		public static Client<S, P> Receive<S, P, T>(this Client<Respond<T, S>, P> respond, out T value) where S : SessionType where P : SessionType
		{
			value = respond.ReceiveAsync<T>().Result;
			return new Client<S, P>(respond);
		}

		public static Server<S, P> Receive<S, P, T>(this Server<Request<T, S>, P> request, out T value) where S : SessionType where P : SessionType
		{
			value = request.ReceiveAsync<T>().Result;
			return new Server<S, P>(request);
		}

		public static void Receive<S, P, T>(this Client<Respond<T, S>, P> respond, Action<Client<S, P>, T> action) where S : SessionType where P : SessionType
		{
			var value = respond.Receive<T>();
			action(new Client<S, P>(respond), value);
		}

		public static void Receive<S, P, T>(this Server<Request<T, S>, P> request, Action<Server<S, P>, T> action) where S : SessionType where P : SessionType
		{
			var value = request.Receive<T>();
			action(new Server<S, P>(request), value);
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
			var value = await respond.ReceiveAsync<T>();
			action(new Client<S, P>(respond), value);
		}

		public static async Task ReceiveAsync<S, P, T>(this Server<Request<T, S>, P> request, Action<Server<S, P>, T> action) where S : SessionType where P : SessionType
		{
			var value = await request.ReceiveAsync<T>();
			action(new Server<S, P>(request), value);
		}

		public static async Task ReceiveAsync<S, P, T>(this Client<Respond<T, S>, P> respond, Func<Client<S, P>, T, Task> action) where S : SessionType where P : SessionType
		{
			var value = await respond.ReceiveAsync<T>();
			await action(new Client<S, P>(respond), value);
		}

		public static async Task ReceiveAsync<S, P, T>(this Server<Request<T, S>, P> request, Func<Server<S, P>, T, Task> action) where S : SessionType where P : SessionType
		{
			var value = await request.ReceiveAsync<T>();
			await action(new Server<S, P>(request), value);
		}

		public static Client<L, P> ChooseLeft<L, R, P>(this Client<RequestChoice<L, R>, P> requestChoice) where L : SessionType where R : SessionType where P : SessionType
		{
			requestChoice.Send(1);
			return new Client<L, P>(requestChoice);
		}

		public static Client<R, P> ChooseRight<L, R, P>(this Client<RequestChoice<L, R>, P> requestChoice) where L : SessionType where R : SessionType where P : SessionType
		{
			requestChoice.Send(2);
			return new Client<R, P>(requestChoice);
		}

		public static Server<L, P> ChooseLeft<L, R, P>(this Server<RespondChoice<L, R>, P> respondChoice) where L : SessionType where R : SessionType where P : SessionType
		{
			respondChoice.Send(1);
			return new Server<L, P>(respondChoice);
		}

		public static Server<R, P> ChooseRight<L, R, P>(this Server<RespondChoice<L, R>, P> respondChoice) where L : SessionType where R : SessionType where P : SessionType
		{
			respondChoice.Send(2);
			return new Server<R, P>(respondChoice);
		}

		public static async Task FollowAsync<L, R, P>(this Client<RespondChoice<L, R>, P> respondChoice, Action<Client<L, P>> leftAction, Action<Client<R, P>> rightAction) where L : SessionType where R : SessionType where P : SessionType
		{
			var choice = await respondChoice.ReceiveAsync<int>();
			if (choice == 1)
			{
				leftAction(new Client<L, P>(respondChoice));
			}
			else if (choice == 2)
			{
				rightAction(new Client<R, P>(respondChoice));
			}
			else
			{

			}
		}

		public static async Task FollowAsync<L, R, P>(this Client<RespondChoice<L, R>, P> respondChoice, Func<Client<L, P>, Task> leftAction, Action<Client<R, P>> rightAction) where L : SessionType where R : SessionType where P : SessionType
		{
			var choice = await respondChoice.ReceiveAsync<int>();
			if (choice == 1)
			{
				await leftAction(new Client<L, P>(respondChoice));
			}
			else if (choice == 2)
			{
				rightAction(new Client<R, P>(respondChoice));
			}
			else
			{

			}
		}

		public static async Task FollowAsync<L, R, P>(this Client<RespondChoice<L, R>, P> respondChoice, Action<Client<L, P>> leftAction, Func<Client<R, P>, Task> rightAction) where L : SessionType where R : SessionType where P : SessionType
		{
			var choice = await respondChoice.ReceiveAsync<int>();
			if (choice == 1)
			{
				leftAction(new Client<L, P>(respondChoice));
			}
			else if (choice == 2)
			{
				await rightAction(new Client<R, P>(respondChoice));
			}
			else
			{

			}
		}

		public static async Task FollowAsync<L, R, P>(this Client<RespondChoice<L, R>, P> respondChoice, Func<Client<L, P>, Task> leftAction, Func<Client<R, P>, Task> rightAction) where L : SessionType where R : SessionType where P : SessionType
		{
			var choice = await respondChoice.ReceiveAsync<int>();
			if (choice == 1)
			{
				await leftAction(new Client<L, P>(respondChoice));
			}
			else if (choice == 2)
			{
				await rightAction(new Client<R, P>(respondChoice));
			}
			else
			{

			}
		}

		public static async Task FollowAsync<L, R, P>(this Server<RequestChoice<L, R>, P> requestChoice, Action<Server<L, P>> leftAction, Action<Server<R, P>> rightAction) where L : SessionType where R : SessionType where P : SessionType
		{
			var choice = await requestChoice.ReceiveAsync<int>();
			if (choice == 1)
			{
				leftAction(new Server<L, P>(requestChoice));
			}
			else if (choice == 2)
			{
				rightAction(new Server<R, P>(requestChoice));
			}
			else
			{

			}
		}

		public static async Task FollowAsync<L, R, P>(this Server<RequestChoice<L, R>, P> requestChoice, Func<Server<L, P>, Task> leftAction, Action<Server<R, P>> rightAction) where L : SessionType where R : SessionType where P : SessionType
		{
			var choice = await requestChoice.ReceiveAsync<int>();
			if (choice == 1)
			{
				await leftAction(new Server<L, P>(requestChoice));
			}
			else if (choice == 2)
			{
				rightAction(new Server<R, P>(requestChoice));
			}
			else
			{

			}
		}

		public static async Task FollowAsync<L, R, P>(this Server<RequestChoice<L, R>, P> requestChoice, Action<Server<L, P>> leftAction, Func<Server<R, P>, Task> rightAction) where L : SessionType where R : SessionType where P : SessionType
		{
			var choice = await requestChoice.ReceiveAsync<int>();
			if (choice == 1)
			{
				leftAction(new Server<L, P>(requestChoice));
			}
			else if (choice == 2)
			{
				await rightAction(new Server<R, P>(requestChoice));
			}
			else
			{

			}
		}

		public static async Task FollowAsync<L, R, P>(this Server<RequestChoice<L, R>, P> requestChoice, Func<Server<L, P>, Task> leftAction, Func<Server<R, P>, Task> rightAction) where L : SessionType where R : SessionType where P : SessionType
		{
			var choice = await requestChoice.ReceiveAsync<int>();
			if (choice == 1)
			{
				await leftAction(new Server<L, P>(requestChoice));
			}
			else if (choice == 2)
			{
				await rightAction(new Server<R, P>(requestChoice));
			}
			else
			{

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
	}
}
