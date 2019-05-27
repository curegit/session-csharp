using System;
using System.Net.Http;
using SessionTypes;
using SessionTypes.Binary;
using SessionTypes.Binary.Threading;

namespace ParallelHttpDownloader
{
	public class Program
	{
		private static void Main(string[] args)
		{
			Console.WriteLine("Parallel HTTP Downloader");
			var list = args;

			var n = 8;
			var arg = new (int, HttpClient)[n];
			for (var i = 0; i < n; i++)
			{
				arg[i] = (i + 1, new HttpClient());
			}

			var clients = BinarySessionChannel<Cons<Request<string, Respond<byte[], RequestChoice<Jump<Zero>, Close>>>, Nil>>.Distribute((server, tuple) =>
			{
				var s = server.Enter();
				while (true)
				{
					var s1 = s.Receive(out var url);
					var flag = false;
					var data = Download(tuple.Item2, url);
					s1.Send(data).Follow(
					left => { s = left.Zero(); },
					right => { flag = true; right.Close(); }
					);
					if (flag) break;
				}
			}
			, arg);
		}

		private static byte[] Download(HttpClient client, string url)
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
	}
}
