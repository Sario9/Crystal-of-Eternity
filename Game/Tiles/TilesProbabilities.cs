using System.Collections.Generic;

namespace Crystal_of_Eternity
{
    public static class TilesProbabilities
    {
        public static Dictionary<(LevelType level, TileType type), double[]> probabilities =
            new Dictionary<(LevelType level, TileType type), double[]>();

        static TilesProbabilities()
        {
            //1 уровень
            probabilities.Add((LevelType.Level1, TileType.Ground), new[] { 0.1, 0.15, 0.2, 0.25, 0.3 });
            probabilities.Add((LevelType.Level1, TileType.Environment), new[] { 0.02, 0.01, 0.02 });

            //2 уровень
            probabilities.Add((LevelType.Level2, TileType.Ground), new[] { 0.815, 0.04, 0.1, 0.04, 0.005 });
            probabilities.Add((LevelType.Level2, TileType.Environment), new[] { 0.005, 0.005, 0.01, 0.01 });
        }
    }
}
