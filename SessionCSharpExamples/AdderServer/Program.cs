using System;
using System.Net;
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
			var protocol = Send(Val<int>, Send(Val<int>, Receive(Val<int>, End)));

			var sp = protocol.OnStream(new BinarySerializer());

			sp.ToTcpServer(IPAddress.Any, 54000).Listen(ch =>
			{
				ch.Receive(out var x).Receive(out var y).Send(x + y).Close();
			});

			using var canceler = new SessionCanceller();

			var ch2 = sp.CreateTcpClient().Connect(IPAddress.Loopback, 54000);
			canceler.Register(ch2);
			var ch3 = sp.CreateTcpClient().Connect(IPAddress.Loopback, 54000);
			canceler.Register(ch3);

			ch2.Send(1).Send(2).Receive(out var x2).Close();
			Console.WriteLine(x2);

			ch3.Send(2).Send(3).Receive(out var x3).Close();
			Console.WriteLine(x3);
		}
	}
}
