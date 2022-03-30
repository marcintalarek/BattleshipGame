using BattleshipGame.Algorithms;
using BattleshipGame.Models;
using NSubstitute;
using System;
using System.Linq;
using Xunit;

namespace BattleshipGame.Tests
{
    public class RandomShipPlacementStrategyTests
    {
        private readonly Random _random;

        public RandomShipPlacementStrategyTests()
        {
            const double CarrierIsVertical = 0.0;
            const double Battleship1IsVertical = 0.0;
            const double Battleship2IsHorizontal = 0.99;
            const int CarrierWiderSideInitialCoordinate = 2;
            const int CarrierNarrowerSideInitialCoordinate = 2;
            const int Battleship1WiderSideInitialCoordinate = 5;
            const int Battleship1NarrowerSideInitialCoordinate = 8;
            const int Battleship2WiderSideInitialCoordinate = 3;
            const int Battleship2NarrowerSideInitialCoordinate = 9;

            _random = Substitute.For<Random>();

            _random.NextDouble().Returns(
                CarrierIsVertical,
                Battleship1IsVertical,
                Battleship2IsHorizontal);

            _random.Next(Arg.Any<int>()).Returns(
                CarrierWiderSideInitialCoordinate,
                CarrierNarrowerSideInitialCoordinate,
                Battleship1WiderSideInitialCoordinate,
                Battleship1NarrowerSideInitialCoordinate,
                Battleship2WiderSideInitialCoordinate,
                Battleship2NarrowerSideInitialCoordinate);
        }

        [Fact]
        public void GenerateShips_NumberOfGeneratedShipsAndCoordinates_IsValid()
        {
            int numberOfShips = 0;
            int numberOfCoordinates = 0;
            var strategy = new RandomShipPlacementStrategy(_random);

            var ships = strategy.GenerateShips();
            foreach(var ship in ships)
            {
                numberOfShips++;
                numberOfCoordinates += ship.Coordinates.Count;
            }

            Assert.Equal(numberOfShips, Constants.ShipsOnBoard.Length);
            Assert.Equal(numberOfCoordinates, Constants.NumberOfCoordinatesWithShips);
        }


        [Theory]
        [InlineData(2, 2, ShipType.Carrier, 0)]
        [InlineData(2, 3, ShipType.Carrier, 0)]
        [InlineData(2, 4, ShipType.Carrier, 0)]
        [InlineData(2, 5, ShipType.Carrier, 0)]
        [InlineData(2, 6, ShipType.Carrier, 0)]
        [InlineData(8, 5, ShipType.Battleship, 1)]
        [InlineData(8, 6, ShipType.Battleship, 1)]
        [InlineData(8, 7, ShipType.Battleship, 1)]
        [InlineData(8, 8, ShipType.Battleship, 1)]
        [InlineData(3, 9, ShipType.Battleship, 2)]
        [InlineData(4, 9, ShipType.Battleship, 2)]
        [InlineData(5, 9, ShipType.Battleship, 2)]
        [InlineData(6, 9, ShipType.Battleship, 2)]
        public void GenerateShips_ShipCoordinatesSetup_FulfilledCorrectly(
            int x,
            int y,
            ShipType shipType,
            int position)
        {
            var strategy = new RandomShipPlacementStrategy(_random);

            var ships = strategy.GenerateShips();
            var ship = ships.Skip(position).First();

            Assert.Equal(ship.ShipType, shipType);
            Assert.True(ship.Coordinates.TryGetValue(new Coordinate(x, y), out Coordinate coordinate));
            Assert.Equal(coordinate.X, x);
            Assert.Equal(coordinate.Y, y);
        }
    }
}