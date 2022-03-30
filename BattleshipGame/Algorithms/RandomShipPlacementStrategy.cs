using BattleshipGame.Algorithms.Interfaces;
using BattleshipGame.Models;
using System.Collections.Immutable;

namespace BattleshipGame.Algorithms
{
    public class RandomShipPlacementStrategy : IShipPlacementStrategy
    {
        private readonly Random _random;

        public RandomShipPlacementStrategy()
            : this(new Random())
        {
        }

        public RandomShipPlacementStrategy(Random random)
        {
            _random = random;
        }

        public IEnumerable<Ship> GenerateShips()
        {
            var ships = new List<Ship>();

            foreach (var shipType in Constants.ShipsOnBoard)
            {
                var shipLocation = GenerateCoordinates(ships, shipType);
                ships.Add(new Ship(shipType, shipLocation.ToImmutableHashSet(new CoordinateEqualityComparer())));
            }

            return ships;
        }

        private HashSet<Coordinate> GenerateCoordinates(IEnumerable<Ship> existingShips, ShipType shipTypeToGenerate)
        {
            HashSet<Coordinate> proposedCoordinates;

            do
            {
                bool isHorizontal = _random.NextBool();

                int widerSideInitialCoordinate = _random.Next(Constants.BoardSideSize - (int)shipTypeToGenerate + 1);
                int narrowerSideCoordinate = _random.Next(Constants.BoardSideSize);

                proposedCoordinates = Enumerable.Range(widerSideInitialCoordinate, (int)shipTypeToGenerate)
                    .Select(widerSideCoordinate => new Coordinate(
                        isHorizontal ? widerSideCoordinate : narrowerSideCoordinate,
                        isHorizontal ? narrowerSideCoordinate : widerSideCoordinate))
                    .ToHashSet(new CoordinateEqualityComparer());
            } while (existingShips.Any(p => p.Coordinates.Overlaps(proposedCoordinates)));

            return proposedCoordinates;
        }
    }
}
