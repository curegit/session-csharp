using System;
using System.Net;
using Session;
using Session.Streaming;
using Session.Streaming.Net;
using Session.Streaming.Serializers;

namespace TravelAgency
{
	using static ProtocolCombinator;

	public class Program
	{
		public class Customer : Select<Send<string, Receive<decimal, Select<Receive<DateTime, Eps>, Customer>>>, Eps>
		{
			public Customer(Select<Send<string, Receive<decimal, Select<Receive<DateTime, Eps>, Customer>>>, Eps> copy) : base(copy) { }
		}

		public class Agency : Offer<Receive<string, Send<decimal, Offer<Send<DateTime, Eps>, Agency>>>, Eps>
		{
			public Agency(Offer<Receive<string, Send<decimal, Offer<Send<DateTime, Eps>, Agency>>>, Eps> copy) : base(copy) { }
		}

		public class DualCA : Dual<Customer, Agency>
		{
			public DualCA() : base(
				Recur(() => Select(Send(Val<string>, Receive(Val<decimal>, Select(Receive(Val<DateTime>, End), new DualCA()))), End), x => new Customer(x), x => new Agency(x))
			)
			{

			}
		}

		public static void Main(string[] args)
		{
			// Travel Agency
			//var prot_C_A = Select(Send(Val<string>, Receive(Val<decimal>, Select(Receive(Val<DateTime>, End), Goto0))), End);
			var prot_C_A = new DualCA();
			var prot_A_S = Send(Val<string>, Receive(Val<DateTime>, End));

			var sprot_C_A = prot_C_A.OnStream(new BinarySerializer());
			var sprot_A_S = prot_A_S.OnStream(new BinarySerializer());

			// Service
			sprot_A_S.ToTcpServer(IPAddress.Any, 9999).Listen(
				ch1 => ch1.Receive(out var dest).Send(DateTime.Now).Close()
				);

			// Agency
			sprot_C_A.ToTcpServer(IPAddress.Any, 8888).Listen(
				ch1 =>
				{
					using var c = new SessionCanceller();
					c.Register(ch1);

					for (var loop = true; loop;)
					{
						ch1.Offer(
							quote => quote.Receive(out var dest).Send(90.00m).Offer(
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
									ch1 = reject;
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

			// Customer
			var ch1 = sprot_C_A.CreateTcpClient().Connect(IPAddress.Loopback, 8888);

			var ch12 = ch1.SelectLeft().Send("London").Receive(out var price);
			if (price < 100)
			{
				ch12.SelectLeft().Receive(out var date).Close();

				Console.WriteLine("Accepted");
				Console.WriteLine(price);
				Console.WriteLine(date);
			}
			else
			{
				ch12.SelectRight().SelectRight().Close();

				Console.WriteLine("Too much expensive!");
			}
		}
	}
}
