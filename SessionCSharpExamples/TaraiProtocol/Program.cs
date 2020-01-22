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
			void f(out int x, out int y)
			{
				x = 0;
				y = 0;
			}
			f(out int x, out int y);


			var protocol = Send(Val<Tuple<int,int,int>>, ThrowNewChannel(Send(Unit, End), Follow(Receive(Val<int>, End), End)));

			var client = protocol.ForkThread(ch1 =>
			{
				var ch2 = ch1.Receive(out var x, out var y, out var z).CatchNewChannel(out var channelForCancel);

				var waitForCancel = channelForCancel.ReceiveAsync(out Task futureCancel);
				try
				{
					var result = Tarai(x, y, z);
					ch2.SelectLeft().Send(result).Close();
				}
				catch (OperationCanceledException)
				{
					ch2.SelectRight().Close();
				}
				finally
				{
					waitForCancel.Result.Close();
				}

				int Tarai(int x, int y, int z)
				{
					if (futureCancel.IsCompleted) throw new OperationCanceledException();
					return x <= y ? y :
						Tarai(Tarai(x - 1, y, z), Tarai(y - 1, z, x), Tarai(z - 1, x, y));
				}
			});

			var c1 = client.Send(new Tuple<int,int,int>(16, 3, 2));
			var c2 = c1.ThrowNewChannel(out var ch);
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
