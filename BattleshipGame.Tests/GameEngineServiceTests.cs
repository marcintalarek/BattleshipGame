using BattleshipGame.Models;
using BattleshipGame.Services;
using BattleshipGame.Services.Interfaces;
using BattleshipGame.Validator;
using NSubstitute;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Xunit;

namespace BattleshipGame.Tests
{
    public class GameEngineServiceTests
    {
        private const string CoordinateAlphanumeric = "CoordinateAlphanumeric";

        private readonly IBoardManageService _boardManageService;
        private readonly ICoordinatesConvertionService _coordinatesConvertionService;
        private readonly IDisplayManageService _displayManageService;
        private readonly IValidatorWrapper _validatorWrapper;
        private readonly GameEngineService _gameEngineService;
        private readonly Dictionary<Coordinate, ShipType?> _discoveredCoordinates;

        public GameEngineServiceTests()
        {
            _boardManageService = Substitute.For<IBoardManageService>();
            _coordinatesConvertionService = Substitute.For<ICoordinatesConvertionService>();
            _displayManageService = Substitute.For<IDisplayManageService>();
            _validatorWrapper = Substitute.For<IValidatorWrapper>();
            _gameEngineService = new GameEngineService(
                _boardManageService, 
                _coordinatesConvertionService, 
                _displayManageService, 
                _validatorWrapper);

            _discoveredCoordinates = new Dictionary<Coordinate, ShipType?>();
        }

        [Fact]
        internal void Run_OnExecution_EventIsNotExecuted_And_ViewIsDrew()
        {
            _boardManageService.GetDiscoveredCoordinates().Returns(_discoveredCoordinates);
            bool wasEventExecuted = false;
            _displayManageService.CoordinateProvided += (CoordinateProvidedEventArgs e) => wasEventExecuted = true;

            _gameEngineService.Run();
            _displayManageService.Received(1).DrawValidView(_discoveredCoordinates, null);
            Assert.False(wasEventExecuted);
        }

        [Fact]
        internal void DisplayManagerServiceCoordinateProvided_IfCoordinateInvalid_DrawsErrorView()
        {
            var eventArgs = new CoordinateProvidedEventArgs(CoordinateAlphanumeric);
            _validatorWrapper.TryValidateObject(eventArgs, out Arg.Any<ICollection<ValidationResult>>()).Returns(false);
            _boardManageService.GetDiscoveredCoordinates().Returns(_discoveredCoordinates);

            _gameEngineService.Run();
            _displayManageService.CoordinateProvided += Raise.Event<CoordinateProvidedEventHandler>(eventArgs);

            _displayManageService.Received(1).DrawErrorView(_discoveredCoordinates, CoordinateAlphanumeric, string.Empty);
        }

        [Fact]
        internal void DisplayManagerServiceCoordinateProvided_IfGameFinished_DrawsFinalView()
        {
            var eventArgs = new CoordinateProvidedEventArgs(CoordinateAlphanumeric);
            _validatorWrapper.TryValidateObject(eventArgs, out _).Returns(true);
            _boardManageService.GetDiscoveredCoordinates().Returns(_discoveredCoordinates);
            _boardManageService.AreAllShipsSunk().Returns(true);

            _gameEngineService.Run();
            _displayManageService.CoordinateProvided += Raise.Event<CoordinateProvidedEventHandler>(eventArgs);

            _displayManageService.Received(1).DrawFinalView(_discoveredCoordinates);
        }

        [Fact]
        internal void DisplayManagerServiceCoordinateProvided_IfGameNotFinished_DrawsValidView()
        {
            var eventArgs = new CoordinateProvidedEventArgs(CoordinateAlphanumeric);
            _validatorWrapper.TryValidateObject(eventArgs, out _).Returns(true);
            _boardManageService.Shot(Arg.Any<Coordinate>()).Returns((ShipHit?)null);
            _boardManageService.GetDiscoveredCoordinates().Returns(_discoveredCoordinates);
            _boardManageService.AreAllShipsSunk().Returns(false);

            _gameEngineService.Run();
            _displayManageService.CoordinateProvided += Raise.Event<CoordinateProvidedEventHandler>(eventArgs);

            _displayManageService.Received(1).DrawValidView(_discoveredCoordinates, CoordinateAlphanumeric, null);
        }

        [Fact]
        internal void DisplayManagerServiceCoordinateProvided_ShotThrowsArgumentException_DrawsErrorView()
        {
            const string exceptionMessage = "test";
            var eventArgs = new CoordinateProvidedEventArgs(CoordinateAlphanumeric);
            _validatorWrapper.TryValidateObject(eventArgs, out var validationResults).Returns(true);
            _boardManageService.When(x => x.Shot(Arg.Any<Coordinate>())).Do(x => { throw new ValidationException(exceptionMessage); });
            _boardManageService.GetDiscoveredCoordinates().Returns(_discoveredCoordinates);

            _gameEngineService.Run();
            _displayManageService.CoordinateProvided += Raise.Event<CoordinateProvidedEventHandler>(eventArgs);

            _displayManageService.Received(1).DrawErrorView(_discoveredCoordinates, CoordinateAlphanumeric, exceptionMessage);
        }
    }
}
