using System;
using System.Threading.Tasks;

namespace Session
{
	public static class SessionInterface
	{
		public static S Send<S>(this Send<S> session) where S : Session
		{
			if (session is null) throw new ArgumentNullException(nameof(session));
			session.Send();
			return session.ToNextSession<S>();
		}

		public static S Send<S, T>(this Send<T, S> session, T value) where S : Session
		{
			if (session is null) throw new ArgumentNullException(nameof(session));
			session.Send(value);
			return session.ToNextSession<S>();
		}

		public static S SendAsync<S>(this Send<S> session) where S : Session 
		{
			if (session is null) throw new ArgumentNullException(nameof(session));
			session.SendAsync();
			return session.ToNextSession<S>();
		}

		public static S SendAsync<S>(this Send<S> session, out Task task) where S : Session 
		{
			if (session is null) throw new ArgumentNullException(nameof(session));
			task = session.SendAsync();
			return session.ToNextSession<S>();
		}

		public static S SendAsync<S, T>(this Send<T, S> session, T value) where S : Session 
		{
			if (session is null) throw new ArgumentNullException(nameof(session));
			session.SendAsync(value);
			return session.ToNextSession<S>();
		}

		public static S SendAsync<S, T>(this Send<T, S> session, T value, out Task task) where S : Session 
		{
			if (session is null) throw new ArgumentNullException(nameof(session));
			task = session.SendAsync(value);
			return session.ToNextSession<S>();
		}

		public static S Receive<S>(this Receive<S> session) where S : Session
		{
			if (session is null) throw new ArgumentNullException(nameof(session));
			session.Receive();
			return session.ToNextSession<S>();
		}

		public static S Receive<S, T>(this Receive<T, S> session, out T value) where S : Session 
		{
			if (session is null) throw new ArgumentNullException(nameof(session));
			value = session.Receive<T>();
			return session.ToNextSession<S>();
		}

		public static S Receive<S, T1, T2>(this Receive<(T1, T2), S> session, out T1 value1, out T2 value2) where S : Session 
		{
			if (session is null) throw new ArgumentNullException(nameof(session));
			(value1, value2) = session.Receive<(T1, T2)>();
			return session.ToNextSession<S>();
		}

		public static S Receive<S, T1, T2, T3>(this Receive<(T1, T2, T3), S> session, out T1 value1, out T2 value2, out T3 value3) where S : Session
		{
			if (session is null) throw new ArgumentNullException(nameof(session));
			(value1, value2, value3) = session.Receive<(T1, T2, T3)>();
			return session.ToNextSession<S>();
		}

		public static S Receive<S, T1, T2, T3, T4>(this Receive<(T1, T2, T3, T4), S> session, out T1 value1, out T2 value2, out T3 value3, out T4 value4) where S : Session 
		{
			if (session is null) throw new ArgumentNullException(nameof(session));
			(value1, value2, value3, value4) = session.Receive<(T1, T2, T3, T4)>();
			return session.ToNextSession<S>();
		}

		public static S Receive<S, T1, T2, T3, T4, T5>(this Receive<(T1, T2, T3, T4, T5), S> session, out T1 value1, out T2 value2, out T3 value3, out T4 value4, out T5 value5) where S : Session 
		{
			if (session is null) throw new ArgumentNullException(nameof(session));
			(value1, value2, value3, value4, value5) = session.Receive<(T1, T2, T3, T4, T5)>();
			return session.ToNextSession<S>();
		}

		public static S ReceiveAsync<S>(this Receive<S> session, out Task future) where S : Session 
		{
			if (session is null) throw new ArgumentNullException(nameof(session));
			future = session.ReceiveAsync();
			return session.ToNextSession<S>();
		}

		public static S ReceiveAsync<S, T>(this Receive<T, S> session, out Task<T> futureValue) where S : Session 
		{
			if (session is null) throw new ArgumentNullException(nameof(session));
			futureValue = session.ReceiveAsync<T>();
			return session.ToNextSession<S>();
		}

