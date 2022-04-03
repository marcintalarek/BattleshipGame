using BattleshipGame.Algorithms.Interfaces;
using BattleshipGame.Models;
using BattleshipGame.Services.Interfaces;
using System.ComponentModel.DataAnnotations;

namespace BattleshipGame.Services
{
    internal class BoardManageService : IBoardManageService
    {
        private readonly IEnumerable<Ship> _ships;
        private readonly HashSet<Coordinate> coordinatesDiscovered = new();
        private int numberOfDiscoveredCoordinatessWithShips = 0;

        public BoardManageService(IShipPlacementStrategy shipPlacementStrategy)
        {
            _ships = shipPlacementStrategy.GenerateShips();
        }

        public ShipHit? Shot(Coordinate coordinate)
        {
            if (coordinatesDiscovered.Contains(coordinate))
            {
                throw new ValidationException("Coordinate already discovered.");
            }

            coordinatesDiscovered.Add(coordinate);

            Ship? ship = _ships.Where(p => p.Coordinates.Contains(coordinate))
                .Cast<Ship?>()
                .FirstOrDefault();

            if (ship == null)
            {
                return null;
            }

            numberOfDiscoveredCoordinatessWithShips++;

            return new ShipHit(ship.Value.ShipType, ship.Value.Coordinates.IsSubsetOf(coordinatesDiscovered));
        }

        public bool AreAllShipsSunk()
            => numberOfDiscoveredCoordinatessWithShips == Constants.NumberOfCoordinatesWithShips;

        public IDictionary<Coordinate, ShipType?> GetDiscoveredCoordinates()
        {
            var discoveredCoordinates = new Dictionary<Coordinate, ShipType?>();
            GetDiscoveredCoordinatesWithShips(discoveredCoordinates);
            GetDiscoveredCoordinatesWithoutShips(discoveredCoordinates);
            return discoveredCoordinates;
        }

        private void GetDiscoveredCoordinatesWithShips(Dictionary<Coordinate, ShipType?> discoveredCoordinates)
        {
            foreach (var ship in _ships)
            {
                var discoveredShipCoordinates = ship.Coordinates.Intersect(coordinatesDiscovered);
                foreach (var discoveredShipCoordinate in discoveredShipCoordinates)
                {
                    discoveredCoordinates.Add(discoveredShipCoordinate, ship.ShipType);
                }
            }
        }

        private void GetDiscoveredCoordinatesWithoutShips(Dictionary<Coordinate, ShipType?> discoveredCoordinates)
        {
            var discoveredCoordinatesWithoutShips = coordinatesDiscovered.Except(discoveredCoordinates.Keys);
            foreach (var discoveredCoordinateWithoutShip in discoveredCoordinatesWithoutShips)
            {
                discoveredCoordinates.Add(discoveredCoordinateWithoutShip, null);
            }
        }
    }
}
