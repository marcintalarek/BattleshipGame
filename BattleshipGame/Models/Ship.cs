using System.Collections.Immutable;

namespace BattleshipGame.Models
{
    public readonly struct Ship
    {
        public Ship(ShipType shipType, ImmutableHashSet<Coordinate> coordinates)
        {
            ShipType = shipType;
            Coordinates = coordinates;
        }

        public ShipType ShipType { get; }
        public ImmutableHashSet<Coordinate> Coordinates { get; }
    }
}
