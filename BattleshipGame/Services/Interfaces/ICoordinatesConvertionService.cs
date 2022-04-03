using BattleshipGame.Models;

namespace BattleshipGame.Services.Interfaces
{
    internal interface ICoordinatesConvertionService
    {
        Coordinate ConvertAlphanumericToCoordinate(string value);
    }
}
