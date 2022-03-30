using CommandLine;

namespace BattleshipGame
{
    public class Options
    {
        [Option('s', "static", Required = false, HelpText = "Use static ships placement strategy instead of random.")]
        public bool UseStaticShipPlacementStrategy { get; set; }
    }
}
