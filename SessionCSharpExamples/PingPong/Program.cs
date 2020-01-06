using System;
using Session;
using Session.Threading;

namespace PingPong
{
	using static ProtocolCombinator;

	public class Program
	{
		public static void Main(string[] args)
		{
			var ping = Select(Send(Unit, Goto1), End);
			var pong = Follow(Receive(Unit, Goto0), End);
			var protocol = Array(ping, pong);

			var client = protocol.ForkThread(ch =>
			{
				int counter = 0;
				for (var loop = true; loop; counter++)
				{
					ch.Follow(
						ping =>
						{
							var s = ping.Receive().Goto();
							if (counter < 3)
							{
								ch = s.SelectLeft().Send().Goto();
							}
							else
							{
								s.SelectRight().Close();
								loop = false;
							}
						},
						end => { end.Close(); loop = false; }
						);
				}
			});

			var loop = true;
			var random = new Random();
			while (loop)
			{
				if (random.NextDouble() < 0.8)
				{
					Console.WriteLine("Ping");
					client.SelectLeft().Send().Goto().Follow(
						pong =>
						{
							client = pong.Receive().Goto();
							Console.WriteLine("Pong");
						},
						end =>
						{
							end.Close();
							loop = false;
						}
					);
				}
				else
				{
					client.SelectRight().Close();
					loop = false;
				}
			}
		}
	}
}
