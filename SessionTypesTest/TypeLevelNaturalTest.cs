using Xunit;
using SessionTypes;

namespace SessionTypesTest
{
	public class TypeLevelNaturalTest
	{
		[Fact]
		public void EvaluationTest()
		{
			var zero = new Zero();
			Assert.Equal(0, zero.Evaluate());
			var one = new Succ<Zero>();
			Assert.Equal(1, one.Evaluate());
			var five = new Succ<Succ<Succ<Succ<Succ<Zero>>>>>();
			Assert.Equal(5, five.Evaluate());
		}

		[Fact]
		public void EqualityTest()
		{
			var rei = new Zero();
			var zero = new Zero();
			var san = new Succ<Succ<Succ<Zero>>>();
			var three = new Succ<Succ<Succ<Zero>>>();
			var yon = new Succ<Succ<Succ<Succ<Zero>>>>();
			var four = new Succ<Succ<Succ<Succ<Zero>>>>();
			Assert.True(rei.Equals(zero));
			Assert.True(rei == zero);
			Assert.True(san.Equals(three));
			Assert.True(san == three);
			Assert.True(yon.Equals(four));
			Assert.True(yon == four);
			Assert.False(zero.Equals(three));
			Assert.False(zero == three);
			Assert.False(three.Equals(four));
			Assert.False(three == four);
			Assert.False(four.Equals(zero));
			Assert.False(four == zero);
		}

		[Fact]
		public void InequalityTest()
		{
			var rei = new Zero();
			var zero = new Zero();
			var ichi = new Succ<Zero>();
			var one = new Succ<Zero>();
			var ni = new Succ<Succ<Zero>>();
			var two = new Succ<Succ<Zero>>();
			Assert.True(zero != one);
			Assert.True(one != two);
			Assert.True(two != zero);
			Assert.False(rei != zero);
			Assert.False(ichi != one);
			Assert.False(ni != two);
		}

		[Fact]
		public void ComparisonTest()
		{
			var rei = new Zero();
			var zero = new Zero();
			var ichi = new Succ<Zero>();
			var one = new Succ<Zero>();
			var ni = new Succ<Succ<Zero>>();
			var two = new Succ<Succ<Zero>>();
			Assert.True(zero < one);
			Assert.True(one < two);
			Assert.True(zero < two);
			Assert.True(two > one);
			Assert.True(one > zero);
			Assert.True(two > zero);
			Assert.True(zero <= rei);
			Assert.True(zero <= one);
			Assert.True(one <= ichi);
			Assert.True(one <= two);
			Assert.True(two <= ni);
			Assert.True(zero <= two);
			Assert.True(two >= ni);
			Assert.True(two >= one);
			Assert.True(one >= ichi);
			Assert.True(one >= zero);
			Assert.True(zero >= rei);
			Assert.True(two >= zero);
			Assert.False(zero > one);
			Assert.False(one > two);
			Assert.False(zero > two);
			Assert.False(two < one);
			Assert.False(one < zero);
			Assert.False(two < zero);
			Assert.False(zero >= one);
			Assert.False(one >= two);
			Assert.False(zero >= two);
			Assert.False(two <= one);
			Assert.False(one <= zero);
			Assert.False(two <= zero);
		}
	}
}
