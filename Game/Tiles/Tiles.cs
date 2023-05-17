using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;

namespace Crystal_of_Eternity
{
    public static class Tiles
    {
        #region Fields
        public static Vector2 TileSize => new(32, 32);

        public static readonly Dictionary<LevelType, List<Texture2D>> Ground;
        public static readonly Dictionary<LevelType, List<Texture2D>> Environment;

        private static Game game = MyGame.Instance; 
        #endregion

        static Tiles()
        {
            Ground = new Dictionary<LevelType, List<Texture2D>>();
            Environment = new Dictionary<LevelType, List<Texture2D>>();

            LoadContent(LevelType.Level1);
            LoadContent(LevelType.Level2);
        }

        private static void LoadContent(LevelType levelType)
        {
            switch (levelType)
            {
                case (LevelType.Level1):
                    AddTiles(Ground, levelType, TileNames.Grass, 5);
                    AddTiles(Environment, levelType, TileNames.Tree, 3);
                    break;
                case (LevelType.Level2):
                    AddTiles(Ground, levelType, TileNames.Bricks, 5);
                    AddTiles(Environment, levelType, TileNames.Vase, 3);
                    AddTiles(Environment, levelType, TileNames.Barrel, 1);
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

            if (!tiles.ContainsKey(type))
                tiles.Add(type, tilesList);
            else
                tiles[type].AddRange(tilesList);
        }
    }
}
