using BattleshipGame.Models;

namespace BattleshipGame.Services.Interfaces
{
    internal interface IDisplayManageService
    {
        event CoordinateProvidedEventHandler? CoordinateProvided;

        void DrawValidView(
            IDictionary<Coordinate, ShipType?> discoveredCoordinates,
            string? coordinateAlphanumeric,
            ShipHit? shipHit = null);

        void DrawErrorView(
            IDictionary<Coordinate, ShipType?> discoveredCoordinates,
            string coordinateAlphanumeric,
            string message);

        void DrawFinalView(IDictionary<Coordinate, ShipType?> discoveredCoordinates);
    }
}
