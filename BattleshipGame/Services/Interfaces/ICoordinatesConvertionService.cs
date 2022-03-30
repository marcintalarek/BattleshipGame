using BattleshipGame.Models;

namespace BattleshipGame.Services.Interfaces
{
    public interface ICoordinatesConvertionService
    {
        Coordinate ConvertAlphanumericToCoordinate(string value);
    }
}
