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


			Thread.Sleep(1000);


			var d = new SessionDieList();
			try
			{
				

				var ch1 = sprotocol.ToTcpClient(IPAddress.Loopback, 54000).Connect();

				d.Add(ch1);

				ch1.Send(1).Send(2).Receive(out var x1).Close();

				Console.WriteLine(x1);
			}
			catch
			{
				d.Dispose();
			}
			Thread.Sleep(1000);

			using (var ds = new SessionDieList())
			{
				var ch2 = sprotocol.ToTcpClient(IPAddress.Loopback, 54000).Connect();
				ds.Add(ch2);
				var ch3 = sprotocol.ToTcpClient(IPAddress.Loopback, 54000).Connect();
				ds.Add(ch3);

				ch2.Send(1).Send(2).Receive(out var x2).Close();
				Console.WriteLine(x2);
				
				ch3.Send(2).Send(3).Receive(out var x3).Close();
				Console.WriteLine(x3);
			}
		}
	}
}
