using System;
using SessionTypes;
using SessionTypes.Threading;

namespace OrdinalProtocol
{
	using static ProtocolBuilder;

	public class Program
	{
		public static void Main(string[] args)
		{
			var dual = C2S(P<int>, S2C(P<string>, End())).AsChannel();
			var client = dual.Fork(server =>
			{
				server.Receive(out var number).Send(ToOrdinalString(number)).Close();
			});
			client.Send(21).Receive(out var ordinal).Close();
			Console.WriteLine(ordinal);
		}

		private static string ToOrdinalString(int n)
		{
			if (GetDigit(n, 1) == 1)
			{
				return $"{n}th";
			}
			else
			{
				switch (GetDigit(n, 0))
				{
					case 1:
						return $"{n}st";
					case 2:
						return $"{n}nd";
					case 3:
						return $"{n}rd";
					default:
						return $"{n}th";
				}
			}
		}

		private static int GetDigit(int i, int n)
		{
			if (n < 0)
			{
				throw new ArgumentOutOfRangeException(nameof(n));
			}
			var m = Math.Abs(i);
			while (n != 0)
			{
				m /= 10;
				n--;
			}
			return m % 10;
		}
	}
}
