namespace Session
{
    namespace Types
    {
        public sealed class Unit
        {
            public static Unit unit = new Unit();
        }

        public class Protocol
        { }
        public class SessionType : Protocol
        { }
        public class SessionEnv : Protocol
        { }

        /// <summary>
        /// A utility class for SessionList<S0,..> which let pick the first out of the list.
        /// Will be removed -- this was only for the sake of ForkThread, which doesn't work in this way.
        /// </summary>
        public class EnvHead<S0> : SessionEnv
            where S0 : SessionType
        { }

        public class Env<S0, S1> : EnvHead<S0>
            where S0 : SessionType
            where S1 : SessionType
        { }
        public class Env<S0, S1, S2> : EnvHead<S0>
            where S0 : SessionType
            where S1 : SessionType
            where S2 : SessionType
        { }
        public class Env<S0, S1, S2, S3> : EnvHead<S0>
            where S0 : SessionType
            where S1 : SessionType
            where S2 : SessionType
            where S3 : SessionType
        { }

        public class Send<V, S> : SessionType { }

        public class Recv<V, S> : SessionType { }

        public class Select<SL, SR> : SessionType { }
        public class Select<SL, SM, SR> : SessionType { }
        public class Select<S0, S1, S2, S3> : SessionType { }

        public class Offer<SL, SR> : SessionType { }
        public class Offer<SL, SM, SR> : SessionType { }
        public class Offer<S0, S1, S2, S3> : SessionType { }

        public class Eps : SessionType { }

        public class Goto0 : SessionType { }
        public class Goto1 : SessionType { }
        public class Goto2 : SessionType { }
        public class Goto3 : SessionType { }

        public class Deleg<S0, T0, S> : SessionType { }

        public class DelegRecv<S0, S> : SessionType { }

        public class Dual<S, T>
            where S : SessionType
            where T : SessionType
        { }
        public class DualEnv<S, T>
            where S : SessionEnv
            where T : SessionEnv
        { }

        public class Val<V> { }

    }
}
