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


			// Travel Agency
			var prot_C_A = Select(Send(Val<string>, Receive(Val<decimal>, Select(Receive(Val<DateTime>, End), Goto0))), End);
			var prot_A_S = Send(Val<string>, Receive(Val<DateTime>, End));

			var sprot_C_A = prot_C_A.OnStream(new BinarySerializer());
			var sprot_A_S = prot_A_S.OnStream(new BinarySerializer());

			sprot_C_A.ToTcpServer(IPAddress.Loopback, 8888).Listen(
				ch1 =>
				{
					using var c = new SessionCanceller();
					c.Register(ch1);

					for (var loop = true; loop;)
					{
						ch1.Follow(
							quote => quote.Receive(out var dest).Send(90.00m).Follow(
								accept =>
								{
									var ch2 = sprot_A_S.CreateTcpClient().Connect(IPAddress.Loopback, 9999);
									c.Register(ch2);


									ch2.Send(dest).Receive(out var date).Close();
									

									accept.Send(date).Close();

									loop = false;
								},
								reject =>
								{
									ch1 = reject.Goto();
								}
							),
							quit =>
							{
								quit.Close();
								loop = false;
							}
						);
					}
				}
			);
		}
	}
}
