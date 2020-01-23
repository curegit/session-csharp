using System;
using System.Threading.Tasks;

namespace Session
{
	public delegate Session<Eps, Any, P> DepletionFunc<S, P>(Session<S, Any, P> session, DepletionFunc<S, P> depletion) where S : SessionType where P : ProtocolType;

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

		public static Session<S, E, P> ReceiveAsync<S, E, P, T>(this Session<Receive<T, S>, E, P> session, out Task<T> future) where S : SessionType where E : SessionStack where P : ProtocolType
		{
			if (session is null) throw new ArgumentNullException(nameof(session));
			future = session.ReceiveAsync<T>();
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

		public static Session<S, E, S> Goto<S, E>(this Session<Call0, E, S> session) where S : SessionType where E : SessionStack
		{
			if (session is null) throw new ArgumentNullException(nameof(session));
			session.CallSimply();
			return session.ToNextSession<S>();
		}

		public static Session<S0, E, Cons<S0, L>> Goto<S0, E, L>(this Session<Call0, E, Cons<S0, L>> session) where S0 : SessionType where E : SessionStack where L : SessionList
		{
			if (session is null) throw new ArgumentNullException(nameof(session));
			session.CallSimply();
			return session.ToNextSession<S0>();
		}

		public static Session<S1, E, Cons<S0, Cons<S1, L>>> Goto<S0, S1, E, L>(this Session<Call1, E, Cons<S0, Cons<S1, L>>> session) where S0 : SessionType where S1 : SessionType where E : SessionStack where L : SessionList
		{
			if (session is null) throw new ArgumentNullException(nameof(session));
			session.CallSimply();
			return session.ToNextSession<S1>();
		}

		public static Session<S2, E, Cons<S0, Cons<S1, Cons<S2, L>>>> Goto<S0, S1, S2, E, L>(this Session<Call2, E, Cons<S0, Cons<S1, Cons<S2, L>>>> session) where S0 : SessionType where S1 : SessionType where S2 : SessionType where E : SessionStack where L : SessionList
		{
			if (session is null) throw new ArgumentNullException(nameof(session));
			session.CallSimply();
			return session.ToNextSession<S2>();
		}

		public static Session<S3, E, Cons<S0, Cons<S1, Cons<S2, Cons<S3, L>>>>> Goto<S0, S1, S2, S3, E, L>(this Session<Call3, E, Cons<S0, Cons<S1, Cons<S2, Cons<S3, L>>>>> session) where S0 : SessionType where S1 : SessionType where S2 : SessionType where S3 : SessionType where E : SessionStack where L : SessionList
		{
			if (session is null) throw new ArgumentNullException(nameof(session));
			session.CallSimply();
			return session.ToNextSession<S3>();
		}

		public static Session<S4, E, Cons<S0, Cons<S1, Cons<S2, Cons<S3, Cons<S4, L>>>>>> Goto<S0, S1, S2, S3, S4, E, L>(this Session<Call4, E, Cons<S0, Cons<S1, Cons<S2, Cons<S3, Cons<S4, L>>>>>> session) where S0 : SessionType where S1 : SessionType where S2 : SessionType where S3 : SessionType where S4 : SessionType where E : SessionStack where L : SessionList
		{
			if (session is null) throw new ArgumentNullException(nameof(session));
			session.CallSimply();
			return session.ToNextSession<S4>();
		}

		public static Session<S5, E, Cons<S0, Cons<S1, Cons<S2, Cons<S3, Cons<S4, Cons<S5, L>>>>>>> Goto<S0, S1, S2, S3, S4, S5, E, L>(this Session<Call5, E, Cons<S0, Cons<S1, Cons<S2, Cons<S3, Cons<S4, Cons<S5, L>>>>>>> session) where S0 : SessionType where S1 : SessionType where S2 : SessionType where S3 : SessionType where S4 : SessionType where S5 : SessionType where E : SessionStack where L : SessionList
		{
			if (session is null) throw new ArgumentNullException(nameof(session));
			session.CallSimply();
			return session.ToNextSession<S5>();
		}

		public static Session<S6, E, Cons<S0, Cons<S1, Cons<S2, Cons<S3, Cons<S4, Cons<S5, Cons<S6, L>>>>>>>> Goto<S0, S1, S2, S3, S4, S5, S6, E, L>(this Session<Call6, E, Cons<S0, Cons<S1, Cons<S2, Cons<S3, Cons<S4, Cons<S5, Cons<S6, L>>>>>>>> session) where S0 : SessionType where S1 : SessionType where S2 : SessionType where S3 : SessionType where S4 : SessionType where S5 : SessionType where S6 : SessionType where E : SessionStack where L : SessionList
		{
			if (session is null) throw new ArgumentNullException(nameof(session));
			session.CallSimply();
			return session.ToNextSession<S6>();
		}

		public static Session<S7, E, Cons<S0, Cons<S1, Cons<S2, Cons<S3, Cons<S4, Cons<S5, Cons<S6, Cons<S7, L>>>>>>>>> Goto<S0, S1, S2, S3, S4, S5, S6, S7, E, L>(this Session<Call7, E, Cons<S0, Cons<S1, Cons<S2, Cons<S3, Cons<S4, Cons<S5, Cons<S6, Cons<S7, L>>>>>>>>> session) where S0 : SessionType where S1 : SessionType where S2 : SessionType where S3 : SessionType where S4 : SessionType where S5 : SessionType where S6 : SessionType where S7 : SessionType where E : SessionStack where L : SessionList
		{
			if (session is null) throw new ArgumentNullException(nameof(session));
			session.CallSimply();
			return session.ToNextSession<S7>();
		}

		public static Session<S8, E, Cons<S0, Cons<S1, Cons<S2, Cons<S3, Cons<S4, Cons<S5, Cons<S6, Cons<S7, Cons<S8, L>>>>>>>>>> Goto<S0, S1, S2, S3, S4, S5, S6, S7, S8, E, L>(this Session<Call8, E, Cons<S0, Cons<S1, Cons<S2, Cons<S3, Cons<S4, Cons<S5, Cons<S6, Cons<S7, Cons<S8, L>>>>>>>>>> session) where S0 : SessionType where S1 : SessionType where S2 : SessionType where S3 : SessionType where S4 : SessionType where S5 : SessionType where S6 : SessionType where S7 : SessionType where S8 : SessionType where E : SessionStack where L : SessionList
		{
			if (session is null) throw new ArgumentNullException(nameof(session));
			session.CallSimply();
			return session.ToNextSession<S8>();
		}

		public static Session<S9, E, Cons<S0, Cons<S1, Cons<S2, Cons<S3, Cons<S4, Cons<S5, Cons<S6, Cons<S7, Cons<S8, Cons<S9, L>>>>>>>>>>> Goto<S0, S1, S2, S3, S4, S5, S6, S7, S8, S9, E, L>(this Session<Call9, E, Cons<S0, Cons<S1, Cons<S2, Cons<S3, Cons<S4, Cons<S5, Cons<S6, Cons<S7, Cons<S8, Cons<S9, L>>>>>>>>>>> session) where S0 : SessionType where S1 : SessionType where S2 : SessionType where S3 : SessionType where S4 : SessionType where S5 : SessionType where S6 : SessionType where S7 : SessionType where S8 : SessionType where S9 : SessionType where E : SessionStack where L : SessionList
		{
			if (session is null) throw new ArgumentNullException(nameof(session));
			session.CallSimply();
			return session.ToNextSession<S9>();
		}

		public static Session<S, Push<Z, E>, S> Call<S, Z, E, L>(this Session<Call0<Z>, E, S> session) where S : SessionType where Z : SessionType where E : SessionStack
		{
			if (session is null) throw new ArgumentNullException(nameof(session));
			session.CallSimply();
			return session.ToNextSession<S, Push<Z, E>>();
		}

		public static Session<S0, Push<Z, E>, Cons<S0, L>> Call<S0, Z, E, L>(this Session<Call0<Z>, E, Cons<S0, L>> session) where S0 : SessionType where Z : SessionType where E : SessionStack where L : SessionList
		{
			if (session is null) throw new ArgumentNullException(nameof(session));
			session.CallSimply();
			return session.ToNextSession<S0, Push<Z, E>>();
		}

		public static Session<S1, Push<Z, E>, Cons<S0, Cons<S1, L>>> Call<S0, S1, Z, E, L>(this Session<Call1<Z>, E, Cons<S0, Cons<S1, L>>> session) where S0 : SessionType where S1 : SessionType where Z : SessionType where E : SessionStack where L : SessionList
		{
			if (session is null) throw new ArgumentNullException(nameof(session));
			session.CallSimply();
			return session.ToNextSession<S1, Push<Z, E>>();
		}

		public static Session<S2, Push<Z, E>, Cons<S0, Cons<S1, Cons<S2, L>>>> Call<S0, S1, S2, Z, E, L>(this Session<Call2<Z>, E, Cons<S0, Cons<S1, Cons<S2, L>>>> session) where S0 : SessionType where S1 : SessionType where S2 : SessionType where Z : SessionType where E : SessionStack where L : SessionList
		{
			if (session is null) throw new ArgumentNullException(nameof(session));
			session.CallSimply();
			return session.ToNextSession<S2, Push<Z, E>>();
		}

		public static Session<S3, Push<Z, E>, Cons<S0, Cons<S1, Cons<S2, Cons<S3, L>>>>> Call<S0, S1, S2, S3, Z, E, L>(this Session<Call3<Z>, E, Cons<S0, Cons<S1, Cons<S2, Cons<S3, L>>>>> session) where S0 : SessionType where S1 : SessionType where S2 : SessionType where S3 : SessionType where Z : SessionType where E : SessionStack where L : SessionList
		{
			if (session is null) throw new ArgumentNullException(nameof(session));
			session.CallSimply();
			return session.ToNextSession<S3, Push<Z, E>>();
		}

		public static Session<S4, Push<Z, E>, Cons<S0, Cons<S1, Cons<S2, Cons<S3, Cons<S4, L>>>>>> Call<S0, S1, S2, S3, S4, Z, E, L>(this Session<Call4<Z>, E, Cons<S0, Cons<S1, Cons<S2, Cons<S3, Cons<S4, L>>>>>> session) where S0 : SessionType where S1 : SessionType where S2 : SessionType where S3 : SessionType where S4 : SessionType where Z : SessionType where E : SessionStack where L : SessionList
		{
			if (session is null) throw new ArgumentNullException(nameof(session));
			session.CallSimply();
			return session.ToNextSession<S4, Push<Z, E>>();
		}

		public static Session<S5, Push<Z, E>, Cons<S0, Cons<S1, Cons<S2, Cons<S3, Cons<S4, Cons<S5, L>>>>>>> Call<S0, S1, S2, S3, S4, S5, Z, E, L>(this Session<Call5<Z>, E, Cons<S0, Cons<S1, Cons<S2, Cons<S3, Cons<S4, Cons<S5, L>>>>>>> session) where S0 : SessionType where S1 : SessionType where S2 : SessionType where S3 : SessionType where S4 : SessionType where S5 : SessionType where Z : SessionType where E : SessionStack where L : SessionList
		{
			if (session is null) throw new ArgumentNullException(nameof(session));
			session.CallSimply();
			return session.ToNextSession<S5, Push<Z, E>>();
		}

		public static Session<S6, Push<Z, E>, Cons<S0, Cons<S1, Cons<S2, Cons<S3, Cons<S4, Cons<S5, Cons<S6, L>>>>>>>> Call<S0, S1, S2, S3, S4, S5, S6, Z, E, L>(this Session<Call6<Z>, E, Cons<S0, Cons<S1, Cons<S2, Cons<S3, Cons<S4, Cons<S5, Cons<S6, L>>>>>>>> session) where S0 : SessionType where S1 : SessionType where S2 : SessionType where S3 : SessionType where S4 : SessionType where S5 : SessionType where S6 : SessionType where Z : SessionType where E : SessionStack where L : SessionList
		{
			if (session is null) throw new ArgumentNullException(nameof(session));
			session.CallSimply();
			return session.ToNextSession<S6, Push<Z, E>>();
		}

		public static Session<S7, Push<Z, E>, Cons<S0, Cons<S1, Cons<S2, Cons<S3, Cons<S4, Cons<S5, Cons<S6, Cons<S7, L>>>>>>>>> Call<S0, S1, S2, S3, S4, S5, S6, S7, Z, E, L>(this Session<Call7<Z>, E, Cons<S0, Cons<S1, Cons<S2, Cons<S3, Cons<S4, Cons<S5, Cons<S6, Cons<S7, L>>>>>>>>> session) where S0 : SessionType where S1 : SessionType where S2 : SessionType where S3 : SessionType where S4 : SessionType where S5 : SessionType where S6 : SessionType where S7 : SessionType where Z : SessionType where E : SessionStack where L : SessionList
		{
			if (session is null) throw new ArgumentNullException(nameof(session));
			session.CallSimply();
			return session.ToNextSession<S7, Push<Z, E>>();
		}

		public static Session<S8, Push<Z, E>, Cons<S0, Cons<S1, Cons<S2, Cons<S3, Cons<S4, Cons<S5, Cons<S6, Cons<S7, Cons<S8, L>>>>>>>>>> Call<S0, S1, S2, S3, S4, S5, S6, S7, S8, Z, E, L>(this Session<Call8<Z>, E, Cons<S0, Cons<S1, Cons<S2, Cons<S3, Cons<S4, Cons<S5, Cons<S6, Cons<S7, Cons<S8, L>>>>>>>>>> session) where S0 : SessionType where S1 : SessionType where S2 : SessionType where S3 : SessionType where S4 : SessionType where S5 : SessionType where S6 : SessionType where S7 : SessionType where S8 : SessionType where Z : SessionType where E : SessionStack where L : SessionList
		{
			if (session is null) throw new ArgumentNullException(nameof(session));
			session.CallSimply();
			return session.ToNextSession<S8, Push<Z, E>>();
		}

		public static Session<S9, Push<Z, E>, Cons<S0, Cons<S1, Cons<S2, Cons<S3, Cons<S4, Cons<S5, Cons<S6, Cons<S7, Cons<S8, Cons<S9, L>>>>>>>>>>> Call<S0, S1, S2, S3, S4, S5, S6, S7, S8, S9, Z, E, L>(this Session<Call9<Z>, E, Cons<S0, Cons<S1, Cons<S2, Cons<S3, Cons<S4, Cons<S5, Cons<S6, Cons<S7, Cons<S8, Cons<S9, L>>>>>>>>>>> session) where S0 : SessionType where S1 : SessionType where S2 : SessionType where S3 : SessionType where S4 : SessionType where S5 : SessionType where S6 : SessionType where S7 : SessionType where S8 : SessionType where S9 : SessionType where Z : SessionType where E : SessionStack where L : SessionList
		{
			if (session is null) throw new ArgumentNullException(nameof(session));
			session.CallSimply();
			return session.ToNextSession<S9, Push<Z, E>>();
		}

		public static Session<Z, E, S> Call<S, Z, E, L>(this Session<Call0<Z>, E, S> session, DepletionFunc<S, S> depletion) where S : SessionType where Z : SessionType where E : SessionStack where L : SessionList
		{
			if (session is null) throw new ArgumentNullException(nameof(session));
			if (depletion is null) throw new ArgumentNullException(nameof(depletion));
			session.CallSimply();
			var depleted = depletion(session.ToNextSession<S, Any>(), depletion);
			depleted.CallSimply();
			return depleted.ToNextSession<Z, E>();
		}

		public static Session<Z, E, Cons<S0, L>> Call<S0, Z, E, L>(this Session<Call0<Z>, E, Cons<S0, L>> session, DepletionFunc<S0, Cons<S0, L>> depletion) where S0 : SessionType where Z : SessionType where E : SessionStack where L : SessionList
		{
			if (session is null) throw new ArgumentNullException(nameof(session));
			if (depletion is null) throw new ArgumentNullException(nameof(depletion));
			session.CallSimply();
			var depleted = depletion(session.ToNextSession<S0, Any>(), depletion);
			depleted.CallSimply();
			return depleted.ToNextSession<Z, E>();
		}

		public static Session<Z, E, Cons<S0, Cons<S1, L>>> Call<S0, S1, Z, E, L>(this Session<Call1<Z>, E, Cons<S0, Cons<S1, L>>> session, DepletionFunc<S1, Cons<S0, Cons<S1, L>>> depletion) where S0 : SessionType where S1 : SessionType where Z : SessionType where E : SessionStack where L : SessionList
		{
			if (session is null) throw new ArgumentNullException(nameof(session));
			if (depletion is null) throw new ArgumentNullException(nameof(depletion));
			session.CallSimply();
			var depleted = depletion(session.ToNextSession<S1, Any>(), depletion);
			depleted.CallSimply();
			return depleted.ToNextSession<Z, E>();
		}

		public static Session<Z, E, Cons<S0, Cons<S1, Cons<S2, L>>>> Call<S0, S1, S2, Z, E, L>(this Session<Call2<Z>, E, Cons<S0, Cons<S1, Cons<S2, L>>>> session, DepletionFunc<S2, Cons<S0, Cons<S1, Cons<S2, L>>>> depletion) where S0 : SessionType where S1 : SessionType where S2 : SessionType where Z : SessionType where E : SessionStack where L : SessionList
		{
			if (session is null) throw new ArgumentNullException(nameof(session));
			if (depletion is null) throw new ArgumentNullException(nameof(depletion));
			session.CallSimply();
			var depleted = depletion(session.ToNextSession<S2, Any>(), depletion);
			depleted.CallSimply();
			return depleted.ToNextSession<Z, E>();
		}

		public static Session<Z, E, Cons<S0, Cons<S1, Cons<S2, Cons<S3, L>>>>> Call<S0, S1, S2, S3, Z, E, L>(this Session<Call3<Z>, E, Cons<S0, Cons<S1, Cons<S2, Cons<S3, L>>>>> session, DepletionFunc<S3, Cons<S0, Cons<S1, Cons<S2, Cons<S3, L>>>>> depletion) where S0 : SessionType where S1 : SessionType where S2 : SessionType where S3 : SessionType where Z : SessionType where E : SessionStack where L : SessionList
		{
			if (session is null) throw new ArgumentNullException(nameof(session));
			if (depletion is null) throw new ArgumentNullException(nameof(depletion));
			session.CallSimply();
			var depleted = depletion(session.ToNextSession<S3, Any>(), depletion);
			depleted.CallSimply();
			return depleted.ToNextSession<Z, E>();
		}

		public static Session<Z, E, Cons<S0, Cons<S1, Cons<S2, Cons<S3, Cons<S4, L>>>>>> Call<S0, S1, S2, S3, S4, Z, E, L>(this Session<Call4<Z>, E, Cons<S0, Cons<S1, Cons<S2, Cons<S3, Cons<S4, L>>>>>> session, DepletionFunc<S4, Cons<S0, Cons<S1, Cons<S2, Cons<S3, Cons<S4, L>>>>>> depletion) where S0 : SessionType where S1 : SessionType where S2 : SessionType where S3 : SessionType where S4 : SessionType where Z : SessionType where E : SessionStack where L : SessionList
		{
			if (session is null) throw new ArgumentNullException(nameof(session));
			if (depletion is null) throw new ArgumentNullException(nameof(depletion));
			session.CallSimply();
			var depleted = depletion(session.ToNextSession<S4, Any>(), depletion);
			depleted.CallSimply();
			return depleted.ToNextSession<Z, E>();
		}

		public static Session<Z, E, Cons<S0, Cons<S1, Cons<S2, Cons<S3, Cons<S4, Cons<S5, L>>>>>>> Call<S0, S1, S2, S3, S4, S5, Z, E, L>(this Session<Call5<Z>, E, Cons<S0, Cons<S1, Cons<S2, Cons<S3, Cons<S4, Cons<S5, L>>>>>>> session, DepletionFunc<S5, Cons<S0, Cons<S1, Cons<S2, Cons<S3, Cons<S4, Cons<S5, L>>>>>>> depletion) where S0 : SessionType where S1 : SessionType where S2 : SessionType where S3 : SessionType where S4 : SessionType where S5 : SessionType where Z : SessionType where E : SessionStack where L : SessionList
		{
			if (session is null) throw new ArgumentNullException(nameof(session));
			if (depletion is null) throw new ArgumentNullException(nameof(depletion));
			session.CallSimply();
			var depleted = depletion(session.ToNextSession<S5, Any>(), depletion);
			depleted.CallSimply();
			return depleted.ToNextSession<Z, E>();
		}

		public static Session<Z, E, Cons<S0, Cons<S1, Cons<S2, Cons<S3, Cons<S4, Cons<S5, Cons<S6, L>>>>>>>> Call<S0, S1, S2, S3, S4, S5, S6, Z, E, L>(this Session<Call6<Z>, E, Cons<S0, Cons<S1, Cons<S2, Cons<S3, Cons<S4, Cons<S5, Cons<S6, L>>>>>>>> session, DepletionFunc<S6, Cons<S0, Cons<S1, Cons<S2, Cons<S3, Cons<S4, Cons<S5, Cons<S6, L>>>>>>>> depletion) where S0 : SessionType where S1 : SessionType where S2 : SessionType where S3 : SessionType where S4 : SessionType where S5 : SessionType where S6 : SessionType where Z : SessionType where E : SessionStack where L : SessionList
		{
			if (session is null) throw new ArgumentNullException(nameof(session));
			if (depletion is null) throw new ArgumentNullException(nameof(depletion));
			session.CallSimply();
			var depleted = depletion(session.ToNextSession<S6, Any>(), depletion);
			depleted.CallSimply();
			return depleted.ToNextSession<Z, E>();
		}

		public static Session<Z, E, Cons<S0, Cons<S1, Cons<S2, Cons<S3, Cons<S4, Cons<S5, Cons<S6, Cons<S7, L>>>>>>>>> Call<S0, S1, S2, S3, S4, S5, S6, S7, Z, E, L>(this Session<Call7<Z>, E, Cons<S0, Cons<S1, Cons<S2, Cons<S3, Cons<S4, Cons<S5, Cons<S6, Cons<S7, L>>>>>>>>> session, DepletionFunc<S7, Cons<S0, Cons<S1, Cons<S2, Cons<S3, Cons<S4, Cons<S5, Cons<S6, Cons<S7, L>>>>>>>>> depletion) where S0 : SessionType where S1 : SessionType where S2 : SessionType where S3 : SessionType where S4 : SessionType where S5 : SessionType where S6 : SessionType where S7 : SessionType where Z : SessionType where E : SessionStack where L : SessionList
		{
			if (session is null) throw new ArgumentNullException(nameof(session));
			if (depletion is null) throw new ArgumentNullException(nameof(depletion));
			session.CallSimply();
			var depleted = depletion(session.ToNextSession<S7, Any>(), depletion);
			depleted.CallSimply();
			return depleted.ToNextSession<Z, E>();
		}

		public static Session<Z, E, Cons<S0, Cons<S1, Cons<S2, Cons<S3, Cons<S4, Cons<S5, Cons<S6, Cons<S7, Cons<S8, L>>>>>>>>>> Call<S0, S1, S2, S3, S4, S5, S6, S7, S8, Z, E, L>(this Session<Call8<Z>, E, Cons<S0, Cons<S1, Cons<S2, Cons<S3, Cons<S4, Cons<S5, Cons<S6, Cons<S7, Cons<S8, L>>>>>>>>>> session, DepletionFunc<S8, Cons<S0, Cons<S1, Cons<S2, Cons<S3, Cons<S4, Cons<S5, Cons<S6, Cons<S7, Cons<S8, L>>>>>>>>>> depletion) where S0 : SessionType where S1 : SessionType where S2 : SessionType where S3 : SessionType where S4 : SessionType where S5 : SessionType where S6 : SessionType where S7 : SessionType where S8 : SessionType where Z : SessionType where E : SessionStack where L : SessionList
		{
			if (session is null) throw new ArgumentNullException(nameof(session));
			if (depletion is null) throw new ArgumentNullException(nameof(depletion));
			session.CallSimply();
			var depleted = depletion(session.ToNextSession<S8, Any>(), depletion);
			depleted.CallSimply();
			return depleted.ToNextSession<Z, E>();
		}

		public static Session<Z, E, Cons<S0, Cons<S1, Cons<S2, Cons<S3, Cons<S4, Cons<S5, Cons<S6, Cons<S7, Cons<S8, Cons<S9, L>>>>>>>>>>> Call<S0, S1, S2, S3, S4, S5, S6, S7, S8, S9, Z, E, L>(this Session<Call9<Z>, E, Cons<S0, Cons<S1, Cons<S2, Cons<S3, Cons<S4, Cons<S5, Cons<S6, Cons<S7, Cons<S8, Cons<S9, L>>>>>>>>>>> session, DepletionFunc<S9, Cons<S0, Cons<S1, Cons<S2, Cons<S3, Cons<S4, Cons<S5, Cons<S6, Cons<S7, Cons<S8, Cons<S9, L>>>>>>>>>>> depletion) where S0 : SessionType where S1 : SessionType where S2 : SessionType where S3 : SessionType where S4 : SessionType where S5 : SessionType where S6 : SessionType where S7 : SessionType where S8 : SessionType where S9 : SessionType where Z : SessionType where E : SessionStack where L : SessionList
		{
			if (session is null) throw new ArgumentNullException(nameof(session));
			if (depletion is null) throw new ArgumentNullException(nameof(depletion));
			session.CallSimply();
			var depleted = depletion(session.ToNextSession<S9, Any>(), depletion);
			depleted.CallSimply();
			return depleted.ToNextSession<Z, E>();
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

		public static Session<F, E, P> Return<F, E, P>(this Session<Eps, Push<F, E>, P> session) where F : SessionType where E : SessionStack where P : ProtocolType
		{
			if (session is null) throw new ArgumentNullException(nameof(session));
			return session.ToNextSession<F, E>();
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
