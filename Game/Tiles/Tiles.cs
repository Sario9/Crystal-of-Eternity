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
            LoadContent(LevelType.Level2, TileType.Ground);
        }

        private static void LoadContent(LevelType levelType, TileType tileType)
        {
            if(tileType == TileType.Ground)
            {
                switch (levelType)
                {
                    case LevelType.Level1:
                            AddTiles(Ground, levelType, TileNames.Grass, 5);
                            break;
                    case LevelType.Level2:
                        AddTiles(Ground, levelType, TileNames.Bricks, 4);
                        break;
                    default:
                        throw new NotImplementedException();
                }
            }
            else if(tileType == TileType.Environment)
            {
                switch (levelType)
                {
                    case LevelType.Level1:
                        AddTiles(Environment, levelType, TileNames.Tree, 3);
                        break;
                    case LevelType.Level2:
                        break;
                    default:
                        throw new NotImplementedException();
                }
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
