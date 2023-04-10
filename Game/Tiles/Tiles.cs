using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;

namespace Crystal_of_Eternity
{
    public static class Tiles
    {
        public static Vector2 TileSize => new(32, 32);

        public readonly static Dictionary<LevelType, List<Texture2D>> Ground;
        public readonly static Dictionary<LevelType, List<Texture2D>> Environment;

        private static Game game = MyGame.Instance;

        static Tiles()
        {
            Ground = new Dictionary<LevelType, List<Texture2D>>();
            Environment = new Dictionary<LevelType, List<Texture2D>>();

            LoadContent(LevelType.Level1, TileType.Ground);
            LoadContent(LevelType.Level1, TileType.Environment);
        }

        private static void LoadContent(LevelType levelType, TileType tileType)
        {
            switch (tileType)
            {
                case TileType.Ground:
                    AddTiles(Ground, levelType, "Sprites/Tiles/Grass_", 5);
                    break;

                case TileType.Environment:
                    AddTiles(Environment, levelType, "Sprites/Environment/Tree_", 3);
                    break;

                default:
                    throw new NotImplementedException();
            }
        }

        private static void AddTiles(Dictionary<LevelType, List<Texture2D>> tiles, LevelType type, string path, int count)
        {
            var tilesList = new List<Texture2D>();

            for (var i = 1; i <= count; i++)
                tilesList.Add(game.Content.Load<Texture2D>(string.Format("Game/{0}/{1}{2}", type.ToString(), path, i)));

            tiles.Add(type, tilesList);
        }
    }
}
