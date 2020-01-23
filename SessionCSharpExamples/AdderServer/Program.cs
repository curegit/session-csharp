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

			var protocol = Send(Val<int>, Send(Val<int>, Receive(Val<int>, End)));

			var sp = protocol.OnStream(new BinarySerializer());

			sp.ToTcpServer(IPAddress.Any, 54000).Listen(ch =>
			{
				ch.Receive(out var x).Receive(out var y).Send(x + y).Close();
			});

			Thread.Sleep(3000);

			var ch = sp.CreateTcpClient(IPAddress.Loopback, 54000).Connect();

			ch.Send(1).Send(2).Receive(out var x).Close();

			Console.WriteLine(x);


			Thread.Sleep(1000);


			var canceller = new SessionCanceller();
			try
			{
				

				var ch1 = sp.CreateTcpClient(IPAddress.Loopback, 54000).Connect();

				canceller.Register(ch1);

				ch1.Send(1).Send(2).Receive(out var x1).Close();

				Console.WriteLine(x1);
			}
			finally
			{
				d.Dispose();
			}
			Thread.Sleep(1000);

			void A(out IDisposable d)
			{
				d = null;
			}

			using var canceler = new SessionCanceller();

			A(out  var a);

			var ch2 = sp.CreateTcpClient(IPAddress.Loopback, 54000).Connect();
			canceler.Register(ch2);
			var ch3 = sp.CreateTcpClient(IPAddress.Loopback, 54000).Connect();
			canceler.Register(ch3);

			ch2.Send(1).Send(2).Receive(out var x2).Close();
			Console.WriteLine(x2);
				
			ch3.Send(2).Send(3).Receive(out var x3).Close();
			Console.WriteLine(x3);
		}
	}
}
