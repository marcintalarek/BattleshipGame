using BattleshipGame.Models;

namespace BattleshipGame.Algorithms.Interfaces
{
    public interface IShipPlacementStrategy
    {
        IEnumerable<Ship> GenerateShips();
    }
}
