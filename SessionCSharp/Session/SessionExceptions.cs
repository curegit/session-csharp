using System;

namespace Session
{
	[Serializable]
	public sealed class LinearityViolationException : InvalidOperationException
	{
		internal LinearityViolationException() : base() { }

		internal LinearityViolationException(string message) : base(message) { }

		internal LinearityViolationException(string message, Exception inner) : base(message, inner) { }
	}

	[Serializable]
	public sealed class InvalidSelectionException : Exception
	{
		internal InvalidSelectionException() : base() { }

		internal InvalidSelectionException(string message) : base(message) { }

		internal InvalidSelectionException(string message, Exception inner) : base(message, inner) { }
	}
}
