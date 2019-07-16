using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
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
			var n = Environment.ProcessorCount;
			Console.WriteLine($"{n} Threads");
			args = new string[]
			{
				"http://www.toei-anim.co.jp/tv/precure/images/special/wallpaper/01_sp1080_1920.jpg",
				"http://www.toei-anim.co.jp/tv/precure/images/special/wallpaper/01_pc1920_1080.jpg",
				"https://www.asahi.co.jp/precure/maho/img/enjoyment/wallpaper/04/1920_1080_2.jpg",
				"https://www.asahi.co.jp/precure/maho/img/enjoyment/wallpaper/06/1920_1080_2.jpg",
				"https://www.asahi.co.jp/precure/maho/img/enjoyment/wallpaper/12/1920_1080_2.jpg",
				"https://www.asahi.co.jp/precure/maho/img/enjoyment/wallpaper/01_sdwu/1920_1080_2.jpg",
				"https://www.asahi.co.jp/precure/maho/img/enjoyment/wallpaper/05dmck/1920_1080_2.jpg",
				"https://www.asahi.co.jp/precure/maho/img/enjoyment/wallpaper/09/1920_1080_2.jpg",
				"https://www.asahi.co.jp/precure/maho/img/enjoyment/wallpaper/10/1920_1080_2.jpg",
				"https://www.asahi.co.jp/precure/maho/img/enjoyment/wallpaper/11/1920_1080_2.jpg",
				"https://www.asahi.co.jp/precure/maho/img/enjoyment/wallpaper/12/1920_1080_2.jpg",
				"https://www.asahi.co.jp/precure/kirakira/img/enjoyment/wallpaper/01/1920_1080_2.jpg",
				"https://www.asahi.co.jp/precure/kirakira/img/enjoyment/wallpaper/02/1920_1080_2.jpg",
				"https://www.asahi.co.jp/precure/kirakira/img/enjoyment/wallpaper/03/1920_1080_2.jpg",
				"https://www.asahi.co.jp/precure/kirakira/img/enjoyment/wallpaper/04/1920_1080_2.jpg",
				"https://www.asahi.co.jp/precure/kirakira/img/enjoyment/wallpaper/05/1920_1080_2.jpg",
				"https://www.asahi.co.jp/precure/kirakira/img/enjoyment/wallpaper/06/1920_1080_2.jpg",
				"https://www.asahi.co.jp/precure/kirakira/img/enjoyment/wallpaper/07/1920_1080_2.jpg",
				"https://www.asahi.co.jp/precure/kirakira/img/enjoyment/wallpaper/08/1920_1080_2.jpg",
				"https://www.asahi.co.jp/precure/kirakira/img/enjoyment/wallpaper/09/1920_1080_2.jpg",
				"https://www.asahi.co.jp/precure/kirakira/img/enjoyment/wallpaper/10/1920_1080_2.jpg",
				"https://www.asahi.co.jp/precure/kirakira/img/enjoyment/wallpaper/11/1920_1080_2.jpg",
				"https://www.asahi.co.jp/precure/kirakira/img/enjoyment/wallpaper/12/1920_1080_2.jpg",
				"https://www.asahi.co.jp/precure/princess/img/enjoyment/wallpaper/precure091_1920.jpg",
				"https://www.asahi.co.jp/precure/princess/img/enjoyment/wallpaper/precure081_1920.jpg",
				"https://www.asahi.co.jp/precure/princess/img/enjoyment/wallpaper/precure071_1920.jpg",
				"https://www.asahi.co.jp/precure/princess/img/enjoyment/wallpaper/precure061_1920.jpg",
				"https://www.asahi.co.jp/precure/princess/img/enjoyment/wallpaper/precure051_1920.jpg",
				"https://www.asahi.co.jp/precure/princess/img/enjoyment/wallpaper/precure041_1920.jpg",
				"https://www.asahi.co.jp/precure/princess/img/enjoyment/wallpaper/precure031_1920.jpg",
				"https://www.asahi.co.jp/precure/princess/img/enjoyment/wallpaper/precure021_1920.jpg",
				"https://www.asahi.co.jp/precure/princess/img/enjoyment/wallpaper/precure12_1920.jpg",
			};

			var ids = Enumerable.Range(1, n).ToArray();

			var clients = BinaryChannel<Cons<ReqChoice<Req<string, Resp<byte[], Goto0>>, Eps>, Nil>>.Distribute((server, id) =>
			{
				var s = server.Enter();
				var http = new HttpClient();
				while (true)
				{
					var flag = false;
					s.Follow(
						left =>
						{
							var s1 = left.Receive(out var url);
							Console.WriteLine($"Thread {id} Started downloading");
							var d = Download(http, url);
							Console.WriteLine($"Thread {id} Finished downloading");
							var s2 = s1.Send(d);
							s = s2.Jump();
						},
						right =>
						{
							Console.WriteLine($"Thread {id} Closed");
							flag = true; right.Close();
						}
					);
					if (flag) break;
				}
			}
			, ids);


			var entries = clients.Select(c => c.Enter()).ToList();
			var working = new List<Task<(Client<Goto0, Cons<ReqChoice<Req<string, Resp<byte[], Goto0>>, Eps>, Nil>>, byte[])>>();
			var data = new List<byte[]>();
			foreach (var url in args)
			{
				if (!entries.Any())
				{
					var wait = Task.WhenAny(working);
					var task = wait.Result;
					working.Remove(task);
					var e = task.Result.Bind(out var bytes);
					data.Add(bytes);
					entries.Add(e.Jump());
				}

				working.Add(entries[0].ChooseLeft().Send(url).ReceiveAsync());
				entries.RemoveAt(0);
			}

			foreach (var entry in entries)
			{
				entry.ChooseRight().Close();
			}

			while (working.Any())
			{
				var wait = Task.WhenAny(working);
				var task = wait.Result;
				working.Remove(task);
				var e = task.Result.Bind(out var bytes);
				data.Add(bytes);
				e.Jump().ChooseRight().Close();
			}

			for (int i = 0; i < data.Count; i++)
			{
				if (data[i] != null)
				{
					File.WriteAllBytes($@"{i}.jpg", data[i]);
				}
			}
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
