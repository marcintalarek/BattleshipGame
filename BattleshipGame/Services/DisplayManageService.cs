using BattleshipGame.Models;
using BattleshipGame.Services.Interfaces;

namespace BattleshipGame.Services
{
    public delegate void CoordinateProvidedEventHandler(CoordinateProvidedEventArgs e);

    public class DisplayManageService : IDisplayManageService
    {
        public event CoordinateProvidedEventHandler? CoordinateProvided;

        public void DrawValidView(
            IDictionary<Coordinate, ShipType?> discoveredCoordinates, 
            string? coordinateAlphanumeric, 
            ShipHit? shipHit = null)
        {
            string message = string.Empty;
            if (!string.IsNullOrEmpty(coordinateAlphanumeric))
            {
                if (shipHit.HasValue)
                {
                    message = $"{coordinateAlphanumeric}.. Hit! {shipHit.Value.ShipType}{(shipHit.Value.IsShipSunk ? ", sunk!" : ".")}";
                }
                else
                {
                    message = $"{coordinateAlphanumeric}.. Miss!";
                }
            }

            DrawView(discoveredCoordinates, message);
        }

        public void DrawErrorView(
            IDictionary<Coordinate, ShipType?> discoveredCoordinates, 
            string coordinateAlphanumeric, 
            string message)
        {
            string warning = $"WARNING! Value: \"{coordinateAlphanumeric}\" Message: {message}";

            DrawView(discoveredCoordinates, warning);
        }

        public void DrawFinalView(IDictionary<Coordinate, ShipType?> discoveredCoordinates)
        {
            Console.Clear();

            BuildWelcomeMessage();
            BuildLegend();
            BuildBoard(discoveredCoordinates);
            BuildFinalMessage();
        }

        private void DrawView(
            IDictionary<Coordinate, ShipType?> discoveredCoordinates, 
            string? message = null)
        {
            Console.Clear();

            BuildWelcomeMessage();
            BuildLegend();
            BuildBoard(discoveredCoordinates);

            BuildMessage(message);
            BuildCoordinatesInput();
        }

        private static void BuildWelcomeMessage()
        {
            Console.WriteLine("Let's play Battleship!");
            Console.WriteLine();
        }

        private static void BuildBoard(IDictionary<Coordinate, ShipType?> discoveredCoordinates)
        {
            Console.WriteLine("  1 2 3 4 5 6 7 8 9 10");

            for (int i = 0; i < Constants.BoardSideSize; i++)
            {
                Console.Write((char)(Constants.LetterA+i));

                for (int j = 0; j < Constants.BoardSideSize; j++)
                {
                    var coordinate = new Coordinate(i, j);

                    if (discoveredCoordinates.ContainsKey(coordinate))
                    {
                        ShipType? shipType = discoveredCoordinates[coordinate];

                        if (shipType.HasValue)
                        {
                            Console.Write($" {(int)shipType.Value}");
                        }
                        else
                        {
                            Console.Write(" X");
                        }
                    }
                    else
                    {
                        Console.Write("  ");
                    }
                }

                Console.WriteLine();
            }
        }

        private static void BuildLegend()
        {
            Console.WriteLine("Legend:");
            Console.WriteLine("4, 5 - size of hit ship");
            Console.WriteLine("X - missed place");
            Console.WriteLine();
        }

        private static void BuildMessage(string? message)
        {
            Console.WriteLine();
            Console.WriteLine(message);
            Console.WriteLine();
        }

        private void BuildCoordinatesInput()
        {
            Console.WriteLine("Enter coordinate and press Enter");
            Console.WriteLine("(A to J, 1 to 10, ex. D7)");
            Console.Write("> ");
            string coordinate = Console.ReadLine() ?? string.Empty;

            CoordinateProvided?.Invoke(new CoordinateProvidedEventArgs(coordinate));
        }

        private static void BuildFinalMessage()
        {
            Console.WriteLine();
            Console.WriteLine("Congratulations! You win!!");
            Console.WriteLine();
            Console.WriteLine("Press any button to close.");
            Console.ReadKey();
        }
    }
}
