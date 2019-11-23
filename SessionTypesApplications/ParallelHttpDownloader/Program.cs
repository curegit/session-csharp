using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Net.Http;
using SessionTypes;
using SessionTypes.Threading;

namespace ParallelHttpDownloader
{
	using static ProtocolBuilder;

	//delegate Session<S, P> SendDeleg<T, S, P>(Session<Send<T, S>, P> s) where S:SessionType where P: ProtocolType;

	public class Program
	{
		private static void Main(string[] args)
		{
			//var a = new SendDeleg(s => { s.Send(1)});

			//Fuc.SedSome(sbyt);

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

			var protocol = AtC(C2S(P<string>, S2C(P<byte[]>, Goto0)), End);

			var clients = protocol.Distribute((server, id) =>
			{
				//
				var http = new HttpClient();
				//
				byte[] Download()
				{
					try
					{
						
					}
					catch
					{
						return null;
					}
				}
				//
				var loop = true;
				
				while(loop) {
					server.Follow(
					left =>
					{
						var s3 = left.Receive(out var url);
						var d = Download(http, url);
						server = s3.Send(d).Goto();
					},
					right =>
					{
						right.Close();
						loop = false;
					});
				}
			}, ids);

			var (s, unusedSessios, remainigArgs) = clients.ZipSessions(args.AsEnumerable(), (c, u) => c.SelectLeft().Send(u).ReceiveAsync());

			//var (cs1, rem, ss) = clients.Zip(args, (c, u) => c.SelectLeft().Send(u).ReceiveAsync()).ToList();

			//var rem = args.Skip(cs1.Count()).ToList();
			var us = unusedSessios.ToList();
			var ss = s.ToList();

			var data = new List<byte[]>();

			us.Select(s1 => { s1.SelectRight().Close(); return 0; });

			foreach(var r in args)
			{
				int i = Task.WaitAny(ss.ToArray());
				var s2 = ss[i].Result.Bind(out var d).Goto();
				ss.RemoveAt(i);
				data.Add(d);
				ss.Add(s2.SelectLeft().Send(r).ReceiveAsync());
			}
			ss.Select(s2 => { s2.Result.Bind(out var a).Goto().SelectRight().Close(); data.Add(a);  return 0; });


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
