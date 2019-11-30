using System;
using SessionTypes;
using SessionTypes.Threading;

namespace SummationProtocol
{
	using static ProtocolBuilder;

	public class Program
	{
		public static void Main(string[] args)
		{
			var client = SessionList(AtC(C2S(P<int>, Goto0), S2C(P<int>, End))).Fork(server =>
			{
				var sum = 0;
				var cont = true;
				
				var s = server.Enter();
				while (cont)
				{
					s.Follow(left =>
					{
						s = left.Receive(out var number).Goto();
						sum += number;
					},
					right =>
					{
						right.Send(sum).Close();
						cont = false;
					});
				}
			});
			var c = client.Enter();
			c = c.SelectLeft().Send(44).Goto();
			c = c.SelectLeft().Send(57).Goto();
			c = c.SelectLeft().Send(83).Goto();
			c.SelectRight().Receive(out var ans).Close();
			Console.WriteLine(ans);
		}
	}
}
