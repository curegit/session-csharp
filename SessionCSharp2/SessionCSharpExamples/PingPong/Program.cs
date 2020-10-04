using System;
using System.Threading;
using System.Threading.Tasks;
using Session;
using Session.Threading;

namespace PingPong
{
	using static ProtocolCombinator;

	class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");

			var proto = new PINGPONG();

			var c = proto.ForkThread(s =>
			{
				while (true)
				{
					s = s.Receive().Send();
					Thread.Sleep(1);
				}
			});

			while(true)
			{
				c = c.Send().Receive();
				Thread.Sleep(1);
			}
        }

		public class PingPong : Send<Receive<PingPong>>
		{
			public PingPong(Send<Receive<PingPong>> copy) : base(copy) { }
		}

		public class PongPing : Receive<Send<PongPing>>
		{
			public PongPing(Receive<Send<PongPing>> copy) : base(copy) { }
		}

		public class PINGPONG : Dual<PingPong, PongPing>
		{
			public PINGPONG() : base(Recur(() => Send(Unit, Receive(Unit, new PINGPONG())), x => new PingPong(x), x => new PongPing(x)))
			{
			}
		}
	}
}
