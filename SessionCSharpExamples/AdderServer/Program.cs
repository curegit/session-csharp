using System;
using System.Net;
using System.Threading;
using Session;
using Session.Streaming;
using Session.Streaming.Net;
using Session.Streaming.Serializers;

namespace AdderServer
{
	using static ProtocolCombinator;

	public class Program
	{
		public static void Main(string[] args)
		{
			Console.WriteLine("Hello World!");

			var protocol = Send(Value<int>, Send(Value<int>, Receive(Value<int>, End)));

			var sprotocol = protocol.OnStream(new BinarySerializer());

			sprotocol.ToTcpServer(IPAddress.Any, 54000).Listen(ch =>
			{
				ch.Receive(out var x).Receive(out var y).Send(x + y).Close();
			});

			Thread.Sleep(3000);

			var ch = sprotocol.ToTcpClient(IPAddress.Loopback, 54000).Connect();

			ch.Send(1).Send(2).Receive(out var x).Close();

			Console.WriteLine(x);
		}
	}
}
