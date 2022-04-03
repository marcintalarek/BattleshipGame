using BattleshipGame.Models;

namespace BattleshipGame.Services.Interfaces
{
    internal interface IBoardManageService
    {
        ShipHit? Shot(Coordinate coordinate);
        
        bool AreAllShipsSunk();

        IDictionary<Coordinate, ShipType?> GetDiscoveredCoordinates();
    }
}
