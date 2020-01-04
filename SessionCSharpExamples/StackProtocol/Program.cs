using System;
using Session;
using Session.Threading;

namespace StackProtocol
{
	using static ProtocolCombinator;

	public class Program
	{
		public static void Main(string[] args)
		{
			var entry = Call1(End);
			var node = Select(Send(Value<int>, Call1(Receive(Value<int>, Goto1))), End);
			var protocol = Array(entry, node);

			var client = protocol.ForkThread(server =>
			{
				server.Call((session, func) => session.Follow(
					push => func(push.Receive(out var x).Call(func).Send(x).Goto(), func),
					end => end)
				).Close();
			});

			var counter = 0;
			var random = new Random();
			client.Call((session, func) =>
			{
				if (random.NextDouble() < 0.5)
				{
					Console.WriteLine($"Push {counter}");
					var s = session.SelectLeft().Send(counter);
					counter++;
					var s2 = s.Call(func).Receive(out var x).Goto();
					Console.WriteLine($"Pop {x}");
					return func(s2, func);
				}
				else return session.SelectRight();
			}).Close();
		}

		/*
		Hello World!
		Push 0
		Pop 0
		Push 1
		Pop 1
		Push 2
		Push 3
		Push 4
		Push 5
		Push 6
		Pop 6
		Push 7
		Push 8
		Pop 8
		Pop 7
		Pop 5
		Push 9
		Pop 9
		Push 10
		Push 11
		Pop 11
		Pop 10
		Pop 4
		Push 12
		Pop 12
		Pop 3
		Pop 2
		*/
	}
}
