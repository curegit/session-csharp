using System;

namespace Session
{
	public class Dual<S, Z> where S : Session where Z : Session
	{
		internal Dual() { }

		public Dual(Dual<S, Z> dual)
		{
			if (dual == null) throw new ArgumentNullException(nameof(dual));
		}

		public Dual<Z, S> Swapped => new Dual<Z, S>();
	}
}
