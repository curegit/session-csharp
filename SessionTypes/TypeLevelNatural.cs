using System;

namespace SessionTypes
{
	public abstract class TypeLevelNatural : IEquatable<TypeLevelNatural>, IComparable<TypeLevelNatural>
	{
		public abstract int Evaluate();

		public override bool Equals(object obj)
		{
			return Equals(obj as TypeLevelNatural);
		}

		public bool Equals(TypeLevelNatural other)
		{
			return Evaluate().Equals(other.Evaluate());
		}

		public override int GetHashCode()
		{
			return Evaluate().GetHashCode();
		}

		public int CompareTo(TypeLevelNatural other)
		{
			return Evaluate().CompareTo(other.Evaluate());
		}

		public override string ToString()
		{
			return Evaluate().ToString();
		}

		public static bool operator ==(TypeLevelNatural n1, TypeLevelNatural n2)
		{
			return n1.Evaluate() == n2.Evaluate();
		}

		public static bool operator !=(TypeLevelNatural n1, TypeLevelNatural n2)
		{
			return n1.Evaluate() != n2.Evaluate();
		}

		public static bool operator <(TypeLevelNatural n1, TypeLevelNatural n2)
		{
			return n1.Evaluate() < n2.Evaluate();
		}

		public static bool operator >(TypeLevelNatural n1, TypeLevelNatural n2)
		{
			return n1.Evaluate() > n2.Evaluate();
		}

		public static bool operator <=(TypeLevelNatural n1, TypeLevelNatural n2)
		{
			return n1.Evaluate() <= n2.Evaluate();
		}

		public static bool operator >=(TypeLevelNatural n1, TypeLevelNatural n2)
		{
			return n1.Evaluate() >= n2.Evaluate();
		}
	}

	public sealed class Succ<N> : TypeLevelNatural where N : TypeLevelNatural, new()
	{
		public override int Evaluate()
		{
			checked
			{
				return 1 + new N().Evaluate();
			}
		}
	}

	public sealed class Zero : TypeLevelNatural
	{
		public override int Evaluate()
		{
			return 0;
		}
	}
}
