using BattleshipGame.Models;
using Xunit;

namespace BattleshipGame.Tests
{
    public class CoordinateEqualityComparerTests
    {
        [Theory]
        [InlineData(1, 2, 1, 2, true)]
        [InlineData(2, 1, 2, 1, true)]
        [InlineData(2, 2, 2, 2, true)]
        [InlineData(1, 2, 2, 1, false)]
        [InlineData(1, 2, 1, 3, false)]
        [InlineData(1, 2, 3, 2, false)]
        public void Equals_ForCoordinates_ReturnsEqualityCorrectly(int x1, int y1, int x2, int y2, bool isEqual)
        {
            var fieldEqualityComparer = new CoordinateEqualityComparer();
            var field1 = new Coordinate(x1, y1);
            var field2 = new Coordinate(x2, y2);

            Assert.Equal(isEqual, fieldEqualityComparer.Equals(field1, field2));
        }

        [Theory]
        [InlineData(0, 0, 0)]
        [InlineData(0, 1, 1)]
        [InlineData(1, 0, 10)]
        [InlineData(1, 1, 11)]
        [InlineData(1, 2, 12)]
        [InlineData(2, 1, 21)]
        [InlineData(9, 9, 99)]
        public void GetHashCode_ForCoordinates_ReturnsHashCorrectly(int x, int y, int hash)
        {
            var fieldEqualityComparer = new CoordinateEqualityComparer();
            var field = new Coordinate(x, y);

            Assert.Equal(hash, fieldEqualityComparer.GetHashCode(field));
        }
    }
}
