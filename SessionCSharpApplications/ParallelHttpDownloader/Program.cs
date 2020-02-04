using System;
using System.Linq;
using System.Threading.Tasks;
using System.Net.Http;
using Session;
using Session.Threading;

namespace ParallelHttpDownloader
{
	using static ProtocolCombinator;

	public class Program
	{
		// Pass URLs to download by command line args
		private static async Task Main(string[] args)
		{
			// Protocol specification
			var prot = Select(Send(Value<string>, Receive(Value<byte[]?>, Goto0)), End);

			var ch1s = prot.Parallel(Environment.ProcessorCount, ch1 =>
			{
				// Init http client
				var http = new HttpClient();
				// Work...
				for (var loop = true; loop;)
				{
					ch1.Offer(
					left =>
					{
						var ch2 = left.Receive(out var url);
						var data = Download(http, url);
						ch1 = ch2.Send(data).Goto();
					},
					right =>
					{
						right.Close();
						loop = false;
					});
				}
				// Download function
				byte[]? Download(HttpClient client, string url)
				{
					try
					{
						return client.GetByteArrayAsync(url).Result;
					}
					catch
					{
						return null;
					}
				}
			});

			// Pass jobs to each thread
			var (ch2s, ch1s_rest, args_rest) = ch1s.ZipWith(args, (ch1, arg) => {
				var ch3 = ch1.SelectLeft().Send(arg).ReceiveAsync(out var data);
				return (ch3.Sync(), data);
			});

			// Close unneeded channels
			ch1s_rest.ForEach(c => c.SelectRight().Close());

			var (working, results) = ch2s.Unzip();
			var working_list = working.ToList();
			var result_list = results.ToList();

			// Wait for a single worker finish and pass a new job
			foreach (var url in args_rest)
			{
				var finished = await Task.WhenAny(working_list);
				working_list.Remove(finished);
				var ch3 = (await finished).Goto().SelectLeft().Send(url).ReceiveAsync(out var data);
				working_list.Add(ch3.Sync());
				result_list.Add(data);
			}

			// Wait for still working threads
			while(working_list.Any())
			{
				var finished = await Task.WhenAny(working_list);
				working_list.Remove(finished);
				(await finished).Goto().SelectRight().Close();
			}

			// Save to files or something...
		}
	}
}
