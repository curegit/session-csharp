using System;
using System.Threading.Tasks;

namespace Session
{
	public static class SessionInterface
	{
		public static Session<S, E, P> Send<S, E, P>(this Session<Send<S>, E, P> session) where S : SessionType where E : SessionStack where P : ProtocolType
		{
			if (session is null) throw new ArgumentNullException(nameof(session));
			session.Send();
			return session.ToNextSession<S>();
		}

		public static Session<S, E, P> Send<S, E, P, T>(this Session<Send<T, S>, E, P> session, T value) where S : SessionType where E : SessionStack where P : ProtocolType
		{
			if (session is null) throw new ArgumentNullException(nameof(session));
			session.Send(value);
			return session.ToNextSession<S>();
		}

		public static Session<S, E, P> SendAsync<S, E, P>(this Session<Send<S>, E, P> session) where S : SessionType where E : SessionStack where P : ProtocolType
		{
			if (session is null) throw new ArgumentNullException(nameof(session));
			session.SendAsync();
			return session.ToNextSession<S>();
		}

		public static Session<S, E, P> SendAsync<S, E, P>(this Session<Send<S>, E, P> session, out Task task) where S : SessionType where E : SessionStack where P : ProtocolType
		{
			if (session is null) throw new ArgumentNullException(nameof(session));
			task = session.SendAsync();
			return session.ToNextSession<S>();
		}

		public static Session<S, E, P> SendAsync<S, E, P, T>(this Session<Send<T, S>, E, P> session, T value) where S : SessionType where E : SessionStack where P : ProtocolType
		{
			if (session is null) throw new ArgumentNullException(nameof(session));
			session.SendAsync(value);
			return session.ToNextSession<S>();
		}

		public static Session<S, E, P> SendAsync<S, E, P, T>(this Session<Send<T, S>, E, P> session, T value, out Task task) where S : SessionType where E : SessionStack where P : ProtocolType
		{
			if (session is null) throw new ArgumentNullException(nameof(session));
			task = session.SendAsync(value);
			return session.ToNextSession<S>();
		}

		public static Session<S, E, P> Receive<S, E, P>(this Session<Receive<S>, E, P> session) where S : SessionType where E : SessionStack where P : ProtocolType
		{
			if (session is null) throw new ArgumentNullException(nameof(session));
			session.Receive();
			return session.ToNextSession<S>();
		}

		public static Session<S, E, P> Receive<S, E, P, T>(this Session<Receive<T, S>, E, P> session, out T value) where S : SessionType where E : SessionStack where P : ProtocolType
		{
			if (session is null) throw new ArgumentNullException(nameof(session));
			value = session.Receive<T>();
			return session.ToNextSession<S>();
		}

		public static Session<S, E, P> Receive<S, E, P, T1, T2>(this Session<Receive<(T1, T2), S>, E, P> session, out T1 value1, out T2 value2) where S : SessionType where E : SessionStack where P : ProtocolType
		{
			if (session is null) throw new ArgumentNullException(nameof(session));
			(value1, value2) = session.Receive<(T1, T2)>();
			return session.ToNextSession<S>();
		}

		public static Session<S, E, P> Receive<S, E, P, T1, T2, T3>(this Session<Receive<(T1, T2, T3), S>, E, P> session, out T1 value1, out T2 value2, out T3 value3) where S : SessionType where E : SessionStack where P : ProtocolType
		{
			if (session is null) throw new ArgumentNullException(nameof(session));
			(value1, value2, value3) = session.Receive<(T1, T2, T3)>();
			return session.ToNextSession<S>();
		}

		public static Session<S, E, P> Receive<S, E, P, T1, T2, T3, T4>(this Session<Receive<(T1, T2, T3, T4), S>, E, P> session, out T1 value1, out T2 value2, out T3 value3, out T4 value4) where S : SessionType where E : SessionStack where P : ProtocolType
		{
			if (session is null) throw new ArgumentNullException(nameof(session));
			(value1, value2, value3, value4) = session.Receive<(T1, T2, T3, T4)>();
			return session.ToNextSession<S>();
		}

