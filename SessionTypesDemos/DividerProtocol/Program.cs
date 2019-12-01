using System;
using SessionTypes;
using SessionTypes.Threading;

namespace DividerProtocol
{
	using static Combinator;

	public class Program
	{
		public static void Main(string[] args)
		{
			var client = C2S(P<int>, C2S(P<int>, AtS(S2C(P<int>, End), S2C(P<string>, End)))).Fork(server =>
			{
				var s = server.Receive(out var dividend).Receive(out var divisor);
				if (divisor != 0)
				{
					s.SelectLeft().Send(dividend / divisor).Close();
				}
				else
				{
					s.SelectRight().Send("Dividing by zero!").Close();
				}
			});
			var c = client.Send(193).Send(13);
			c.Follow(left =>
			{
				left.Receive(out var quotient).Close();
				Console.WriteLine(quotient);
			},
			right =>
			{
				right.Receive(out var message).Close();
				Console.WriteLine(message);
			});
		}
	}
}
