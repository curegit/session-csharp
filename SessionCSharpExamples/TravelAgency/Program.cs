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
        private static readonly int servicePort = 9991;

        private static readonly int agencyPort = 8886;

        public static void Main(string[] args)
        {
            // Travel Agency
            var prot_C_A = Select(Send(Val<string>, Receive(Val<decimal>, Select(Receive(Val<DateTime>, End), Goto0))), End);
            var prot_A_S = Send(Val<string>, Receive(Val<DateTime>, End));

            var sprot_C_A = prot_C_A.OnStream(new BasicSerializer());
            var sprot_A_S = prot_A_S.OnStream(new BasicSerializer());

            // Service
            sprot_A_S.ToTcpServer(IPAddress.Any, servicePort).Listen(
                ch1 => ch1.Receive(out var dest).Send(DateTime.Now).Close()
            );

            // Agency
            sprot_C_A.ToTcpServer(IPAddress.Any, agencyPort).Listen(
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
                                    var ch2 = sprot_A_S.CreateTcpClient().Connect(IPAddress.Loopback, servicePort);
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

            // Customer
            Console.WriteLine("Connecting...");
            var ch1 = sprot_C_A.CreateTcpClient().Connect(IPAddress.Loopback, agencyPort);

            Console.WriteLine("Connected");

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
                ch12.SelectRight().Goto().SelectRight().Close();

                Console.WriteLine("Too much expensive!");
            }
        }
    }
}
