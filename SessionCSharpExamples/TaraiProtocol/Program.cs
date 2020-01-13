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

			var client = protocol.ForkThread(async ch1 =>
			{
				static int Tarai(int x, int y, int z, Func<bool> cancelled)
				{
					if (cancelled()) throw new OperationCanceledException();
					if (x <= y) return y;
					return Tarai(Tarai(x - 1, y, z, cancelled), Tarai(y - 1, z, x, cancelled), Tarai(z - 1, x, y, cancelled), cancelled);
				}

				var ch2 = ch1.Receive(out var x).Receive(out var y).Receive(out var z);
				var ch3 = ch2.ThrowNewChannel(out var channelForCancel);

				var waitForCancel = channelForCancel.ReceiveAsync();
				try
				{
					var result = Tarai(x, y, z, () => waitForCancel.IsCompleted);
					ch3.SelectLeft().Send(result).Close();
				}
				catch
				{
					ch3.SelectRight().Close();
				}
				finally
				{
					(await waitForCancel).Close();
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
