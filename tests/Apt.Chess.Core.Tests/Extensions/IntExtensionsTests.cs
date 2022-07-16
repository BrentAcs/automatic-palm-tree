using Apt.Chess.Core.Extensions;

namespace Apt.Chess.Core.Tests.Extensions;

public class IntExtensionsTests
{
   [Theory]
   [InlineData(1, true)]
   [InlineData(0, false)]
   [InlineData(-1, false)]
   public void IsPositive_Test(int value, bool expected)
   {
      bool result = value.IsPositive();

      result.Should().Be(expected);
   }
   
   [Theory]
   [InlineData(1, false)]
   [InlineData(0, false)]
   [InlineData(-1, true)]
   public void IsNegative_Test(int value, bool expected)
   {
      bool result = value.IsNegative();

      result.Should().Be(expected);
   }
}
