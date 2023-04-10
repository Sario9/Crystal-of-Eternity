using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Crystal_of_Eternity
{
    public class TileMap
    {
        public readonly Point Size;
        public readonly LevelType Type;

        private Texture2D[,] ground;
        private Texture2D[,] environment;

        public TileMap(LevelType levelType, int width, int height)
        {
            Size = new(width, height);
            Type = levelType;
            ground = new Texture2D[width, height];
            environment = new Texture2D[width, height];
            LoadContent();
        }

        private void LoadContent()
        {
            MapGenerator.GenerateGround(ground, Size, Type);
            MapGenerator.GenerateEnvironment(environment, Size, Type);
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
            {
                for (int j = 0; j < Size.Y; j++)
                {
                    DrawTile(ground, i, j, 0, spriteBatch);

                    if (environment[i, j] != null)
                    {
                        DrawTile(environment, i, j, 1, spriteBatch);
                    }
                }
            }
        }
    }
}
