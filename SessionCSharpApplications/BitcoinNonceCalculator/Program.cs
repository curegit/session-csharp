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
		public static async Task Main()
		{
			// Protocol specification
			var protocol = Select(Send(Val<Block>, CatchNewChannel(Send(Unit, End),
				Follow(Receive(Val<uint>, Goto0), Goto0))), End);

			// Pairs of thread index and total thread, passed to each thread
			var cpuCount = Environment.ProcessorCount;
			var threadArgs = Enumerable.Range(0, cpuCount).Select(index => (index, cpuCount));

			// Run server threads and start communication
			var ch1s = protocol.Parallel(threadArgs, (ch1, args) =>
			{
				// Server thread implementation
				for (var loop = true; loop;)
				{
					ch1.Follow
					(
						cont =>
						{
							// Receive a block and delegate a new channel for cancellation
							var ch2 = cont.Receive(out var block).ThrowNewChannel(out var cancelCh);

							// Receive cancellation as a future object
							cancelCh.ReceiveAsync(out var cancelTask).CloseAsync();

							// Initialize a miner
							var miner = new Miner(block, (uint)args.index, (uint)args.cpuCount);

							// Work hard to find a nonce
							while (true)
							{
								if (miner.TestNextNonce(out var nonce))
								{
									// Nonce found, send it back to client
									ch1 = ch2.SelectLeft().Send(nonce).Goto();
									break;
								}
								else if (cancelTask.IsCompleted)
								{
									// Cancellation invoked
									ch1 = ch2.SelectRight().Goto();
									break;
								}
							}
						},
						end =>
						{
							// End of protocol
							end.Close();
							loop = false;
						}
					);
				}
			});

			// Client implementation (main thread)
			foreach (var block in Block.GetSampleBlocks())
			{
				Console.WriteLine("Start mining");
				Console.WriteLine("===== Mining Target Block =====");
				Console.WriteLine(block);
				Console.WriteLine("===== =================== =====");

				// Send a block to each thread
				var ch2s = ch1s.Map(ch1 => ch1.SelectLeft().Send(block));

				//  external choice
				var (ch3s, cancelChs) = ch2s.Map(ch2 =>
				{
					var followTask = ch2.CatchNewChannel(out var cancalCh).FollowAsync
					(
						some =>
						{
							var ch3 = some.Receive(out var nonce).Goto();
							return (ch3, nonce);
						},
						none =>
						{
							var ch3 = none.Goto();
							return (ch3, default(uint?));
						}
					);
					return (followTask, cancalCh);
				}).Unzip();

				// Wait for any single thread to respond
				await Task.WhenAny(ch3s);

				// Send cancellation to each thread
				cancelChs.ForEach(ch => ch.Send().Close());

				// Get endpoints and results from future object
				var (ch4s, results) = ch3s.Select(c => c.Result).Unzip();

				// Print results
				foreach (var (index, result) in results.Select((i, r) => (r, i)))
				{
					if (result.HasValue)
					{
						Console.WriteLine($"Thread {index} found: 0x{result:x}");
					}
					else
					{
						Console.WriteLine($"Thread {index} found: None");
					}
					Console.WriteLine();
				}

				// Assign and recurse
				ch1s = ch4s;
			}

			// No blocks to mine, finish channels
			ch1s.ForEach(ch1 => ch1.SelectRight().Close());
		}
	}
}
