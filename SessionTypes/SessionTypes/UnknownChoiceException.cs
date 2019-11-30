using System;
using System.Runtime.Serialization;

namespace SessionTypes
{
	[Serializable]
	public sealed class UnknownChoiceException : InvalidOperationException
	{
		public UnknownChoiceException() : base() { }

		public UnknownChoiceException(string message) : base(message) { }

		public UnknownChoiceException(string message, Exception inner) : base(message, inner) { }

		private UnknownChoiceException(SerializationInfo info, StreamingContext context) : base(info, context) { }
	}
}
