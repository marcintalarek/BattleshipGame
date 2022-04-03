using CommandLine;

namespace BattleshipGame
{
    internal class Options
    {
        [Option('s', "static", Required = false, HelpText = "Use static ships placement strategy instead of random.")]
        public bool UseStaticShipPlacementStrategy { get; set; }
    }
}
