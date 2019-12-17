using System;
using System.Threading.Tasks;

namespace SessionTypes
{
	public static class SessionInterface
	{
		public static Session<S, E, P> Send<S, E, P, T>(this Session<Send<T, S>, E, P> session, T value) where S : SessionType where E : SessionStack where P : ProtocolType
		{
			session.Send(value);
			return session.ToNextSession<S>();
		}

		public static async Task<Session<S, E, P>> SendAsync<S, E, P, T>(this Session<Send<T, S>, E, P> session, T value) where S : SessionType where E : SessionStack where P : ProtocolType
		{
			await session.SendAsync(value);
			return session.ToNextSession<S>();
		}

		public static (Session<S, E, P>, T) Receive<S, E, P, T>(this Session<Receive<T, S>, E, P> session) where S : SessionType where E : SessionStack where P : ProtocolType
		{
			return (session.ToNextSession<S>(), session.Receive<T>());
		}

		public static Session<S, E, P> Receive<S, E, P, T>(this Session<Receive<T, S>, E, P> session, out T value) where S : SessionType where E : SessionStack where P : ProtocolType
		{
			value = session.Receive<T>();
			return session.ToNextSession<S>();
		}

		public static async Task<(Session<S, E, P>, T)> ReceiveAsync<S, E, P, T>(this Session<Receive<T, S>, E, P> session) where S : SessionType where E : SessionStack where P : ProtocolType
		{
			return (session.ToNextSession<S>(), await session.ReceiveAsync<T>());
		}

		public static Session<L, E, P> SelectLeft<L, R, E, P>(this Session<Select<L, R>, E, P> session) where L : SessionType where R : SessionType where E : SessionStack where P : ProtocolType
		{
			session.Select(Direction.Left);
			return session.ToNextSession<L>();
		}

		public static Session<R, E, P> SelectRight<L, R, E, P>(this Session<Select<L, R>, E, P> session) where L : SessionType where R : SessionType where E : SessionStack where P : ProtocolType
		{
			session.Select(Direction.Right);
			return session.ToNextSession<R>();
		}

		public static async Task<Session<L, E, P>> SelectLeftAsync<L, R, E, P>(this Session<Select<L, R>, E, P> session) where L : SessionType where R : SessionType where E : SessionStack where P : ProtocolType
		{
			await session.SelectAsync(Direction.Left);
			return session.ToNextSession<L>();
		}

		public static async Task<Session<R, E, P>> SelectRightAsync<L, R, E, P>(this Session<Select<L, R>, E, P> session) where L : SessionType where R : SessionType where E : SessionStack where P : ProtocolType
		{
			await session.SelectAsync(Direction.Right);
			return session.ToNextSession<R>();
		}

		public static void Follow<L, R, E, P>(this Session<Follow<L, R>, E, P> session, Action<Session<L, E, P>> leftAction, Action<Session<R, E, P>> rightAction) where L : SessionType where R : SessionType where E : SessionStack where P : ProtocolType
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

		public static N Follow<L, R, N, E, P>(this Session<Follow<L, R>, E, P> session, Func<Session<L, E, P>, N> leftAction, Func<Session<R, E, P>, N> rightAction) where L : SessionType where R : SessionType where E : SessionStack where P : ProtocolType
		{
			switch (session.Follow())
			{
				case Direction.Left:
					return leftAction(session.ToNextSession<L>());
				case Direction.Right:
					return rightAction(session.ToNextSession<R>());
				default:
					throw new UnknownChoiceException();
			}
		}

		public static async Task Follow<L, R, E, P>(this Session<Follow<L, R>, E, P> session, Func<Session<L, E, P>, Task> leftAction, Action<Session<R, E, P>> rightAction) where L : SessionType where R : SessionType where E : SessionStack where P : ProtocolType
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

		public static async Task Follow<L, R, E, P>(this Session<Follow<L, R>, E, P> session, Action<Session<L, E, P>> leftAction, Func<Session<R, E, P>, Task> rightAction) where L : SessionType where R : SessionType where E : SessionStack where P : ProtocolType
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

		public static async Task Follow<L, R, E, P>(this Session<Follow<L, R>, E, P> session, Func<Session<L, E, P>, Task> leftAction, Func<Session<R, E, P>, Task> rightAction) where L : SessionType where R : SessionType where E : SessionStack where P : ProtocolType
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

		public static async Task FollowAsync<L, R, E, P>(this Session<Follow<L, R>, E, P> session, Action<Session<L, E, P>> leftAction, Action<Session<R, E, P>> rightAction) where L : SessionType where R : SessionType where E : SessionStack where P : ProtocolType
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