		public static Session<S, E, P> Receive<S, E, P, T1, T2, T3, T4, T5>(this Session<Receive<(T1, T2, T3, T4, T5), S>, E, P> session, out T1 value1, out T2 value2, out T3 value3, out T4 value4, out T5 value5) where S : SessionType where E : SessionStack where P : ProtocolType
		{
			if (session is null) throw new ArgumentNullException(nameof(session));
			(value1, value2, value3, value4, value5) = session.Receive<(T1, T2, T3, T4, T5)>();
			return session.ToNextSession<S>();
		}

		public static Session<S, E, P> ReceiveAsync<S, E, P>(this Session<Receive<S>, E, P> session, out Task future) where S : SessionType where E : SessionStack where P : ProtocolType
		{
			if (session is null) throw new ArgumentNullException(nameof(session));
			future = session.ReceiveAsync();
			return session.ToNextSession<S>();
		}

		public static Session<S, E, P> ReceiveAsync<S, E, P, T>(this Session<Receive<T, S>, E, P> session, out Task<T> futureValue) where S : SessionType where E : SessionStack where P : ProtocolType
		{
			if (session is null) throw new ArgumentNullException(nameof(session));
			futureValue = session.ReceiveAsync<T>();
			return session.ToNextSession<S>();
		}

		public static Session<L, E, P> SelectLeft<L, R, E, P>(this Session<Select<L, R>, E, P> session) where L : SessionType where R : SessionType where E : SessionStack where P : ProtocolType
		{
			if (session is null) throw new ArgumentNullException(nameof(session));
			session.Select(Selection.Left);
			return session.ToNextSession<L>();
		}

		public static Session<R, E, P> SelectRight<L, R, E, P>(this Session<Select<L, R>, E, P> session) where L : SessionType where R : SessionType where E : SessionStack where P : ProtocolType
		{
			if (session is null) throw new ArgumentNullException(nameof(session));
			session.Select(Selection.Right);
			return session.ToNextSession<R>();
		}

		public static Session<L, E, P> SelectLeft<L, C, R, E, P>(this Session<Select<L, C, R>, E, P> session) where L : SessionType where C : SessionType where R : SessionType where E : SessionStack where P : ProtocolType
		{
			if (session is null) throw new ArgumentNullException(nameof(session));
			session.Select(Selection.Left);
			return session.ToNextSession<L>();
		}

		public static Session<C, E, P> SelectCenter<L, C, R, E, P>(this Session<Select<L, C, R>, E, P> session) where L : SessionType where C : SessionType where R : SessionType where E : SessionStack where P : ProtocolType
		{
			if (session is null) throw new ArgumentNullException(nameof(session));
			session.Select(Selection.Center);
			return session.ToNextSession<C>();
		}

		public static Session<R, E, P> SelectRight<L, C, R, E, P>(this Session<Select<L, C, R>, E, P> session) where L : SessionType where C : SessionType where R : SessionType where E : SessionStack where P : ProtocolType
		{
			if (session is null) throw new ArgumentNullException(nameof(session));
			session.Select(Selection.Right);
			return session.ToNextSession<R>();
		}

		public static Session<L, E, P> SelectLeftAsync<L, R, E, P>(this Session<Select<L, R>, E, P> session) where L : SessionType where R : SessionType where E : SessionStack where P : ProtocolType
		{
			if (session is null) throw new ArgumentNullException(nameof(session));
			session.SelectAsync(Selection.Left);
			return session.ToNextSession<L>();
		}

		public static Session<L, E, P> SelectLeftAsync<L, R, E, P>(this Session<Select<L, R>, E, P> session, out Task task) where L : SessionType where R : SessionType where E : SessionStack where P : ProtocolType
		{
			if (session is null) throw new ArgumentNullException(nameof(session));
			task = session.SelectAsync(Selection.Left);
			return session.ToNextSession<L>();
		}

