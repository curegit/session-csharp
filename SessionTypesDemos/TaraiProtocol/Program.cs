using System;
using System.Threading.Tasks;
using SessionTypes;
using SessionTypes.Threading;

using System.Runtime.Serialization.Formatters.Binary;

namespace TaraiProtocol
{
	//using static Combinator<>;
	using static Portobuf;
	using static ProtocolCombinator;
	using static ProtocolCombinator<Protobuf>;
	//using static ProtocolCom

	namespace TaraiProtocol
	{
		public class TaraiProtocol
		{

		}

		public class TaraiProtocol2 : TaraiProtocol
		{

		}
	}


	public class Program
	{
		public static async Task Main(string[] args)
		{
			var protocol = C2S(P<int>, C2S(P<int>, C2S(P<int>, S2CAdd(C2S(P<bool>, End), AtS(S2C(P<int>, End), End)))));

			//protocol.

			var client = protocol.Fork(async server =>
			{
				var s1 = server.Receive(out var x).Receive(out var y).Receive(out var z);
				var s2 = s1.SendNewChannel(out var session);
				var calc = Task.Run(() => Tarai(x, y, z));
				var cancel = session.ReceiveAsync();
				int i = Task.WaitAny(calc, cancel);
				if (i == 0)
				{
					s2.SelectLeft().Send(calc.Result).Close();
					(await cancel).Bind(out var b).Close();
				}
				else
				{
					s2.SelectRight().Close();
					cancel.Result.Bind(out var b).Close();
				}

				
			});

			var c1 = client.Send(16).Send(3).Send(2);
			var c2 = c1.ReceiveNewChannel(out var ch);
			var ret = c2.FollowAsync(left =>
			{
				left.Receive(out var ans).Close();
				Console.WriteLine($"Tarai = {ans}");
			},
			right =>
			{
				right.Close();
				Console.WriteLine("Canceled");
			});
			var timeout = Task.Delay(20000);
			var task = await Task.WhenAny(ret, timeout);
			if (task == ret)
			{
				ch.Send(false).Close();
			}
			else
			{
				ch.Send(true).Close();
				await ret;
			}
		}

		private static int Tarai(int x, int y, int z)
		{
			return x <= y ? y : Tarai(Tarai(x - 1, y, z), Tarai(y - 1, z, x), Tarai(z - 1, x, y));
		}
	}
}
