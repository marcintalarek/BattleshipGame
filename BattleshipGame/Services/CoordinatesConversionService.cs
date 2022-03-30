using BattleshipGame.Models;
using BattleshipGame.Services.Interfaces;

namespace BattleshipGame.Services
{
    public class CoordinatesConversionService : ICoordinatesConvertionService
    {
        public Coordinate ConvertAlphanumericToCoordinate(string value)
        {
            value = value.ToUpper();

            char letter = value[0];
            string number = value[1..];

            int x = ConvertLetterToX(letter);
            int y = ConvertNumberToY(number);

            return new Coordinate(x, y);
        }

        private static int ConvertLetterToX(char letter)
            => letter - Constants.LetterA;

        private static int ConvertNumberToY(string number)
            => int.Parse(number) - 1;
    }
}
