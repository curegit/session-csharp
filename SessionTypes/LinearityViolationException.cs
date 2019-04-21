using System;
using System.Runtime.Serialization;

namespace SessionTypes
{
	[Serializable]
	public sealed class LinearityViolationException : InvalidOperationException
	{
		public LinearityViolationException() : base() { }

		public LinearityViolationException(string message) : base(message) { }

		public LinearityViolationException(string message, Exception inner) : base(message, inner) { }

		private LinearityViolationException(SerializationInfo info, StreamingContext context) : base(info, context) { }
	}
}
