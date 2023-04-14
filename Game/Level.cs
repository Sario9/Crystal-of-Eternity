using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended;
using MonoGame.Extended.Collisions;
using System.Collections.Generic;
using System.Linq;

namespace Crystal_of_Eternity
{
    public class Level
    {
        public TileMap Map { get; private set; }
        public Player Player { get; private set; }
        public List<IEntity> Entities { get; private set; }
        public RectangleF bounds => new RectangleF(Vector2.Zero - Vector2.One * 4, new(Map.Size.X * 31, Map.Size.Y * 31));

        public Vector2 PlayerStartPosition { get; private set; }

        public readonly CollisionComponent collisionComponent;

        public Level(LevelType levelType, Point size, Vector2 playerStartPosition)
        {
            Map = new TileMap(levelType, size.X, size.Y);
            PlayerStartPosition = playerStartPosition;
            collisionComponent = new CollisionComponent(new RectangleF(0, 0, Map.Size.X * 32, Map.Size.Y * 32));
            Entities = new List<IEntity>();
        }

        public void Initialize()
        {
            Player = new Player("Player", PlayerStartPosition, 100.0f, bounds);
            collisionComponent.Insert(Player);
            foreach (var obstacle in Map.GetObstacles().Where(x => x != null))
            {
                collisionComponent.Insert(obstacle);
            }
        }

        public void Update(GameTime gameTime)
        {
            collisionComponent.Update(gameTime);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            //spriteBatch.DrawRectangle(bounds, Color.Green, 5);
        }
    }
}
