using BattleshipGame.Models;

namespace BattleshipGame.Algorithms.Interfaces
{
    internal interface IShipPlacementStrategy
    {
        IEnumerable<Ship> GenerateShips();
    }
}
