using System;
using SessionTypes;
using SessionTypes.Threading;

namespace StackProtocol
{
	using static ProtocolBuilder;

	public class Program
	{
		public static void Main(string[] args)
		{
			var p0 = Call1(End);
			var p1 = AtC(C2S(P<int>, Call1(S2C(P<int>, Goto1))), End);
			var protocol = SessionList(p0, p1);

			var client = protocol.Fork(server =>
			{
				server.Enter().Call((session, thisFunc) =>
				{
					return session.Follow(
						push => thisFunc(push.Receive(out var x).Call(thisFunc).Send(x).Goto(), thisFunc),
						end => end
					);
				}).Close();
			});

			var counter = 0;
			var random = new Random();
			client.Enter().Call((session, thisFunc) =>
			{
				if (random.NextDouble() < 0.5)
				{
					Console.WriteLine($"Push {counter}");
					var s = session.SelectLeft().Send(counter);
					counter++;
					var s2 = s.Call(thisFunc).Receive(out var x).Goto();
					Console.WriteLine($"Pop {x}");
					return thisFunc(s2, thisFunc);
				}
				else
				{
					return session.SelectRight();
				}
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