		public static Session<R, E, P> SelectRightAsync<L, R, E, P>(this Session<Select<L, R>, E, P> session) where L : SessionType where R : SessionType where E : SessionStack where P : ProtocolType
		{
			if (session is null) throw new ArgumentNullException(nameof(session));
			session.SelectAsync(Selection.Right);
			return session.ToNextSession<R>();
		}

		public static Session<R, E, P> SelectRightAsync<L, R, E, P>(this Session<Select<L, R>, E, P> session, out Task task) where L : SessionType where R : SessionType where E : SessionStack where P : ProtocolType
		{
			if (session is null) throw new ArgumentNullException(nameof(session));
			task = session.SelectAsync(Selection.Right);
			return session.ToNextSession<R>();
		}

		public static Session<L, E, P> SelectLeftAsync<L, C, R, E, P>(this Session<Select<L, C, R>, E, P> session) where L : SessionType where C : SessionType where R : SessionType where E : SessionStack where P : ProtocolType
		{
			if (session is null) throw new ArgumentNullException(nameof(session));
			session.SelectAsync(Selection.Left);
			return session.ToNextSession<L>();
		}

		public static Session<L, E, P> SelectLeftAsync<L, C, R, E, P>(this Session<Select<L, C, R>, E, P> session, out Task task) where L : SessionType where C : SessionType where R : SessionType where E : SessionStack where P : ProtocolType
		{
			if (session is null) throw new ArgumentNullException(nameof(session));
			task = session.SelectAsync(Selection.Left);
			return session.ToNextSession<L>();
		}

		public static Session<C, E, P> SelectCenterAsync<L, C, R, E, P>(this Session<Select<L, C, R>, E, P> session) where L : SessionType where C : SessionType where R : SessionType where E : SessionStack where P : ProtocolType
		{
			if (session is null) throw new ArgumentNullException(nameof(session));
			session.SelectAsync(Selection.Center);
			return session.ToNextSession<C>();
		}

		public static Session<C, E, P> SelectCenterAsync<L, C, R, E, P>(this Session<Select<L, C, R>, E, P> session, out Task task) where L : SessionType where C : SessionType where R : SessionType where E : SessionStack where P : ProtocolType
		{
			if (session is null) throw new ArgumentNullException(nameof(session));
			task = session.SelectAsync(Selection.Center);
			return session.ToNextSession<C>();
		}

		public static Session<R, E, P> SelectRightAsync<L, C, R, E, P>(this Session<Select<L, C, R>, E, P> session) where L : SessionType where C : SessionType where R : SessionType where E : SessionStack where P : ProtocolType
		{
			if (session is null) throw new ArgumentNullException(nameof(session));
			session.SelectAsync(Selection.Right);
			return session.ToNextSession<R>();
		}

		public static Session<R, E, P> SelectRightAsync<L, C, R, E, P>(this Session<Select<L, C, R>, E, P> session, out Task task) where L : SessionType where C : SessionType where R : SessionType where E : SessionStack where P : ProtocolType
		{
			if (session is null) throw new ArgumentNullException(nameof(session));
			task = session.SelectAsync(Selection.Right);
			return session.ToNextSession<R>();
		}

		public static void Follow<L, R, E, P>(this Session<Follow<L, R>, E, P> session, Action<Session<L, E, P>> leftAction, Action<Session<R, E, P>> rightAction) where L : SessionType where R : SessionType where E : SessionStack where P : ProtocolType
		{
			if (session is null) throw new ArgumentNullException(nameof(session));
			if (leftAction is null) throw new ArgumentNullException(nameof(leftAction));
			if (rightAction is null) throw new ArgumentNullException(nameof(rightAction));
			switch (session.Follow())
			{
				case Selection.Left:
					leftAction(session.ToNextSession<L>());
					break;
				case Selection.Right:
					rightAction(session.ToNextSession<R>());
					break;
				default:
					throw new InvalidSelectionException();
			}
		}

