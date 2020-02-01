using System;
using Session;
using Session.Types;
using System.Threading.Tasks;

namespace SessionTest
{
	using static Session.ProtocolCombinators;
	using static Session.Communication;

	public class Main
	{
		public static void Test()
		{
			{
				var p1 = Send(Val<int>, Eps);

				var cliCh = p1.ForkThread(srvCh =>
				{
					srvCh.Receive(out int x).Close();
				});
				cliCh.Send(100).Close();
			}

			// protocol with branching and recursion
			{
				var p2 = Arrange(Send(Val<int>, Recv(Val<string>, Goto1)), Send(Val<string>, Offer(Recv(Val<int>, Eps), Eps)));

				var cliCh = p2.ForkThread(srvCh => {
					var srvCh2 = srvCh.Receive(out int v).Send(v.ToString()).Goto1().Receive(out string str);
					if (int.TryParse(str, out int i))
					{
						srvCh2.SelectLeft().Send(i).Close();
					}
					else
					{
						srvCh2.SelectRight().Close();
					}
				});

				cliCh.Send(100).Receive(out string str2).Goto1().Send("100")
					.Offer(left => {
						left.Receive(out int i).Close();
					}, right => {
						right.Close();
					});

			}

		}
	}

}
