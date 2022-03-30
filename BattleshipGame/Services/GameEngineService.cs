using BattleshipGame.Models;
using BattleshipGame.Services.Interfaces;
using BattleshipGame.Validator;
using System.ComponentModel.DataAnnotations;

namespace BattleshipGame.Services
{
    public class GameEngineService : IGameEngineService
    {
        private readonly IBoardManageService _boardManageService;
        private readonly ICoordinatesConvertionService _coordinatesConvertionService;
        private readonly IDisplayManageService _displayManageService;
        private readonly IValidatorWrapper _validatorWrapper;

        public GameEngineService(
            IBoardManageService boardService,
            ICoordinatesConvertionService coordinatesConvertionService,
            IDisplayManageService displayManageService,
            IValidatorWrapper validatorWrapper)
        {
            _boardManageService = boardService;
            _coordinatesConvertionService = coordinatesConvertionService;
            _displayManageService = displayManageService;
            _validatorWrapper = validatorWrapper;
        }

        public void Run()
        {
            var board = _boardManageService.GetDiscoveredCoordinates();
            _displayManageService.CoordinateProvided += DisplayManagerService_CoordinateProvided;
            _displayManageService.DrawValidView(board, coordinateAlphanumeric: null);
        }

        private void DisplayManagerService_CoordinateProvided(CoordinateProvidedEventArgs eventArgs)
        {
            IDictionary<Coordinate, ShipType?> discoveredCoordinates;

             if (!_validatorWrapper.TryValidateObject(eventArgs, out ICollection<ValidationResult> errorResults))
            {
                discoveredCoordinates = _boardManageService.GetDiscoveredCoordinates();
                _displayManageService.DrawErrorView(discoveredCoordinates, eventArgs.Coordinate, errorResults?.FirstOrDefault()?.ErrorMessage ?? string.Empty);
                return;
            }

            try
            {
                var coordinate = _coordinatesConvertionService.ConvertAlphanumericToCoordinate(eventArgs.Coordinate);
                var shipHit = _boardManageService.Shot(coordinate);
                bool isGameFinished = _boardManageService.AreAllShipsSunk();
                discoveredCoordinates = _boardManageService.GetDiscoveredCoordinates();

                if (isGameFinished)
                {
                    _displayManageService.DrawFinalView(discoveredCoordinates);
                }
                else
                {
                    _displayManageService.DrawValidView(discoveredCoordinates, eventArgs.Coordinate, shipHit);
                }
            }
            catch (ValidationException ex)
            {
                discoveredCoordinates = _boardManageService.GetDiscoveredCoordinates();
                _displayManageService.DrawErrorView(discoveredCoordinates, eventArgs.Coordinate, ex.Message);
            }

        }
    }
}
