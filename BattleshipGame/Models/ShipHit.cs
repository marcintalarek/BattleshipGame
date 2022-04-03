namespace BattleshipGame.Models
{
    internal readonly struct ShipHit
    {
        public ShipHit(ShipType shipType, bool isShipSunk)
        {
            ShipType = shipType;
            IsShipSunk = isShipSunk;
        }

        public ShipType ShipType { get; }
        public bool IsShipSunk { get; }
    }
}
