using BattleshipGame.Models;
using System.Diagnostics.CodeAnalysis;

namespace BattleshipGame
{
    public class CoordinateEqualityComparer : IEqualityComparer<Coordinate>
    {
        public bool Equals(Coordinate coordinate1, Coordinate coordinate2)
            => coordinate1.X == coordinate2.X && coordinate1.Y == coordinate2.Y;

        public int GetHashCode([DisallowNull] Coordinate coordinate)
            => int.Parse($"{coordinate.X}{coordinate.Y}");
    }
}
