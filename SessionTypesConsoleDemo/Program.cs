using System;
using System.Threading;
using System.Threading.Tasks;
using SessionTypes.Binary;

namespace SessionTypesConsoleDemo
{
	// エイリアスを貼ることもできる
	//using MyProtocol = Request<int, Respond<string, Close>>;

	public class Program
	{
		private static async Task Main(string[] args)
		{
			Console.WriteLine("Hello Session-Types!");

			var client = BinarySession<Request<int, Respond<string, Close>>>.Fork(async server =>
			{
				Console.WriteLine("Server Start");

				// 拒否できる
				// var server1 = await server.Send("ng");

				var server1 = await Task.Run(() => server.Receive());

				// 関数の形でも書ける
				// var server1 = await Task.Run(() => BinarySession.Receive(server));

				var server2 = server1.Send("ok");

				Console.WriteLine("Server End");
			});

			Console.WriteLine("Client Start");

			// クライアント側の処理
			Thread.Sleep(1000);

			// これは拒否
			// var client1 = await Task.Run(() => client.Receive());

			var client1 = client.Send(2);

			// これも拒否
			// var client2 = client1.Send(10); 

			var client2 = await Task.Run(() => client1.Receive());

			Console.WriteLine("Client End");




			// ---- 分岐　(RS flip-flop)----
			var client10 = BinarySession<Request<bool, Request<bool, Request<bool, RespondChoice<Respond<bool, Close>, Respond<string, Close>>>>>>.Fork(async server =>
			{
				// サーバーサイド
				var s1 = server.Receive();
				bool a = true; // 実際にはここに通信で得た値を入れる
				var s2 = s1.Receive();
				bool b = false; // 実際にはここに通信で得た値を入れる
				var s3 = s2.Receive();
				bool c = true; // 実際にはここに通信で得た値を入れる
				if (b && c)
				{
					s3.ChooseRight().Send("Not allowed");
				}
				else
				{
					s3.ChooseLeft().Send((b || c) ? b : a);
				}
			});

			// クライアントサイド
			var client11 = client10.Send(true).Send(false).Send(true);
			client11.Follow(
			async left =>
			{
				// Receive<bool, Close>
				var b = left.Receive();
			},
			async right =>
			{
				// Receive<string, Close>
				var s = right.Receive();
			}
			);
		}
	}
}
