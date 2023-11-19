using System;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace Session
{
    public static class SessionUtility
    {
        public static Session<S, E, P> Tap<S, E, P>(this Session<S, E, P> session, Action action) where S : SessionType where E : SessionStack where P : ProtocolType
        {
            ArgumentNullException.ThrowIfNull(session);
            session.CallSimply();
            action();
            return session.Duplicate();
        }

        public static Session<S, E, P> Let<S, E, P, T>(this Session<S, E, P> session, out T variable, T value) where S : SessionType where E : SessionStack where P : ProtocolType
        {
            ArgumentNullException.ThrowIfNull(session);
            session.CallSimply();
            variable = value;
            return session.Duplicate();
        }

        public static Session<S, E, P> Wait<S, E, P>(this Session<S, E, P> session) where S : SessionType where E : SessionStack where P : ProtocolType
        {
            ArgumentNullException.ThrowIfNull(session);
            session.CallSimply();
            session.WaitForLastTask();
            return session.Duplicate();
        }

        public static async Task<Session<S, E, P>> Sync<S, E, P>(this Session<S, E, P> session) where S : SessionType where E : SessionStack where P : ProtocolType
        {
            ArgumentNullException.ThrowIfNull(session);
            session.CallSimply();
            await session.AwaitLastTask();
            return session.Duplicate();
        }

        public static void ForEach<S>(this IEnumerable<S> sessions, Action<S> action) where S : Session
        {
            foreach (var session in sessions)
            {
                action(session);
            }
        }

        public static IEnumerable<T> Map<S, T>(this IEnumerable<S> sessions, Func<S, T> func) where S : Session
        {
            ArgumentNullException.ThrowIfNull(sessions);
            ArgumentNullException.ThrowIfNull(func);
            var results = new List<T>();
            foreach (var session in sessions) results.Add(func(session));
            return results;
        }

        public static (IEnumerable<U> ziped, IEnumerable<S> sessRest, IEnumerable<T> argRest) ZipWith<S, T, U>(this IEnumerable<S> sessions, IEnumerable<T> args, Func<S, T, U> func) where S : Session
        {
            var ss = new List<S>();
            var ts = new List<T>();
            var us = new List<U>();
            var se = sessions.GetEnumerator();
            var ae = args.GetEnumerator();
            while (true)
            {
                if (se.MoveNext())
                {
                    if (ae.MoveNext())
                    {
                        us.Add(func(se.Current, ae.Current));
                        continue;
                    }
                    else
                    {
                        ss.Add(se.Current);
                        while (se.MoveNext())
                        {
                            ss.Add(se.Current);
                        }
                        break;
                    }
                }
                else
                {
                    if (ae.MoveNext())
                    {
                        ts.Add(ae.Current);
                        while (ae.MoveNext())
                        {
                            ts.Add(ae.Current);
                        }
                        break;
                    }
                    else
                    {
                        break;
                    }
                }
            }
            return (us, ss, ts);
        }

        public static (IEnumerable<T1>, IEnumerable<T2>) Unzip<T1, T2>(this IEnumerable<(T1, T2)> ss)
        {
            ArgumentNullException.ThrowIfNull(ss);
            var results1 = new List<T1>();
            var results2 = new List<T2>();
            foreach (var s in ss)
            {
                results1.Add(s.Item1);
                results2.Add(s.Item2);
            }
            return (results1, results2);
        }
    }
}