		public static L SelectLeft<L, R>(this Select<L, R> session) where L : Session where R : Session 
		{
			if (session is null) throw new ArgumentNullException(nameof(session));
			session.Select(Selection.Left);
			return session.ToNextSession<L>();
		}

		public static R SelectRight<L, R>(this Select<L, R> session) where L : Session where R : Session 
		{
			if (session is null) throw new ArgumentNullException(nameof(session));
			session.Select(Selection.Right);
			return session.ToNextSession<R>();
		}

		public static L SelectLeft<L, C, R>(this Select<L, C, R> session) where L : Session where C : Session where R : Session 
		{
			if (session is null) throw new ArgumentNullException(nameof(session));
			session.Select(Selection.Left);
			return session.ToNextSession<L>();
		}

		public static C SelectCenter<L, C, R>(this Select<L, C, R> session) where L : Session where C : Session where R : Session 
		{
			if (session is null) throw new ArgumentNullException(nameof(session));
			session.Select(Selection.Center);
			return session.ToNextSession<C>();
		}

		public static R SelectRight<L, C, R>(this Select<L, C, R> session) where L : Session where C : Session where R : Session 
		{
			if (session is null) throw new ArgumentNullException(nameof(session));
			session.Select(Selection.Right);
			return session.ToNextSession<R>();
		}

		public static L SelectLeftAsync<L, R>(this Select<L, R> session) where L : Session where R : Session 
		{
			if (session is null) throw new ArgumentNullException(nameof(session));
			session.SelectAsync(Selection.Left);
			return session.ToNextSession<L>();
		}

		public static L SelectLeftAsync<L, R>(this Select<L, R> session, out Task task) where L : Session where R : Session 
		{
			if (session is null) throw new ArgumentNullException(nameof(session));
			task = session.SelectAsync(Selection.Left);
			return session.ToNextSession<L>();
		}

		public static R SelectRightAsync<L, R>(this Select<L, R> session) where L : Session where R : Session 
		{
			if (session is null) throw new ArgumentNullException(nameof(session));
			session.SelectAsync(Selection.Right);
			return session.ToNextSession<R>();
		}

		public static R SelectRightAsync<L, R>(this Select<L, R> session, out Task task) where L : Session where R : Session 
		{
			if (session is null) throw new ArgumentNullException(nameof(session));
			task = session.SelectAsync(Selection.Right);
			return session.ToNextSession<R>();
		}

		public static L SelectLeftAsync<L, C, R>(this Select<L, C, R> session) where L : Session where C : Session where R : Session 
		{
			if (session is null) throw new ArgumentNullException(nameof(session));
			session.SelectAsync(Selection.Left);
			return session.ToNextSession<L>();
		}

		public static L SelectLeftAsync<L, C, R>(this Select<L, C, R> session, out Task task) where L : Session where C : Session where R : Session 
		{
			if (session is null) throw new ArgumentNullException(nameof(session));
			task = session.SelectAsync(Selection.Left);
			return session.ToNextSession<L>();
		}

		public static C SelectCenterAsync<L, C, R>(this Select<L, C, R> session) where L : Session where C : Session where R : Session 
		{
			if (session is null) throw new ArgumentNullException(nameof(session));
			session.SelectAsync(Selection.Center);
			return session.ToNextSession<C>();
		}

		public static C SelectCenterAsync<L, C, R>(this Select<L, C, R> session, out Task task) where L : Session where C : Session where R : Session 
		{
			if (session is null) throw new ArgumentNullException(nameof(session));
			task = session.SelectAsync(Selection.Center);
			return session.ToNextSession<C>();
		}

		public static R SelectRightAsync<L, C, R>(this Select<L, C, R> session) where L : Session where C : Session where R : Session 
		{
			if (session is null) throw new ArgumentNullException(nameof(session));
			session.SelectAsync(Selection.Right);
			return session.ToNextSession<R>();
		}

