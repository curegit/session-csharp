using System;
using System.Threading.Tasks;

namespace SessionTypes
{
	public static class SessionInterface
	{
		public static Session<S, P> Send<S, P, T>(this Session<Send<T, S>, P> session, T value) where S : SessionType where P : ProtocolType
		{
			session.Send(value);
			return session.ToNextSession<S>();
		}

		public static async Task<Session<S, P>> SendAsync<S, P, T>(this Session<Send<T, S>, P> session, T value) where S : SessionType where P : ProtocolType
		{
			await session.SendAsync(value);
			return session.ToNextSession<S>();
		}

		public static (Session<S, P>, T) Receive<S, P, T>(this Session<Receive<T, S>, P> session) where S : SessionType where P : ProtocolType
		{
			return (session.ToNextSession<S>(), session.Receive<T>());
		}

		public static Session<S, P> Receive<S, P, T>(this Session<Receive<T, S>, P> session, out T value) where S : SessionType where P : ProtocolType
		{
			value = session.Receive<T>();
			return session.ToNextSession<S>();
		}

		public static async Task<(Session<S, P>, T)> ReceiveAsync<S, P, T>(this Session<Receive<T, S>, P> session) where S : SessionType where P : ProtocolType
		{
			return (session.ToNextSession<S>(), await session.ReceiveAsync<T>());
		}

		public static Session<S, P> SendNewChannel<N, O, S, P>(this Session<Cast<N, O, S>, P> session, out Session<N, N> channel) where N : ProtocolType where O : ProtocolType where S : SessionType where P : ProtocolType
		{
			channel = session.SendNewChannel<N, O>();
			return session.ToNextSession<S>();
		}

		public static Session<S, P> ReceiveNewChannel<N, S, P>(this Session<Accept<N, S>, P> session, out Session<N, N> channel) where N : ProtocolType where S : SessionType where P : ProtocolType
		{
			channel = session.ReceiveNewChannel<N>();
			return session.ToNextSession<S>();
		}

		public static Session<L, P> SelectLeft<L, R, P>(this Session<Select<L, R>, P> session) where L : SessionType where R : SessionType where P : ProtocolType
		{
			session.Select(Direction.Left);
			return session.ToNextSession<L>();
		}

		public static Session<R, P> SelectRight<L, R, P>(this Session<Select<L, R>, P> session) where L : SessionType where R : SessionType where P : ProtocolType
		{
			session.Select(Direction.Right);
			return session.ToNextSession<R>();
		}

		public static async Task<Session<L, P>> SelectLeftAsync<L, R, P>(this Session<Select<L, R>, P> session) where L : SessionType where R : SessionType where P : ProtocolType
		{
			await session.SelectAsync(Direction.Left);
			return session.ToNextSession<L>();
		}

		public static async Task<Session<R, P>> SelectRightAsync<L, R, P>(this Session<Select<L, R>, P> session) where L : SessionType where R : SessionType where P : ProtocolType
		{
			await session.SelectAsync(Direction.Right);
			return session.ToNextSession<R>();
		}

		public static void Follow<L, R, P>(this Session<Follow<L, R>, P> session, Action<Session<L, P>> leftAction, Action<Session<R, P>> rightAction) where L : SessionType where R : SessionType where P : ProtocolType
		{
			switch (session.Follow())
			{
				case Direction.Left:
					leftAction(session.ToNextSession<L>());
					break;
				case Direction.Right:
					rightAction(session.ToNextSession<R>());
					break;
				default:
					throw new UnknownChoiceException();
			}
		}

		public static async Task Follow<L, R, P>(this Session<Follow<L, R>, P> session, Func<Session<L, P>, Task> leftAction, Action<Session<R, P>> rightAction) where L : SessionType where R : SessionType where P : ProtocolType
		{
			switch (session.Follow())
			{
				case Direction.Left:
					await leftAction(session.ToNextSession<L>());
					break;
				case Direction.Right:
					rightAction(session.ToNextSession<R>());
					break;
				default:
					throw new UnknownChoiceException();
			}
		}

