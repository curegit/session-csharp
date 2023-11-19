using System;
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

                var cliCh = p2.ForkThread(srvCh =>
                {
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
                    .Offer(left =>
                    {
                        return left.Receive(out int i);
                    }, right =>
                    {
                        return right;
                    }).Close();
            }

            // tak
            {
                var protocol = Send(Val<(int, int, int)>, DelegRecv(Send(Val<Unit>, Eps), Offer(Recv(Val<int>, Eps), Eps)));

                var cliCh = protocol.ForkThread(srvCh =>
                {
                    var srvCh2 = srvCh.Receive(out var x, out var y, out var z).DelegNew(out var cancelCh);

                    cancelCh.ReceiveAsync(out Task cancel).Close();
                    try
                    {
                        var result = Tak(x, y, z);
                        srvCh2.SelectLeft().Send(result).Close();
                    }
                    catch (OperationCanceledException)
                    {
                        srvCh2.SelectRight().Close();
                    }

                    int Tak(int a, int b, int c)
                    {
                        if (cancel.IsCompleted) throw new OperationCanceledException();
                        if (a <= b) return b;
                        return Tak(Tak(a - 1, b, c), Tak(b - 1, c, a), Tak(c - 1, a, b));
                    }
                });

                var cliCh2 = cliCh.Send((16, 3, 2)).DelegRecv(out var cancelCh2);

                Task.Delay(10500).ContinueWith(t =>
                {
                    cancelCh2.Send().Close();
                });

                cliCh2.Offer(
                    left =>
                    {
                        left.Receive(out var ans).Close();
                        Console.WriteLine($"Tarai = {ans}");
                    },
                    right =>
                    {
                        right.Close();
                        Console.WriteLine("Canceled");
                    });
            }
        }
    }
}
