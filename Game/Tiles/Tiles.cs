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

            LoadContent();
        }

        private static void LoadContent()
        {
            //уровень 1
            AddTiles(Ground, LevelType.Level1, TileType.Ground, TileNames.Grass);
            AddTiles(Environment, LevelType.Level1, TileNames.Tree, 3);

            //уровень 2
            AddTiles(Ground, LevelType.Level2, TileType.Ground, TileNames.Bricks);
            AddTiles(Environment, LevelType.Level2, TileNames.Vase, 3);
            AddTiles(Environment, LevelType.Level2, TileNames.Barrel, 1);
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

        private static void AddTiles(Dictionary<LevelType, List<Texture2D>> tiles, LevelType level, TileType type, string path)
        {
            var tilesList = new List<Texture2D>();
            var count = TilesProbabilities.probabilities[(level, type)].Length;

            for (var i = 1; i <= count; i++)
                tilesList.Add(game.Content.Load<Texture2D>(string.Format("Game/{0}/{1}{2}", level.ToString(), path, i)));

            if (!tiles.ContainsKey(level))
                tiles.Add(level, tilesList);
            else
                tiles[level].AddRange(tilesList);
        }
    }
}
