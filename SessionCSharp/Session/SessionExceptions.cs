using System;
using System.Runtime.Serialization;

namespace Session
{
	[Serializable]
	public sealed class LinearityViolationException : InvalidOperationException
	{
		public LinearityViolationException() : base() { }

		public LinearityViolationException(string message) : base(message) { }

		public LinearityViolationException(string message, Exception inner) : base(message, inner) { }

		private LinearityViolationException(SerializationInfo info, StreamingContext context) : base(info, context) { }
	}

	[Serializable]
	public sealed class UnknownChoiceException : InvalidOperationException
	{
		public UnknownChoiceException() : base() { }

		public UnknownChoiceException(string message) : base(message) { }

		public UnknownChoiceException(string message, Exception inner) : base(message, inner) { }

		private UnknownChoiceException(SerializationInfo info, StreamingContext context) : base(info, context) { }
	}
}