		public static async Task Follow<L, R, E, P>(this Session<Follow<L, R>, E, P> session, Func<Session<L, E, P>, Task> leftAction, Action<Session<R, E, P>> rightAction) where L : SessionType where R : SessionType where E : SessionStack where P : ProtocolType
		{
			if (session is null) throw new ArgumentNullException(nameof(session));
			if (leftAction is null) throw new ArgumentNullException(nameof(leftAction));
			if (rightAction is null) throw new ArgumentNullException(nameof(rightAction));
			switch (session.Follow())
			{
				case Selection.Left:
					await leftAction(session.ToNextSession<L>());
					break;
				case Selection.Right:
					rightAction(session.ToNextSession<R>());
					break;
				default:
					throw new InvalidSelectionException();
			}
		}

		public static async Task Follow<L, R, E, P>(this Session<Follow<L, R>, E, P> session, Action<Session<L, E, P>> leftAction, Func<Session<R, E, P>, Task> rightAction) where L : SessionType where R : SessionType where E : SessionStack where P : ProtocolType
		{
			if (session is null) throw new ArgumentNullException(nameof(session));
			if (leftAction is null) throw new ArgumentNullException(nameof(leftAction));
			if (rightAction is null) throw new ArgumentNullException(nameof(rightAction));
			switch (session.Follow())
			{
				case Selection.Left:
					leftAction(session.ToNextSession<L>());
					break;
				case Selection.Right:
					await rightAction(session.ToNextSession<R>());
					break;
				default:
					throw new InvalidSelectionException();
			}
		}

		public static async Task Follow<L, R, E, P>(this Session<Follow<L, R>, E, P> session, Func<Session<L, E, P>, Task> leftAction, Func<Session<R, E, P>, Task> rightAction) where L : SessionType where R : SessionType where E : SessionStack where P : ProtocolType
		{
			if (session is null) throw new ArgumentNullException(nameof(session));
			if (leftAction is null) throw new ArgumentNullException(nameof(leftAction));
			if (rightAction is null) throw new ArgumentNullException(nameof(rightAction));
			switch (session.Follow())
			{
				case Selection.Left:
					await leftAction(session.ToNextSession<L>());
					break;
				case Selection.Right:
					await rightAction(session.ToNextSession<R>());
					break;
				default:
					throw new InvalidSelectionException();
			}
		}

		public static T Follow<L, R, E, P, T>(this Session<Follow<L, R>, E, P> session, Func<Session<L, E, P>, T> leftFunc, Func<Session<R, E, P>, T> rightFunc) where L : SessionType where R : SessionType where E : SessionStack where P : ProtocolType
		{
			if (session is null) throw new ArgumentNullException(nameof(session));
			if (leftFunc is null) throw new ArgumentNullException(nameof(leftFunc));
			if (rightFunc is null) throw new ArgumentNullException(nameof(rightFunc));
			switch (session.Follow())
			{
				case Selection.Left:
					return leftFunc(session.ToNextSession<L>());
				case Selection.Right:
					return rightFunc(session.ToNextSession<R>());
				default:
					throw new InvalidSelectionException();
			}
		}

		public static async Task<T> Follow<L, R, E, P, T>(this Session<Follow<L, R>, E, P> session, Func<Session<L, E, P>, Task<T>> leftFunc, Func<Session<R, E, P>, T> rightFunc) where L : SessionType where R : SessionType where E : SessionStack where P : ProtocolType
		{
			if (session is null) throw new ArgumentNullException(nameof(session));
			if (leftFunc is null) throw new ArgumentNullException(nameof(leftFunc));
			if (rightFunc is null) throw new ArgumentNullException(nameof(rightFunc));
			switch (session.Follow())
			{
				case Selection.Left:
					return await leftFunc(session.ToNextSession<L>());
				case Selection.Right:
					return rightFunc(session.ToNextSession<R>());
				default:
					throw new InvalidSelectionException();
			}
		}