		public static R SelectRightAsync<L, C, R>(this Select<L, C, R> session, out Task task) where L : Session where C : Session where R : Session 
		{
			if (session is null) throw new ArgumentNullException(nameof(session));
			task = session.SelectAsync(Selection.Right);
			return session.ToNextSession<R>();
		}

		public static void Offer<L, R>(this Offer<L, R> session, Action<L> leftAction, Action<R> rightAction) where L : Session where R : Session
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

		public static T Offer<L, R, T>(this Offer<L, R> session, Func<L, T> leftFunc, Func<R, T> rightFunc) where L : Session where R : Session
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

		public static async Task OfferAsync<L, R>(this Offer<L, R> session, Action<L> leftAction, Action<R> rightAction) where L : Session where R : Session
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

		public static async Task<T> OfferAsync<L, R, T>(this Offer<L, R> session, Func<L, T> leftFunc, Func<R, T> rightFunc) where L : Session where R : Session
		{
			if (session is null) throw new ArgumentNullException(nameof(session));
			if (leftFunc is null) throw new ArgumentNullException(nameof(leftFunc));
			if (rightFunc is null) throw new ArgumentNullException(nameof(rightFunc));
			switch (await session.FollowAsync())
			{
				case Selection.Left:
					return leftFunc(session.ToNextSession<L>());
				case Selection.Right:
					return rightFunc(session.ToNextSession<R>());
				default:
					throw new InvalidSelectionException();
			}
		}

		public static void Offer<L, C, R>(this Offer<L, C, R> session, Action<L> leftAction, Action<C> centerAction, Action<R> rightAction) where L : Session where C : Session where R : Session
		{
			if (session is null) throw new ArgumentNullException(nameof(session));
			if (leftAction is null) throw new ArgumentNullException(nameof(leftAction));
			if (centerAction is null) throw new ArgumentNullException(nameof(centerAction));
			if (rightAction is null) throw new ArgumentNullException(nameof(rightAction));
			switch (session.Follow())
			{
				case Selection.Left:
					leftAction(session.ToNextSession<L>());
					break;
				case Selection.Center:
					centerAction(session.ToNextSession<C>());
					break;
				case Selection.Right:
					rightAction(session.ToNextSession<R>());
					break;
				default:
					throw new InvalidSelectionException();
			}
		}

		public static T Offer<L, C, R, T>(this Offer<L, C, R> session, Func<L, T> leftFunc, Func<C, T> centerFunc, Func<R, T> rightFunc) where L : Session where C : Session where R : Session 
		{
			if (session is null) throw new ArgumentNullException(nameof(session));
			if (leftFunc is null) throw new ArgumentNullException(nameof(leftFunc));
			if (centerFunc is null) throw new ArgumentNullException(nameof(centerFunc));
			if (rightFunc is null) throw new ArgumentNullException(nameof(rightFunc));
			switch (session.Follow())
			{
				case Selection.Left:
					return leftFunc(session.ToNextSession<L>());
				case Selection.Center:
					return centerFunc(session.ToNextSession<C>());
				case Selection.Right:
					return rightFunc(session.ToNextSession<R>());
				default:
					throw new InvalidSelectionException();
			}
		}

		public static async Task OfferAsync<L, C, R>(this Offer<L, C, R> session, Action<L> leftAction, Action<C> centerAction, Action<R> rightAction) where L : Session where C : Session where R : Session 
		{
			if (session is null) throw new ArgumentNullException(nameof(session));
			if (leftAction is null) throw new ArgumentNullException(nameof(leftAction));
			if (centerAction is null) throw new ArgumentNullException(nameof(centerAction));
			if (rightAction is null) throw new ArgumentNullException(nameof(rightAction));
			switch (await session.FollowAsync())
			{
				case Selection.Left:
					leftAction(session.ToNextSession<L>());
					break;
				case Selection.Center:
					centerAction(session.ToNextSession<C>());
					break;
				case Selection.Right:
					rightAction(session.ToNextSession<R>());
					break;
				default:
					throw new InvalidSelectionException();
			}
		}

