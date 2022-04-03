namespace BattleshipGame
{
    internal static class Constants
    {
        public const char LetterA = 'A';
        public const char LetterJ = 'J';

        public const int BoardSideSize = 10;

        public static readonly ShipType[] ShipsOnBoard = new ShipType[] {
            ShipType.Carrier,
            ShipType.Battleship,
            ShipType.Battleship
        };

        public static readonly int NumberOfCoordinatesWithShips = 
            (int)ShipType.Carrier + 
            (int)ShipType.Battleship + 
            (int)ShipType.Battleship;
    }
}
