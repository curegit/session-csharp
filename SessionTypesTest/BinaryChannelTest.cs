using Xunit;
using System;
using SessionTypes.Binary;
using SessionTypes.Binary.Threading;

namespace SessionTypesTest
{
	public class BinaryChannelTest
	{
		[Fact]
		public void ContinuationPassingTest()
		{
			var client = BinarySessionChannel<Request<int, Respond<double, Close>>>.Fork
			(
				server =>
				{
					server.Receive((s1, v) => s1.Send(Math.Sqrt(v), s2 => s2.Close()));
				}
			);
			double result = default;
			client.Send
			(10, c1 =>
				{
					c1.Receive(
					(c2, v) =>
						{
							result = v;
							c2.Close();
						}
					);
				}
			);
			Assert.Equal(Math.Sqrt(10), result);
		}
	}
}
