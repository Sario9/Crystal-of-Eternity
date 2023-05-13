using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace Crystal_of_Eternity
{
    public static class MapGenerator
    {
        public static void GenerateGround(Texture2D[,] tiles, Point mapSize, LevelType type)
        {
            for (int i = 0; i < mapSize.X; i++)
            {
                for (int j = 0; j < mapSize.Y; j++)
                {
                    switch (type)
                    {
                        case LevelType.Level1:
                            {
                                var index = Randomizer.SelectRandomWithProbability(0.1, 0.15, 0.2, 0.25, 0.3);
                                tiles[i, j] = Tiles.Ground[type][index];
                                break;
                            }
                        case LevelType.Level2:
                            {
                                var index = Randomizer.SelectRandomWithProbability(0.78, 0.1, 0.1, 0.02);
                                tiles[i, j] = Tiles.Ground[type][index];
                                break;
                            }
                        default:
                            throw new NotImplementedException();
                    }
                }
            }
        }

        public static void GenerateEnvironment(Texture2D[,] tiles, Point mapSize, LevelType type)
        {
            for (int i = 0; i < mapSize.X; i++)
            {
                for (int j = 0; j < mapSize.Y; j++)
                {
                    switch (type)
                    {
                        case LevelType.Level1:
                            var index = Randomizer.SelectRandomWithProbability(0.02, 0.01, 0.02, 0.95);
                            if (index == 3) continue;
                            tiles[i, j] = Tiles.Environment[type][index];
                            break;
                        case LevelType.Level2:
                            continue;
                        default:
                            throw new NotImplementedException();
                    }
                }
            }
        }
    }
}
