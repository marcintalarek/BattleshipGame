using NSubstitute;
using System;
using Xunit;

namespace BattleshipGame.Tests
{
    public class RandomExtensionsTests
    {
        [Theory]
        [InlineData(0.0, false)]
        [InlineData(0.4, false)]
        [InlineData(0.4999, false)]
        [InlineData(0.5, true)]
        [InlineData(0.5001, true)]
        [InlineData(0.9999, true)]
        public void NextBool_RandomDoubleNumber_ReturnsBoolCorrectly(double nextDouble, bool result)
        {
            var random = Substitute.For<Random>();
            random.NextDouble().Returns(nextDouble);

            Assert.Equal(result, random.NextBool());
        }
    }
}
