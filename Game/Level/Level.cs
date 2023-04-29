using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended;
using MonoGame.Extended.Collisions;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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

        private List<Corpse> corpses;

        public Level(LevelType levelType, Point size, Vector2 playerStartPosition)
        {
            Map = new TileMap(levelType, size.X, size.Y);
            PlayerStartPosition = playerStartPosition;
            collisionComponent = new CollisionComponent(new RectangleF(0, 0, Map.Size.X * 32, Map.Size.Y * 32));
            Entities = new List<IEntity>();
            corpses = new List<Corpse>();
        }

        public void Initialize()
        {
            Player = new Player("Player", PlayerStartPosition, 100.0f, bounds);
            SpawnEntity(() => new Skeleton(randomPosition, bounds), 5);
            SpawnEntity(() => new Rogue(1, randomPosition, bounds), 5);
            SpawnEntity(() => new Rogue(2, randomPosition, bounds), 5);
            collisionComponent.Insert(Player);

            foreach (var entity in Entities)
                collisionComponent.Insert(entity);

            foreach (var obstacle in Map.GetObstacles().Where(x => x != null))
                collisionComponent.Insert(obstacle);

            Player.OnDeath += KillEntity;
        }

        public void KillEntity(IEntity entity)
        {
            collisionComponent.Remove(entity);
            collisionComponent.Dispose();
            Entities.Remove(entity);
            if(entity.CorpseSpritePath != "")
                corpses.Add(new Corpse(entity.CorpseSpritePath, entity.Position));
        }

        public void SpawnEntity(Func<IEntity> entity, int count)
        {
            for (int i = 0; i < count; i++)
                Entities.Add(entity());
        }

        public void Update(GameTime gameTime)
        {
            Player.Update(gameTime);
            foreach (var entity in Entities)
                entity.Update(gameTime);
            collisionComponent.Update(gameTime);
        }

        public void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            foreach (var corpse in corpses)
                corpse.Draw(spriteBatch);
            foreach (var entity in Entities)
                entity.Draw(gameTime, spriteBatch);
            Player.Draw(gameTime, spriteBatch);
            //DrawBounds(spriteBatch);
        }

        private void DrawBounds(SpriteBatch spriteBatch)
        {
            spriteBatch.DrawRectangle(bounds, Color.Green, 5);

            Player.DrawBounds(spriteBatch);
            foreach (var entity in Entities)
                entity.DrawBounds(spriteBatch);
        }

        private Vector2 randomPosition => Randomizer.NextVector2((int)bounds.Width, (int)bounds.Height);
    }
}
