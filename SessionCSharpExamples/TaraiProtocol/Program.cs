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
			var protocol = Send(Value<int>, Send(Value<int>, Send(Value<int>, ReceiveNewChannel(Send(Unit, End), Follow(Receive(Value<int>, End), End)))));

			var client = protocol.ForkThread(async server =>
			{
				static int Tarai(int x, int y, int z, Task waitForCancel)
				{
					if (waitForCancel.IsCompleted) throw new OperationCanceledException();
					if (x <= y) return y;
					return Tarai(Tarai(x - 1, y, z, waitForCancel), Tarai(y - 1, z, x, waitForCancel), Tarai(z - 1, x, y, waitForCancel), waitForCancel);
				}

				var s1 = server.Receive(out var x).Receive(out var y).Receive(out var z);
				var s2 = s1.SendNewChannel(out var channelForCancel);

				var waitForCancel = channelForCancel.ReceiveAsync();
				try
				{
					var result = Tarai(x, y, z, waitForCancel);
					s2.SelectLeft().Send(result).Close();
					(await waitForCancel).Close();
				}
				catch
				{
					s2.SelectRight().Close();
					(await waitForCancel).Close();
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