		public static async Task Follow<L, R, P>(this Session<Follow<L, R>, P> session, Action<Session<L, P>> leftAction, Func<Session<R, P>, Task> rightAction) where L : SessionType where R : SessionType where P : ProtocolType
		{
			switch (session.Follow())
			{
				case Direction.Left:
					leftAction(session.ToNextSession<L>());
					break;
				case Direction.Right:
					await rightAction(session.ToNextSession<R>());
					break;
				default:
					throw new UnknownChoiceException();
			}
		}

		public static async Task Follow<L, R, P>(this Session<Follow<L, R>, P> session, Func<Session<L, P>, Task> leftAction, Func<Session<R, P>, Task> rightAction) where L : SessionType where R : SessionType where P : ProtocolType
		{
			switch (session.Follow())
			{
				case Direction.Left:
					await leftAction(session.ToNextSession<L>());
					break;
				case Direction.Right:
					await rightAction(session.ToNextSession<R>());
					break;
				default:
					throw new UnknownChoiceException();
			}
		}

		public static async Task FollowAsync<L, R, P>(this Session<Follow<L, R>, P> session, Action<Session<L, P>> leftAction, Action<Session<R, P>> rightAction) where L : SessionType where R : SessionType where P : ProtocolType
		{
			switch (await session.FollowAsync())
			{
				case Direction.Left:
					leftAction(session.ToNextSession<L>());
					break;
				case Direction.Right:
					rightAction(session.ToNextSession<R>());
					break;
				default:
					throw new UnknownChoiceException();
			}
		}

		public static async Task FollowAsync<L, R, P>(this Session<Follow<L, R>, P> session, Func<Session<L, P>, Task> leftAction, Action<Session<R, P>> rightAction) where L : SessionType where R : SessionType where P : ProtocolType
		{
			switch (await session.FollowAsync())
			{
				case Direction.Left:
					await leftAction(session.ToNextSession<L>());
					break;
				case Direction.Right:
					rightAction(session.ToNextSession<R>());
					break;
				default:
					throw new UnknownChoiceException();
			}
		}

		public static async Task FollowAsync<L, R, P>(this Session<Follow<L, R>, P> session, Action<Session<L, P>> leftAction, Func<Session<R, P>, Task> rightAction) where L : SessionType where R : SessionType where P : ProtocolType
		{
			switch (await session.FollowAsync())
			{
				case Direction.Left:
					leftAction(session.ToNextSession<L>());
					break;
				case Direction.Right:
					await rightAction(session.ToNextSession<R>());
					break;
				default:
					throw new UnknownChoiceException();
			}
		}

		public static async Task FollowAsync<L, R, P>(this Session<Follow<L, R>, P> session, Func<Session<L, P>, Task> leftAction, Func<Session<R, P>, Task> rightAction) where L : SessionType where R : SessionType where P : ProtocolType
		{
			switch (await session.FollowAsync())
			{
				case Direction.Left:
					await leftAction(session.ToNextSession<L>());
					break;
				case Direction.Right:
					await rightAction(session.ToNextSession<R>());
					break;
				default:
					throw new UnknownChoiceException();
			}
		}

		public static Session<S, P> Enter<S, L, P>(this Session<Cons<S, L>, P> session) where S : SessionType where L : SessionList where P : ProtocolType
		{
			return session.ToNextSession<S>();
		}

		public static Session<S, S> Goto<S>(this Session<Goto0, S> session) where S : SessionType
		{
			return session.ToNextSession<S>();
		}

		public static Session<S0, Cons<S0, L>> Goto<S0, L>(this Session<Goto0, Cons<S0, L>> session) where S0 : SessionType where L : SessionList
		{
			return session.ToNextSession<S0>();
		}

		public static Session<S1, Cons<S0, Cons<S1, L>>> Goto<S0, S1, L>(this Session<Goto1, Cons<S0, Cons<S1, L>>> session) where S0 : SessionType where S1 : SessionType where L : SessionList
		{
			return session.ToNextSession<S1>();
		}

		public static Session<S2, Cons<S0, Cons<S1, Cons<S2, L>>>> Goto<S0, S1, S2, L>(this Session<Goto2, Cons<S0, Cons<S1, Cons<S2, L>>>> session) where S0 : SessionType where S1 : SessionType where S2 : SessionType where L : SessionList
		{
			return session.ToNextSession<S2>();
		}

		public static void Close<P>(this Session<End, P> session) where P : ProtocolType
		{
			session.Close();
		}
	}
}
