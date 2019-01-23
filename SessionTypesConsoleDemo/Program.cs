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
		}
	}
}
