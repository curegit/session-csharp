using System;
using System.Linq;
using System.Threading.Tasks;
using Session;
using Session.Threading;

namespace BitcoinNonceCalculator
{
	using static ProtocolCombinator;

	public class Program
	{
		public static void Main()
		{
			// Protocol specification
			var protocol = Select(Send(Val<Block>, CatchNewChannel(Send(Unit, End),
				Follow(Receive(Val<uint>, Goto0), Goto0))), End);

			// N-threads
			var chs = protocol.Parallel(Environment.ProcessorCount / 2, ch1 =>
			{
				// 
				for (var loop = true; loop;)
				{
					ch1.Follow
					(
						cont =>
						{
							// 
							var ch2 = cont.Receive(out var block).ThrowNewChannel(out var cancelCh);
							//
							cancelCh.ReceiveAsync(out var cancelTask).CloseAsync();
							// 
							var miner = new Miner(block);
							//
							while (true)
							{
								if (miner.TestRandomNonce(out var nonce))
								{
									ch1 = ch2.SelectLeft().Send(nonce).Goto();
									break;
								}
								else if (cancelTask.IsCompleted)
								{
									ch1 = ch2.SelectRight().Goto();
									break;
								}
							}
						},
						end =>
						{
							end.Close();
							loop = false;
						}
					);
				}
			});

			// 
			foreach (var block in Block.GetSampleBlocks())
			{
				var ch3 = chs.Select(ch =>
				{

					var followTask = ch.SelectLeft().SendAsync(block).CatchNewChannelAsync(out var cancalCh).FollowAsync
					(
						some =>
						{
							var ch2 = some.Receive(out var nonce).Goto();
							return (ch2, nonce);
						},
						none => (none.Goto(), default(uint?))
					);

					return (followTask, cancalCh);
				}).ToArray();


				var ch4 = ch3.Select(c => c.followTask).ToArray();
				var cans = ch3.Select(c => c.cancalCh).ToArray();

				Task.WhenAny(ch4).Wait();



				cans.Select(c => { c.Result.Send().Close(); return 0; }).ToArray();

				foreach (var ccc in ch4)
				{
					var (e, n) = ccc.Result;
					Console.WriteLine(n);


				}

				chs = ch4.Select(c => c.Result.Item1).ToArray();
			}

			chs.Select(c => c.SelectRight()).ToArray();
		}
	}
}
