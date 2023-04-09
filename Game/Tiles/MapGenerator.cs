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
                            var index = Randomizer.SelectRandomWithProbability(10, 15, 20, 25, 30);
                            tiles[i, j] = Tiles.Ground[type][index];
                            break;
                        default:
                            throw new NotImplementedException();
                    }
                }
            }
        }
    }
}
