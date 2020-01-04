using System;
using System.IO;
using Google.Protobuf;
using Google.Protobuf.Reflection;

namespace Session.Streaming.Serializers
{
	public sealed class ProtobufSerializer : ISerializer
	{
		public ProtobufSerializer() { }

		public void Serialize<T>(Stream stream, T value)
		{
			throw new NotImplementedException();
			//var a = new Google.Protobuf.MessageParser<int>();
			//a.ParseFrom(,)
		}

		public T Deserialize<T>(Stream stream)
		{
			throw new NotImplementedException();
			//Google.Protobuf.MessageParser<T>
			//(IMessage<T>)T;
		}
	}
}
