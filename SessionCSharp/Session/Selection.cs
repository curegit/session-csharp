namespace Session
{
	internal enum Selection : byte
	{
		Default = 0,
		Left = 1,
		Center = 2,
		Right = 3,
	}

	internal static class SelectionUtility
	{
		public static byte ToByte(this Selection selection)
		{
			return (byte)selection;
		}

		public static Selection ToSelection(this byte code)
		{
			return (Selection)code;
		}
	}
}
