using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace Crystal_of_Eternity
{
    public class TileMap
    {
        #region Fields
        public readonly Point Size;
        public readonly LevelType Type;

        private Texture2D[,] ground;
        private Texture2D[,] environment;
        private Collider[,] obstacles;
        #endregion

        public TileMap(LevelType levelType, int width, int height)
        {
            Size = new(width, height);
            Type = levelType;
            ground = new Texture2D[width, height];
            environment = new Texture2D[width, height];
            obstacles = new Collider[width, height];
            LoadContent();
        }

        public IEnumerable<Collider> GetObstacles()
        {
            foreach (var obstacle in obstacles)
                yield return obstacle;
        }

        private void LoadContent()
        {
            MapGenerator.GenerateGround(ground, Size, Type);
            MapGenerator.GenerateEnvironment(environment, Size, Type);
            for (int i = 0; i < Size.X; i++)
            {
                for (int j = 0; j < Size.Y; j++)
                {
                    if (environment[i, j] != null)
                        obstacles[i, j] = new Collider(new((new Vector2(i, j) - Vector2.One / 4)
                            * Tiles.TileSize.X, Tiles.TileSize * 0.4f), ColliderType.Obstacle, 0.0f);
                }
            }
        }

        public void DrawTile(Texture2D[,] tiles, int x, int y, int layer, SpriteBatch spriteBatch)
        {
            spriteBatch.Draw
            (
                tiles[x, y],
                (new Vector2(x, y) - Vector2.One / 2) * Tiles.TileSize.X,
                null,
                Color.White,
                0.0f,
                Vector2.Zero,
                1,
                SpriteEffects.None,
                layer
            );
        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            for (int i = 0; i < Size.X; i++)
                for (int j = 0; j < Size.Y; j++)
                    DrawTile(ground, i, j, 0, spriteBatch);
        }

        public void DrawObstacles(GameTime gameTime, SpriteBatch spriteBatch)
        {
            for (int i = 0; i < Size.X; i++)
                for (int j = 0; j < Size.Y; j++)
                    if (environment[i, j] != null)
                    {
                        DrawTile(environment, i, j, 1, spriteBatch);
                        //obstacles[i, j].Draw(spriteBatch);
                    }
        }
    }
}
