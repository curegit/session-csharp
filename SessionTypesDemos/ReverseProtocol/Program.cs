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
			Console.WriteLine("Hello World!");

			// 
			var element0 = AtC(C2S(P<int>, Call0(S2C(P<int>, End))), End);
			var protocol = SessionList(element0);

			var c = protocol.Fork(server =>
			{
				server.Enter().Follow(
					left =>
					{
						left.Receive(out var n).Call3((session, thisFunc) =>
						{
							return session.Follow(
								left => left.Receive(out var m).Call3(thisFunc).Return().Send(m)//.Call3(thisFunc).Send(m),
								right => right
								);
						}).Return().Send(n).Close();
					},
					right =>
					{
						right.Close();
					});
			});

			var d = c.Enter().Send(1).SelectLeft().Call().Send(2).SelectLeft();

			var t = d.Call((se, self) =>
			{
				var h = se.Send(1).Call(self);

				var a = h.Receive(out var g);

				return a.Return();
			});
		}
	}
}
