using System;
using SessionTypes;
using SessionTypes.Threading;

namespace ReverseProtocol
{
	using static ProtocolBuilder;

	public class Program
	{
		public static void Main(string[] args)
		{
			// int を送った回数ぶんだけ int を返すプロトコル
			// Call0 は element0 を呼び出して S2C(P<int>, End) に続く
			var element0 = AtC(C2S(P<int>, Call0(S2C(P<int>, End))), End);
			var protocol = SessionList(element0);

			var client = protocol.Fork(server =>
			{
				// サーバー側
				server.Enter().Follow(
					left =>
					{
						// thisFunc にはこのラムダ自体が入るという設計（再帰呼び出しのため）
						left.Receive(out var n).Call((session, thisFunc) =>
						{
							return session.Follow(
								left => left.Receive(out var m).Call(thisFunc).Send(m),
								right => right
								);
						}).Send(n).Close();
					},
					right =>
					{
						right.Close();
					});
			});

			// クライアント側
			// 3回送信する（Call() で残りのセッションをスタックに入れて、再帰先のセッションを展開する）
			var c1 = client.Enter().SelectLeft().Send(1).Call().SelectLeft().Send(2).Call().SelectLeft().Send(3);
			// ここで右を選択
			var c2 = c1.Call().SelectRight().Return();
			// 3回送信したので3回受信する必要がある（Return() はスタックからセッション型をとってくる操作）
			c2.Receive(out var x).Return().Receive(out var y).Return().Receive(out var z).Close();

			// Received: [x: 3, y: 2, z: 1]　送信と逆順に帰ってくる
			Console.WriteLine($"Received: [x: {x}, y: {y}, z: {z}]");
		}
	}
}
