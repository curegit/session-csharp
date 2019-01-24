namespace SessionTypes.Common
{
	public abstract class TypeLevelNatural
	{

	}

	public sealed class Successor<N> : TypeLevelNatural where N : TypeLevelNatural
	{

	}

	public sealed class Zero : TypeLevelNatural
	{

	}
}
