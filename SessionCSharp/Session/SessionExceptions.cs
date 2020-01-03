using System;
using System.Runtime.Serialization;

namespace Session
{
	[Serializable]
	public sealed class LinearityException : InvalidOperationException
	{
		public LinearityException() : base() { }

		public LinearityException(string message) : base(message) { }

		public LinearityException(string message, Exception inner) : base(message, inner) { }

		private LinearityException(SerializationInfo info, StreamingContext context) : base(info, context) { }
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
