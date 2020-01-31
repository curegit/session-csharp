using System;
using System.Collections.Generic;
using System.Text;

namespace Session
{
	namespace Types
	{
		public class Protocol
		{ }
		public class SessionType : Protocol
		{ }
		public class SessionList<S0, S1> : Protocol
			where S0 : SessionType
			where S1 : SessionType
		{ }
		public class SessionList<S0, S1, S2> : Protocol
			where S0 : SessionType
			where S1 : SessionType
			where S2 : SessionType
		{ }
		public class SessionList<S0, S1, S2, S3> : Protocol
			where S0 : SessionType
			where S1 : SessionType
			where S2 : SessionType
			where S3 : SessionType
		{ }

		public class Send<V, S> : SessionType { }

		public class Recv<V, S> : SessionType { }

		public class Select<SL, SR> : SessionType { }

		public class Offer<SL, SR> : SessionType { }


		public class Eps : SessionType { }

		public class Goto0 : SessionType { }
		public class Goto1 : SessionType { }
		public class Goto2 : SessionType { }
		public class Goto3 : SessionType { }

		public class Deleg<S0, T0, S> : SessionType { }

		public class DelegRecv<S0, S> : SessionType { }

		public class Dual<S, T>
			where S : Protocol
			where T : Protocol
		{ }

		public class Val<V> { }

	}
}
