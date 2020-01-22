using System;

using Session;
using Session.Threading;
//using SessionTypes.Stream.Net;
//using SessionTypes.Stream;


using System.Threading;
using System.Threading.Tasks;

namespace VarianceProtocol
{
	using static ProtocolCombinator;

	class Program
	{
		static void Main(string[] args)
		{
			//Session(s, p)
			Console.WriteLine("Hello World!");

			var prot = Receive(Value<int>, Receive(Value<int>, Receive(Value<int>, End)));

			var ch1 = prot.ForkThread(ch1 => {
				Thread.Sleep(2000);
				ch1.Send(1).Send(2).Send(3).Close();
			});


			ch1.ReceiveAsync(out var x).ReceiveAsync(out var y).ReceiveAsync(out var z).CloseAsync();

			Console.WriteLine("a");

			Console.WriteLine($"{z.IsCompleted}");

			Console.WriteLine($"{x.Result} {y.Result} {z.Result}");


		}
	}
}
