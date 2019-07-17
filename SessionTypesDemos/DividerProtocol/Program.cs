using System;
using SessionTypes.Binary;
using SessionTypes.Binary.Threading;

namespace DividerProtocol
{
	using DivProtocol = Req<int, Req<int, RespChoice<Resp<int, Eps>, Resp<string, Eps>>>>;

	public class Program
	{
		public static void Main(string[] args)
		{
			var client = BinaryChannel<DivProtocol>.Fork(server =>
			{
				var s = server.Receive(out var dividend).Receive(out var divisor);
				if (divisor != 0)
				{
					s.ChooseLeft().Send(dividend / divisor).Close();
				}
				else
				{
					s.ChooseRight().Send("Dividing by zero!").Close();
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
