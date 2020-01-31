using System;
using Session;
using Session.Types;

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
					srvCh.Recv(out int x).Close();
				});
				cliCh.Send(100).Close();
			}

			{
				var p2 = Arrange(Send(Val<int>, Recv(Val<string>, Goto1)), Send(Val<string>, Offer(Recv(Val<int>, Eps), Eps)));


			}

		}
	}

}
