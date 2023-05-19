using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace Crystal_of_Eternity
{
    public static class MapGenerator
    {
        public static void GenerateGround(Texture2D[,] tiles, Point mapSize, LevelType level)
        {
            for (int i = 0; i < mapSize.X; i++)
            {
                for (int j = 0; j < mapSize.Y; j++)
                {
                    var probabilities = TilesProbabilities.probabilities[(level, TileType.Ground)];
                    var index = Randomizer.SelectRandomWithProbability(probabilities);
                    tiles[i, j] = Tiles.Ground[level][index];
                }
            }
        }

        public static void GenerateEnvironment(Texture2D[,] tiles, Point mapSize, LevelType level)
        {
            for (int i = 0; i < mapSize.X; i++)
            {
                for (int j = 0; j < mapSize.Y; j++)
                {
                    var probabilities = TilesProbabilities.probabilities[(level, TileType.Environment)];
                    var index = Randomizer.SelectRandomWithProbability(probabilities);
                    if (index == probabilities.Length) continue;
                    tiles[i, j] = Tiles.Environment[level][index];
                }
            }
        }
    }
}
