using BattleshipGame.Algorithms.Interfaces;
using BattleshipGame.Models;
using System.Collections.Immutable;

namespace BattleshipGame.Algorithms
{
    public class StaticShipPlacementStrategy : IShipPlacementStrategy
    {
        public IEnumerable<Ship> GenerateShips()
            => new List<Ship>
                {
                    GenerateShip(ShipType.Carrier, 2, 2, 2, 6),
                    GenerateShip(ShipType.Battleship, 8, 5, 8, 8),
                    GenerateShip(ShipType.Battleship, 3, 9, 6, 9)
                };

        private static Ship GenerateShip(
            ShipType shipType,
            int x1,
            int y1,
            int x2,
            int y2)
        {
            var shipLocation = GenerateCoordinates(x1, y1, x2, y2);
            return new Ship(shipType, ImmutableHashSet.Create<Coordinate>(shipLocation.ToArray()));
        }

        private static IEnumerable<Coordinate> GenerateCoordinates(int x1, int y1, int x2, int y2)
        {
            for (int i = x1; i <= x2; i++)
            {
                for (int j = y1; j <= y2; j++)
                {
                    yield return new Coordinate(i, j);
                }
            }
        }
    }
}
