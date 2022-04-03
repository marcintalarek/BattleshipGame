using System.ComponentModel.DataAnnotations;

namespace BattleshipGame.Models
{
    internal class CoordinateProvidedEventArgs : EventArgs
    {
        public CoordinateProvidedEventArgs(string coordinate)
        {
            Coordinate = coordinate;
        }

        [Required(ErrorMessage = "Invalid coordinate value.")]
        [RegularExpression("^[a-jA-J]([1-9]|10)$", ErrorMessage = "Invalid coordinate value.")]
        public string Coordinate { get; }
    }
}
