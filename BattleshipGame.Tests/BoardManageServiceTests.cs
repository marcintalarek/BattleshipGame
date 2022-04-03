using BattleshipGame.Algorithms.Interfaces;
using BattleshipGame.Models;
using BattleshipGame.Services;
using NSubstitute;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.ComponentModel.DataAnnotations;
using Xunit;

namespace BattleshipGame.Tests
{
    public class BoardManageServiceTests
    {
        [Fact]
        internal void Shot_SameCordinateForEmptyField_ThrowsValidationException()
        {
            var shipPlacementStrategy = Substitute.For<IShipPlacementStrategy>();
            var boardManageService = new BoardManageService(shipPlacementStrategy);
            var coordinate = new Coordinate(1, 1);

            boardManageService.Shot(coordinate);

            var exception = Assert.Throws<ValidationException>(() => boardManageService.Shot(coordinate));
            Assert.Equal("Coordinate already discovered.", exception.Message);
        }

        [Fact]
        internal void Shot_SameCordinateForFieldWithShip_ThrowsValidationException()
        {
            var shipPlacementStrategy = Substitute.For<IShipPlacementStrategy>();
            shipPlacementStrategy.GenerateShips().Returns(new List<Ship>
            {
                new Ship(ShipType.Carrier, ImmutableHashSet.Create(new Coordinate(1,1)))
            });
            var boardManageService = new BoardManageService(shipPlacementStrategy);
            var coordinate = new Coordinate(1, 1);

            boardManageService.Shot(coordinate);

            var exception = Assert.Throws<ValidationException>(() => boardManageService.Shot(coordinate));
            Assert.Equal("Coordinate already discovered.", exception.Message);
        }

        [Fact]
        internal void Shot_CoordinateWithoutShip_ReturnsNull()
        {
            var shipPlacementStrategy = Substitute.For<IShipPlacementStrategy>();
            var boardManageService = new BoardManageService(shipPlacementStrategy);
            var coordinate = new Coordinate(1, 1);

            var result = boardManageService.Shot(coordinate);

            Assert.Null(result);
        }

        [Fact]
        internal void Shot_CoordinateWithShip_ReturnsTypeAndIfSunkProperly()
        {
            var shipPlacementStrategy = Substitute.For<IShipPlacementStrategy>();
            shipPlacementStrategy.GenerateShips().Returns(new List<Ship>
            {
                new Ship(ShipType.Carrier, ImmutableHashSet.Create(new Coordinate(1,1), new Coordinate(1,2)))
            });
            var boardManageService = new BoardManageService(shipPlacementStrategy);

            var firstShot = boardManageService.Shot(new Coordinate(1, 1));
            var secondShot = boardManageService.Shot(new Coordinate(1, 2));

            Assert.Equal(ShipType.Carrier, firstShot?.ShipType);
            Assert.False(firstShot?.IsShipSunk);

            Assert.Equal(ShipType.Carrier, secondShot?.ShipType);
            Assert.True(secondShot?.IsShipSunk);
        }

        [Fact]
        internal void AreAllShipsSunk_WithoutHittingOfAllShips_ReturnsFalse()
        {
            var shipPlacementStrategy = Substitute.For<IShipPlacementStrategy>();
            var boardManageService = new BoardManageService(shipPlacementStrategy);

            boardManageService.Shot(new Coordinate(0, 0));

            Assert.False(boardManageService.AreAllShipsSunk());
        }

        [Fact]
        internal void AreAllShipsSunk_WithHittingOfAllThirteenShips_ReturnsTrue()
        {
            var shipPlacementStrategy = Substitute.For<IShipPlacementStrategy>();
            shipPlacementStrategy.GenerateShips().Returns(new List<Ship>
            {
                new Ship(ShipType.Carrier, ImmutableHashSet.Create(
                    new Coordinate(0,0),
                    new Coordinate(0,1),
                    new Coordinate(0,2),
                    new Coordinate(0,3),
                    new Coordinate(0,4),
                    new Coordinate(0,5),
                    new Coordinate(0,6),
                    new Coordinate(0,7),
                    new Coordinate(0,8),
                    new Coordinate(0,9),
                    new Coordinate(1,0),
                    new Coordinate(1,1),
                    new Coordinate(1,2)))
            });
            var boardManageService = new BoardManageService(shipPlacementStrategy);

            boardManageService.Shot(new Coordinate(0, 0));
            boardManageService.Shot(new Coordinate(0, 1));
            boardManageService.Shot(new Coordinate(0, 2));
            boardManageService.Shot(new Coordinate(0, 3));
            boardManageService.Shot(new Coordinate(0, 4));
            boardManageService.Shot(new Coordinate(0, 5));
            boardManageService.Shot(new Coordinate(0, 6));
            boardManageService.Shot(new Coordinate(0, 7));
            boardManageService.Shot(new Coordinate(0, 8));
            boardManageService.Shot(new Coordinate(0, 9));
            boardManageService.Shot(new Coordinate(1, 0));
            boardManageService.Shot(new Coordinate(1, 1));
            boardManageService.Shot(new Coordinate(1, 2));

            Assert.True(boardManageService.AreAllShipsSunk());
        }

        [Fact]
        internal void GetDiscoveredCoordinates_ForNoShots_ReturnsEmptyDictionary()
        {
            var shipPlacementStrategy = Substitute.For<IShipPlacementStrategy>();
            var boardManageService = new BoardManageService(shipPlacementStrategy);

            var result = boardManageService.GetDiscoveredCoordinates();

            Assert.Equal(0, result.Count);
        }

        [Fact]
        internal void GetDiscoveredCoordinates_ForOneHitAndOneMiss_ReturnsTwoElementsInDictionary()
        {
            var shipPlacementStrategy = Substitute.For<IShipPlacementStrategy>();
            shipPlacementStrategy.GenerateShips().Returns(new List<Ship>
            {
                new Ship(ShipType.Carrier, ImmutableHashSet.Create(new Coordinate(1,2)))
            });
            var boardManageService = new BoardManageService(shipPlacementStrategy);

            boardManageService.Shot(new Coordinate(1, 1));
            boardManageService.Shot(new Coordinate(1, 2));

            var result = boardManageService.GetDiscoveredCoordinates();

            Assert.Equal(2, result.Count);
            Assert.Null(result[new Coordinate(1, 1)]);
            Assert.Equal(ShipType.Carrier, result[new Coordinate(1, 2)]);
        }
    }
}
