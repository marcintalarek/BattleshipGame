using BattleshipGame.Algorithms;
using BattleshipGame.Algorithms.Interfaces;
using BattleshipGame.Services;
using BattleshipGame.Validator;
using CommandLine;

namespace BattleshipGame
{
    static class Program
    {
        static void Main(string[] args)
        {
            Parser.Default.ParseArguments<Options>(args)
                   .WithParsed<Options>(o =>
                   {
                       IShipPlacementStrategy shipPlacementStrategy = o.UseStaticShipPlacementStrategy
                        ? new StaticShipPlacementStrategy()
                        : new RandomShipPlacementStrategy();

                       var boardManageService = new BoardManageService(shipPlacementStrategy);
                       var coordinatesConversionService = new CoordinatesConversionService();
                       var displayManageService = new DisplayManageService();
                       var validatorWrapper = new ValidatorWrapper();

                       var gameEngineService = new GameEngineService(
                               boardManageService,
                               coordinatesConversionService,
                               displayManageService,
                               validatorWrapper
                           );

                       gameEngineService.Run();
                   });
        }
    }
}