using System;
using System.Threading.Tasks;
using Session;
using Session.Threading;

namespace TaraiProtocol
{
    using static ProtocolCombinator;

    public class Program
    {
        public static async Task Main(string[] args)
        {
            var protocol = Send(Tuple<int, int, int>, DelegRecv(Send(Unit, End), Follow(Receive(Val<int>, End), End)));

            var cliCh = protocol.ForkThread(srvCh =>
            {
                var srvCh2 = srvCh.Receive(out var x, out var y, out var z).DelegSendNew(out var cancelCh);

                cancelCh.ReceiveAsync(out Task cancel).CloseAsync();
                try
                {
                    var result = Tak(x, y, z);
                    srvCh2.SelectLeft().Send(result).Close();
                }
                catch (OperationCanceledException)
                {
                    srvCh2.SelectRight().Close();
                }
                int Tak(int x, int y, int z)
                {
                    if (cancel.IsCompleted) throw new OperationCanceledException();
                    if (x <= y) return y;
                    return Tak(Tak(x - 1, y, z), Tak(y - 1, z, x), Tak(z - 1, x, y));
                }
            });

            var ret =
             cliCh.Send((16, 3, 2))
                  .DelegRecv(out var ch)
                  .OfferAsync(
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
            var timeout = Task.Delay(10000);
            var task = await Task.WhenAny(ret, timeout);
            if (task == ret)
            {
                ch.Send().Close();
            }
            else
            {
                ch.Send().Close();
                await ret;
            }
        }
    }
}