		public static async Task<T> Follow<L, R, E, P, T>(this Session<Follow<L, R>, E, P> session, Func<Session<L, E, P>, T> leftFunc, Func<Session<R, E, P>, Task<T>> rightFunc) where L : SessionType where R : SessionType where E : SessionStack where P : ProtocolType
		{
			if (session is null) throw new ArgumentNullException(nameof(session));
			if (leftFunc is null) throw new ArgumentNullException(nameof(leftFunc));
			if (rightFunc is null) throw new ArgumentNullException(nameof(rightFunc));
			switch (session.Follow())
			{
				case Selection.Left:
					return leftFunc(session.ToNextSession<L>());
				case Selection.Right:
					return await rightFunc(session.ToNextSession<R>());
				default:
					throw new InvalidSelectionException();
			}
		}

		public static async Task<T> Follow<L, R, E, P, T>(this Session<Follow<L, R>, E, P> session, Func<Session<L, E, P>, Task<T>> leftFunc, Func<Session<R, E, P>, Task<T>> rightFunc) where L : SessionType where R : SessionType where E : SessionStack where P : ProtocolType
		{
			if (session is null) throw new ArgumentNullException(nameof(session));
			if (leftFunc is null) throw new ArgumentNullException(nameof(leftFunc));
			if (rightFunc is null) throw new ArgumentNullException(nameof(rightFunc));
			switch (session.Follow())
			{
				case Selection.Left:
					return await leftFunc(session.ToNextSession<L>());
				case Selection.Right:
					return await rightFunc(session.ToNextSession<R>());
				default:
					throw new InvalidSelectionException();
			}
		}

		public static async Task FollowAsync<L, R, E, P>(this Session<Follow<L, R>, E, P> session, Action<Session<L, E, P>> leftAction, Action<Session<R, E, P>> rightAction) where L : SessionType where R : SessionType where E : SessionStack where P : ProtocolType
		{
			if (session is null) throw new ArgumentNullException(nameof(session));
			if (leftAction is null) throw new ArgumentNullException(nameof(leftAction));
			if (rightAction is null) throw new ArgumentNullException(nameof(rightAction));
			switch (await session.FollowAsync())
			{
				case Selection.Left:
					leftAction(session.ToNextSession<L>());
					break;
				case Selection.Right:
					rightAction(session.ToNextSession<R>());
					break;
				default:
					throw new InvalidSelectionException();
			}
		}

		public static async Task FollowAsync<L, R, E, P>(this Session<Follow<L, R>, E, P> session, Func<Session<L, E, P>, Task> leftAction, Action<Session<R, E, P>> rightAction) where L : SessionType where R : SessionType where E : SessionStack where P : ProtocolType
		{
			if (session is null) throw new ArgumentNullException(nameof(session));
			if (leftAction is null) throw new ArgumentNullException(nameof(leftAction));
			if (rightAction is null) throw new ArgumentNullException(nameof(rightAction));
			switch (await session.FollowAsync())
			{
				case Selection.Left:
					await leftAction(session.ToNextSession<L>());
					break;
				case Selection.Right:
					rightAction(session.ToNextSession<R>());
					break;
				default:
					throw new InvalidSelectionException();
			}
		}

		public static async Task FollowAsync<L, R, E, P>(this Session<Follow<L, R>, E, P> session, Action<Session<L, E, P>> leftAction, Func<Session<R, E, P>, Task> rightAction) where L : SessionType where R : SessionType where E : SessionStack where P : ProtocolType
		{
			if (session is null) throw new ArgumentNullException(nameof(session));
			if (leftAction is null) throw new ArgumentNullException(nameof(leftAction));
			if (rightAction is null) throw new ArgumentNullException(nameof(rightAction));
			switch (await session.FollowAsync())
			{
				case Selection.Left:
					leftAction(session.ToNextSession<L>());
					break;
				case Selection.Right:
					await rightAction(session.ToNextSession<R>());
					break;
				default:
					throw new InvalidSelectionException();
			}
		}

