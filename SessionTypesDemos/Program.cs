using System;
using System.Threading;
using System.Threading.Tasks;
using SessionTypes;
using SessionTypes.Binary;
using SessionTypes.Binary.Threading;

namespace SessionTypesConsoleDemo
{
	public class Program
	{
		private static async Task Main(string[] args)
		{
			// 例1
			int m = 20;
			var r = new Random();
			Console.WriteLine($"例1: 序数詞サーバー（{m}回試行）");
			Console.WriteLine();
			for (int i = 0; i < m; i++)
			{
				Console.WriteLine($"{i + 1} 回目");
				await Example1(r.Next() % 200);
				Console.WriteLine();
			}
			Console.WriteLine();
			Console.WriteLine();
			Console.WriteLine();

			// 例2
			Console.WriteLine($"例2: 割り算サーバー（{m}回試行）");
			Console.WriteLine();
			for (int i = 0; i < m; i++)
			{
				Console.WriteLine($"{i + 1} 回目");
				await Example2(r.Next() % 200, r.Next() % 6);
				Console.WriteLine();
			}
			Console.WriteLine();
			Console.WriteLine();
			Console.WriteLine();

			// 例3
			int n = r.Next() % 151;
			Console.WriteLine($"例3: FizzBuzz（{n} まで）");
			Console.WriteLine();
			await Example3(n);
		}

		/// <summary>
		/// 最も単純な通信
		/// サーバーは整数を受けて、対応する英語序数詞を付加した文字列を返す
		/// </summary>
		private static async Task Example1(int n)
		{
			var client = BinarySessionChannel<Request<int, Respond<string, Close>>>.Fork(async server =>
			{
				// サーバースレッドの処理
				Console.WriteLine("Server Start");
				var (server1, v) = await server.Receive();
				Console.WriteLine($"Server Received: {v}");
				string ord = ToOrdinal(v, false);
				Console.WriteLine($"Server Sent: {ord}");
				var server2 = server1.Send(ord);
				Console.WriteLine("Server End");
			});
			// クライアント側の処理
			Console.WriteLine("Client Start");
			Thread.Sleep(1000);
			Console.WriteLine($"Client Sent: {n}");
			var client1 = client.Send(n);
			var (client2, s) = await client1.Receive();
			Console.WriteLine($"Client Received: {s}");
			Console.WriteLine("Client End");
		}

		/// <summary>
		/// 分岐のある通信
		/// サーバーは被除数と除数を受けて零除算でなければ商を返す
		/// 零除算であれば誤りを記した文字列を返す
		/// </summary>
		private static async Task Example2(int dividend, int divisor)
		{
			var client = BinarySessionChannel<Request<int, Request<int, RespondChoice<Respond<int, Close>, Respond<string, Close>>>>>.Fork(async server =>
			{
				// サーバースレッドの処理
				Console.WriteLine("Server Start");
				var (s1, n) = await server.Receive();
				Console.WriteLine($"Server Received: {n}");
				var (s2, m) = await s1.Receive();
				Console.WriteLine($"Server Received: {m}");
				if (m == 0)
				{
					Console.WriteLine("Server Chose: Right");
					var s3 = s2.ChooseRight();
					var mes = "Divided by Zero";
					Console.WriteLine($"Server Sent: {mes}");
					var s4 = s3.Send(mes);
				}
				else
				{
					Console.WriteLine("Server Chose: Left");
					var s3 = s2.ChooseLeft();
					var div = n / m;
					Console.WriteLine($"Server Sent: {div}");
					var s4 = s3.Send(div);
				}
				Console.WriteLine("Server End");
			});
			// クライアント側の処理
			Console.WriteLine("Client Start");
			Thread.Sleep(1000);
			Console.WriteLine($"Client Sent: {dividend}");
			var c1 = client.Send(dividend);
			Console.WriteLine($"Client Sent: {divisor}");
			var c2 = c1.Send(divisor);
			await c2.Follow(
				async left =>
				{
					Console.WriteLine("Client Followed: Left");
					var (c3, div) = await left.Receive();
					Console.WriteLine($"Client Received: {div}");
				},
				async right =>
				{
					Console.WriteLine("Client Followed: Right");
					var (c3, str) = await right.Receive();
					Console.WriteLine($"Client Received: {str}");
				});
			Console.WriteLine("Client End");
		}

		/// <summary>
		/// 分岐・再帰のある通信
		/// サーバーはクライアントから整数を受け取り対応するFizzBuzzゲームの回答を返す
		/// クライアントが満足するまで繰り返す
		/// </summary>
		private static async Task Example3(int n)
		{
			var client = BinarySessionChannel<Block<Request<int, Respond<string, RequestChoice<Jump<Zero>, Close>>>, EndBlock>>.Fork(async server =>
			{
				// サーバースレッドの処理
				var s1 = server.Enter();
				while (true)
				{
					var (s2, i) = await s1.Receive();
					var str = Mod(i, 3) == 0 ? (Mod(i, 5) == 0 ? "FizzBuzz" : "Fizz") : (Mod(i, 5) == 0 ? "Buzz" : $"{i}");
					var s3 = s2.Send(str);
					bool l = false;
					await s3.Follow(
						async left =>
						{
							l = true;
							s1 = left.Zero();
							// TODO 本当はここからループはじめに飛びたい
						},
						async right =>
						{
							
						});
					// TODO
					if (!l)
					{
						break;
					}
				}
			});
			// クライアント側の処理
			int j = 1;
			var c1 = client.Enter();
			while (true)
			{
				var c2 = c1.Send(j);
				var (c3, s) = await c2.Receive();
				Console.WriteLine(s);
				if (j == n)
				{
					c3.ChooseRight();
					break;
				}
				c1 = c3.ChooseLeft().Zero();
				j++;
			}
		}

		private static int Mod(int dividend, int divisor)
		{
			return (dividend % divisor + Math.Abs(divisor)) % divisor;
		}

		private static int Digit(int i, int n)
		{
			if (n < 0)
			{
				throw new ArgumentOutOfRangeException("n", "Digit should be a natural number.");
			}
			i = Math.Abs(i);
			while (n != 0)
			{
				i /= 10;
				n--;
			}
			return Mod(i, 10);
		}

		private static string ToOrdinal(int n, bool onlySuffix = false)
		{
			if (Digit(n, 1) == 1)
			{
				return n.ToString() + "th";
			}
			else
			{
				switch (Digit(n, 0))
				{
					case 1:
						return (onlySuffix ? "" : n.ToString()) + "st";
					case 2:
						return (onlySuffix ? "" : n.ToString()) + "nd";
					case 3:
						return (onlySuffix ? "" : n.ToString()) + "rd";
					default:
						return (onlySuffix ? "" : n.ToString()) + "th";
				}
			}
		}
	}
}
