namespace BattleshipGame
{
    public static class RandomExtensions
    {
        // https://stackoverflow.com/questions/19191058/fastest-way-to-generate-a-random-boolean
        public static bool NextBool(this Random random)
            => random.NextDouble() >= 0.5;
    }
}
