using System;
using SessionTypes.Binary;
using SessionTypes.Binary.Threading;

namespace SummationProtocol
{
	using SumProtocol = Cons<ReqChoice<Req<int, Goto0>, Resp<int, Eps>>, Nil>;

	public class Program
	{
		public static void Main(string[] args)
		{
			var client = BinaryChannel<SumProtocol>.Fork(server =>
			{
				var sum = 0;
				var cont = true;
				var s = server.Enter();
				while (cont)
				{
					s.Follow(left =>
					{
						s = left.Receive(out var number).Jump();
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
			c = c.ChooseLeft().Send(44).Jump();
			c = c.ChooseLeft().Send(57).Jump();
			c = c.ChooseLeft().Send(83).Jump();
			c.ChooseRight().Receive(out var ans).Close();
			Console.WriteLine(ans);
		}
	}
}