		public static async Task FollowAsync<L, R, E, P>(this Session<Follow<L, R>, E, P> session, Func<Session<L, E, P>, Task> leftAction, Func<Session<R, E, P>, Task> rightAction) where L : SessionType where R : SessionType where E : SessionStack where P : ProtocolType
		{
			if (session is null) throw new ArgumentNullException(nameof(session));
			if (leftAction is null) throw new ArgumentNullException(nameof(leftAction));
			if (rightAction is null) throw new ArgumentNullException(nameof(rightAction));
			switch (await session.FollowAsync())
			{
				case Selection.Left:
					await leftAction(session.ToNextSession<L>());
					break;
				case Selection.Right:
					await rightAction(session.ToNextSession<R>());
					break;
				default:
					throw new InvalidSelectionException();
			}
		}

		public static (Session<S, E, P> continuation, Session<Z, Empty, Q> newSession) ThrowNewChannel<S, E, P, Z, Q>(this Session<ThrowNewChannel<Z, Q, S>, E, P> session) where Z : SessionType where Q : ProtocolType where S : SessionType where E : SessionStack where P : ProtocolType
		{
			if (session is null) throw new ArgumentNullException(nameof(session));
			return (session.ToNextSession<S>(), session.ThrowNewChannel<Z, Q>());
		}

		public static Session<S, E, P> ThrowNewChannel<S, E, P, Z, Q>(this Session<ThrowNewChannel<Z, Q, S>, E, P> session, out Session<Z, Empty, Q> newSession) where Z : SessionType where Q : ProtocolType where S : SessionType where E : SessionStack where P : ProtocolType
		{
			if (session is null) throw new ArgumentNullException(nameof(session));
			newSession = session.ThrowNewChannel<Z, Q>();
			return session.ToNextSession<S>();
		}

		public static async Task<(Session<S, E, P> continuation, Session<Z, Empty, Q> newSession)> ThrowNewChannelAsync<S, E, P, Z, Q>(this Session<ThrowNewChannel<Z, Q, S>, E, P> session) where Z : SessionType where Q : ProtocolType where S : SessionType where E : SessionStack where P : ProtocolType
		{
			if (session is null) throw new ArgumentNullException(nameof(session));
			return (session.ToNextSession<S>(), await session.ThrowNewChannelAsync<Z, Q>());
		}

		public static (Session<S, E, P> continuation, Session<Z, Empty, Q> newSession) CatchNewChannel<S, E, P, Z, Q>(this Session<CatchNewChannel<Z, Q, S>, E, P> session) where Z : SessionType where Q : ProtocolType where S : SessionType where E : SessionStack where P : ProtocolType
		{
			if (session is null) throw new ArgumentNullException(nameof(session));
			return (session.ToNextSession<S>(), session.CatchNewChannel<Z, Q>());
		}

		public static Session<S, E, P> CatchNewChannel<S, E, P, Z, Q>(this Session<CatchNewChannel<Z, Q, S>, E, P> session, out Session<Z, Empty, Q> newSession) where Z : SessionType where Q : ProtocolType where S : SessionType where E : SessionStack where P : ProtocolType
		{
			if (session is null) throw new ArgumentNullException(nameof(session));
			newSession = session.CatchNewChannel<Z, Q>();
			return session.ToNextSession<S>();
		}

		public static async Task<(Session<S, E, P> continuation, Session<Z, Empty, Q> newSession)> CatchNewChannelAsync<S, E, P, Z, Q>(this Session<CatchNewChannel<Z, Q, S>, E, P> session) where Z : SessionType where Q : ProtocolType where S : SessionType where E : SessionStack where P : ProtocolType
		{
			if (session is null) throw new ArgumentNullException(nameof(session));
			return (session.ToNextSession<S>(), await session.CatchNewChannelAsync<Z, Q>());
		}

		public static void Close<P>(this Session<Eps, Empty, P> session) where P : ProtocolType
		{
			if (session is null) throw new ArgumentNullException(nameof(session));
			session.Close();
		}

		public static Task CloseAsync<P>(this Session<Eps, Empty, P> session) where P : ProtocolType
		{
			if (session is null) throw new ArgumentNullException(nameof(session));
			return session.CloseAsync();
		}
	}
}
