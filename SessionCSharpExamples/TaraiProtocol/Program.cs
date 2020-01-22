using System;
using System.Threading.Tasks;
using Session;
using Session.Threading;

namespace TaraiProtocol
{
	using static ProtocolCombinator;

	public class Program
	{
		public static async Task Main(string[] args)
		{
			var protocol = Send(Value<int>, Send(Value<int>, Send(Value<int>, CatchNewChannel(Send(Unit, End), Follow(Receive(Value<int>, End), End)))));

			var client = protocol.ForkThread(ch1 =>
			{
				

				var ch2 = ch1.Receive(out var x).Receive(out var y).Receive(out var z);
				var ch3 = ch2.ThrowNewChannel(out var channelForCancel);

				var waitForCancel = channelForCancel.ReceiveAsync(out var futureCancel);
				try
				{
					var result = Tak(x, y, z);
					ch3.SelectLeft().Send(result).Close();
				}
				catch (OperationCanceledException)
				{
					ch3.SelectRight().Close();
				}
				finally
				{
					waitForCancel.Result.Close();
				}

				int Tak(int x, int y, int z)
				{
					if (futureCancel.IsCompleted) throw new OperationCanceledException();
					if (x <= y) return y;
					return Tak(Tak(x - 1, y, z), Tak(y - 1, z, x), Tak(z - 1, x, y));
				}
			});

			var c1 = client.Send(16).Send(3).Send(2);
			var c2 = c1.CatchNewChannel(out var ch);
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
			var timeout = Task.Delay(10500);
			var task = await Task.WhenAny(ret, timeout);
			if (task == ret)
			{
				ch.Send().Close();
			}
			else
			{
				ch.Send().Close();
				await ret;
			}
		}
	}
}