		public static async Task FollowAsync<L, R, E, P>(this Session<Follow<L, R>, E, P> session, Func<Session<L, E, P>, Task> leftAction, Action<Session<R, E, P>> rightAction) where L : SessionType where R : SessionType where E : SessionStack where P : ProtocolType
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

		public static async Task FollowAsync<L, R, E, P>(this Session<Follow<L, R>, E, P> session, Action<Session<L, E, P>> leftAction, Func<Session<R, E, P>, Task> rightAction) where L : SessionType where R : SessionType where E : SessionStack where P : ProtocolType
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

		public static async Task FollowAsync<L, R, E, P>(this Session<Follow<L, R>, E, P> session, Func<Session<L, E, P>, Task> leftAction, Func<Session<R, E, P>, Task> rightAction) where L : SessionType where R : SessionType where E : SessionStack where P : ProtocolType
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

		public static Session<S, E, P> Enter<S, L, E, P>(this Session<Cons<S, L>, E, P> session) where S : SessionType where L : SessionList where E : SessionStack where P : ProtocolType
		{
			return session.ToNextSession<S>();
		}

		public static Session<S, E, S> Goto<S, E>(this Session<Goto0, E, S> session) where S : SessionType where E : SessionStack
		{
			return session.ToNextSession<S>();
		}

		public static Session<S0, E, Cons<S0, L>> Goto<S0, E, L>(this Session<Goto0, E, Cons<S0, L>> session) where S0 : SessionType where E : SessionStack where L : SessionList
		{
			return session.ToNextSession<S0>();
		}

		public static Session<S1, E, Cons<S0, Cons<S1, L>>> Goto<S0, S1, E, L>(this Session<Goto1, E, Cons<S0, Cons<S1, L>>> session) where S0 : SessionType where S1 : SessionType where E : SessionStack where L : SessionList
		{
			return session.ToNextSession<S1>();
		}

		public static Session<S2, E, Cons<S0, Cons<S1, Cons<S2, L>>>> Goto<S0, S1, S2, E, L>(this Session<Goto2, E, Cons<S0, Cons<S1, Cons<S2, L>>>> session) where S0 : SessionType where S1 : SessionType where S2 : SessionType where E : SessionStack where L : SessionList
		{
			return session.ToNextSession<S2>();
		}

		public static Session<S0, Push<N, E>, Cons<S0, L>> Call<S0, N, E, L>(this Session<Call0<N>, E, Cons<S0, L>> session) where S0 : SessionType where N : SessionType where L : SessionList where E : SessionStack
		{
			return session.ToNextSession<S0, Push<N, E>>();
		}

		public static Session<S1, Push<N, E>, Cons<S0, Cons<S1, L>>> Call<S0, S1, N, E, L>(this Session<Call1<N>, E, Cons<S0, Cons<S1, L>>> session) where S0 : SessionType where S1 : SessionType where N : SessionType where L : SessionList where E : SessionStack
		{
			return session.ToNextSession<S1, Push<N, E>>();
		}

		public delegate Session<Close, E, P> SelfCall<S, E, P>(Session<S, E, P> s, SelfCall<S, E, P> f) where S : SessionType where P : ProtocolType where E : SessionStack;

		public static Session<N, E, Cons<S0, L>> Call<S0, N, E, L>
			(this Session<Call0<N>, E, Cons<S0, L>> session, SelfCall<S0, E, Cons<S0, L>> f)
		where S0 : SessionType where N : SessionType where L : SessionList where E : SessionStack
		{
			return f(session.ToNextSession<S0>(), f).ToNextSession<N, E>();
		}

		public static Session<N, E, Cons<S0, Cons<S1, L>>> Call<S0, S1, N, E, L>
	(this Session<Call1<N>, E, Cons<S0, Cons<S1, L>>> session, SelfCall<S1, E, Cons<S0, Cons<S1, L>>> f)
where S0 : SessionType where S1 : SessionType where N : SessionType where L : SessionList where E : SessionStack
		{
			return f(session.ToNextSession<S1>(), f).ToNextSession<N, E>();
		}

		public static Session<F, E, P> Return<F, E,P>(this Session<Close, Push<F, E>, P> session) where F : SessionType where E : SessionStack where P : ProtocolType
		{
			return session.ToNextSession<F, E>();
		}

		public static void Close<P>(this Session<Close, Empty, P> session) where P : ProtocolType
		{
			session.Close();
		}
	}
}