		public static async Task<T> OfferAsync<L, C, R, T>(this Offer<L, C, R> session, Func<L, T> leftFunc, Func<C, T> centerFunc, Func<R, T> rightFunc) where L : Session where C : Session where R : Session 
		{
			if (session is null) throw new ArgumentNullException(nameof(session));
			if (leftFunc is null) throw new ArgumentNullException(nameof(leftFunc));
			if (centerFunc is null) throw new ArgumentNullException(nameof(centerFunc));
			if (rightFunc is null) throw new ArgumentNullException(nameof(rightFunc));
			switch (await session.FollowAsync())
			{
				case Selection.Left:
					return leftFunc(session.ToNextSession<L>());
				case Selection.Center:
					return centerFunc(session.ToNextSession<C>());
				case Selection.Right:
					return rightFunc(session.ToNextSession<R>());
				default:
					throw new InvalidSelectionException();
			}
		}

		public static S DelegSendNew<S, X, Y>(this DelegSend<X, Y, S> session, out Y newSession) where S : Session where X : Session where Y : Session
		{
			if (session is null) throw new ArgumentNullException(nameof(session));
			newSession = session.ThrowNewChannel<Y>();
			return session.ToNextSession<S>();
		}

		public static S DelegRecv<S, X>(this DelegRecv<X, S> session, out X newSession) where S : Session where X : Session
		{
			if (session is null) throw new ArgumentNullException(nameof(session));
			newSession = session.CatchNewChannel<X>();
			return session.ToNextSession<S>();
		}

		/*
		public static S ThrowNewChannel<S, E, P, Z, Q>(this ThrowNewChannel<Z, Q, S> session, out Session<Z, Empty, Q> newSession) where S : Session where E : SessionStack where P : SessionList where Z : SessionType where Q : SessionList
		{
			if (session is null) throw new ArgumentNullException(nameof(session));
			newSession = session.ThrowNewChannel<Z, Q>();
			return session.ToNextSession<S>();
		}

		public static S ThrowNewChannelAsync<S, E, P, Z, Q>(this ThrowNewChannel<Z, Q, S> session, out Task<Session<Z, Empty, Q>> futureNewSession) where S : Session where E : SessionStack where P : SessionList where Z : SessionType where Q : SessionList
		{
			if (session is null) throw new ArgumentNullException(nameof(session));
			futureNewSession = session.ThrowNewChannelAsync<Z, Q>();
			return session.ToNextSession<S>();
		}

		public static S CatchNewChannel<S, E, P, Z, Q>(this CatchNewChannel<Z, Q, S> session, out Session<Z, Empty, Q> newSession) where S : Session where E : SessionStack where P : SessionList where Z : SessionType where Q : SessionList
		{
			if (session is null) throw new ArgumentNullException(nameof(session));
			newSession = session.CatchNewChannel<Z, Q>();
			return session.ToNextSession<S>();
		}

		public static S CatchNewChannelAsync<S, E, P, Z, Q>(this CatchNewChannel<Z, Q, S> session, out Task<Session<Z, Empty, Q>> futureNewSession) where S : Session where E : SessionStack where P : SessionList where Z : SessionType where Q : SessionList
		{
			if (session is null) throw new ArgumentNullException(nameof(session));
			futureNewSession = session.CatchNewChannelAsync<Z, Q>();
			return session.ToNextSession<S>();
		}*/

		public static void Close(this Eps session)
		{
			if (session is null) throw new ArgumentNullException(nameof(session));
			session.Close();
		}

		public static Task CloseAsync(this Eps session)
		{
			if (session is null) throw new ArgumentNullException(nameof(session));
			return session.CloseAsync();
		}
	}
}
