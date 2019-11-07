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

		public static async Task<Session<S, P>> SendAsync<S, P, T>(this Session<Send<T, S>, P> session, T value) where S : SessionType where P : ProtocolType
		{
			await session.SendAsync(value);
			return session.ToNext<S>();
		}

		public static (Session<S, P>, T) Receive<S, P, T>(this Session<Receive<T, S>, P> session) where S : SessionType where P : ProtocolType
		{
			return (session.ToNext<S>(), session.Receive<T>());
		}

		public static Session<S, P> Receive<S, P, T>(this Session<Receive<T, S>, P> session, out T value) where S : SessionType where P : ProtocolType
		{
			value = session.Receive<T>();
			return session.ToNext<S>();
		}

		public static async Task<(Session<S, P>, T)> ReceiveAsync<S, P, T>(this Session<Receive<T, S>, P> session) where S : SessionType where P : ProtocolType
		{
			return (session.ToNext<S>(), await session.ReceiveAsync<T>());
		}

		public static Session<L, P> SelectLeft<L, R, P>(this Session<Select<L, R>, P> session) where L : SessionType where R : SessionType where P : ProtocolType
		{
			session.Choose(Choice.Left);
			return session.ToNext<L>();
		}

		public static Session<R, P> SelectRight<L, R, P>(this Session<Select<L, R>, P> session) where L : SessionType where R : SessionType where P : ProtocolType
		{
			session.Choose(Choice.Right);
			return session.ToNext<R>();
		}

		public static async Task<Session<L, P>> SelectLeftAsync<L, R, P>(this Session<Select<L, R>, P> session) where L : SessionType where R : SessionType where P : ProtocolType
		{
			await session.ChooseAsync(Choice.Left);
			return session.ToNext<L>();
		}

		public static async Task<Session<R, P>> SelectRightAsync<L, R, P>(this Session<Select<L, R>, P> session) where L : SessionType where R : SessionType where P : ProtocolType
		{
			await session.ChooseAsync(Choice.Right);
			return session.ToNext<R>();
		}

		public static void Follow<L, R, P>(this Session<Follow<L, R>, P> session, Action<Session<L, P>> leftAction, Action<Session<R, P>> rightAction) where L : SessionType where R : SessionType where P : ProtocolType
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

		public static async Task Follow<L, R, P>(this Session<Follow<L, R>, P> session, Func<Session<L, P>, Task> leftAction, Action<Session<R, P>> rightAction) where L : SessionType where R : SessionType where P : ProtocolType
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

		public static async Task Follow<L, R, P>(this Session<Follow<L, R>, P> session, Action<Session<L, P>> leftAction, Func<Session<R, P>, Task> rightAction) where L : SessionType where R : SessionType where P : ProtocolType
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

		public static async Task Follow<L, R, P>(this Session<Follow<L, R>, P> session, Func<Session<L, P>, Task> leftAction, Func<Session<R, P>, Task> rightAction) where L : SessionType where R : SessionType where P : ProtocolType
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

		public static async Task FollowAsync<L, R, P>(this Session<Follow<L, R>, P> session, Action<Session<L, P>> leftAction, Action<Session<R, P>> rightAction) where L : SessionType where R : SessionType where P : ProtocolType
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

		public static async Task FollowAsync<L, R, P>(this Session<Follow<L, R>, P> session, Func<Session<L, P>, Task> leftAction, Action<Session<R, P>> rightAction) where L : SessionType where R : SessionType where P : ProtocolType
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

		public static async Task FollowAsync<L, R, P>(this Session<Follow<L, R>, P> session, Action<Session<L, P>> leftAction, Func<Session<R, P>, Task> rightAction) where L : SessionType where R : SessionType where P : ProtocolType
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

		public static async Task FollowAsync<L, R, P>(this Session<Follow<L, R>, P> session, Func<Session<L, P>, Task> leftAction, Func<Session<R, P>, Task> rightAction) where L : SessionType where R : SessionType where P : ProtocolType
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

		public static Session<S, P> Enter<S, L, P>(this Session<Cons<S, L>, P> session) where S : SessionType where L : SessionList where P : ProtocolType
		{
			return session.ToNext<S>();
		}

		public static Session<S, S> Jump<S>(this Session<Goto0, S> session) where S : SessionType
		{
			return session.ToNext<S>();
		}

		public static Session<S, Cons<S, L>> Jump<S, L>(this Session<Goto0, Cons<S, L>> session) where S : SessionType where L : SessionList
		{
			return session.ToNext<S>();
		}

		public static void Close<P>(this Session<Close, P> session) where P : ProtocolType
		{
			session.Close();
		}
	}
}
