using BattleshipGame.Services;
using Xunit;

namespace BattleshipGame.Tests
{
    public class CoordinatesConversionServiceTests
    {
        [Theory]
        [InlineData("A1", 0, 0)]
        [InlineData("b3", 1, 2)]
        [InlineData("c2", 2, 1)]
        [InlineData("j10", 9, 9)]
        public void ConvertAlphanumericToCoordinate_ValidInput_ReturnsCoordinateProperly(string value, int x, int y)
        {
            var coordinatesConversionService = new CoordinatesConversionService();

            var coordinate = coordinatesConversionService.ConvertAlphanumericToCoordinate(value);

            Assert.Equal(x, coordinate.X);
            Assert.Equal(y, coordinate.Y);
        }
    }
}
