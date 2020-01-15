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
				static int Tarai(int x, int y, int z, Task futureCancel)
				{
					if (futureCancel.IsCompleted) throw new OperationCanceledException();
					if (x <= y) return y;
					return Tarai(Tarai(x - 1, y, z, futureCancel), Tarai(y - 1, z, x, futureCancel), Tarai(z - 1, x, y, futureCancel), futureCancel);
				}

				var ch2 = ch1.Receive(out var x).Receive(out var y).Receive(out var z);
				var ch3 = ch2.ThrowNewChannel(out var channelForCancel);

				var waitForCancel = channelForCancel.ReceiveAsync(out var futureCancel);
				try
				{
					var result = Tarai(x, y, z, futureCancel);
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
			var timeout = Task.Delay(11500);
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
