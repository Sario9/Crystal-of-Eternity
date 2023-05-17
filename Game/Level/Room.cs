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
    public class Room
    {
        public readonly CollisionComponent CollisionComponent;
        public bool isCompleted;

        public TileMap Map { get; private set; }
        public Player Player { get; private set; }

        public List<MovableEntity> MovableEntities { get; private set; }
        public int EnemiesCount => MovableEntities.Count - 1;
        private List<Corpse> corpses;

        public delegate void EnemiesHandler(int count);
        public event EnemiesHandler onEnemyDie;

        public RectangleF Bounds { get; private set; }
        private Vector2 playerStartPosition;

        public Room(LevelType levelType, Point size, Vector2 playerStartPosition)
        {
            Map = new TileMap(levelType, size.X, size.Y);
            Bounds = new RectangleF(Vector2.Zero - Vector2.One * 4, new(Map.Size.X * 31, Map.Size.Y * 31));
            CollisionComponent = new CollisionComponent(Bounds);
            this.playerStartPosition = playerStartPosition;
        }

        public void Initialize()
        {
            MovableEntities = new List<MovableEntity>();
            corpses = new List<Corpse>();

            Player = new Player(playerStartPosition, 100.0f, 0.7f, 0.0f, Bounds, CollisionComponent);
            SpawnEntity(() => new Skeleton(RandomPosition, Bounds, Player), 15);
            SpawnEntity(() => new Rogue(1, RandomPosition, Bounds, Player), 15);
            SpawnEntity(() => new Rogue(2, RandomPosition, Bounds, Player), 15);
            SpawnEntity(() => Player, 1);

            foreach (var entity in MovableEntities)
            {
                CollisionComponent.Insert(entity);
                if(!(entity is Player))
                    entity.OnDeath += KillEntity;
            }

            foreach (var obstacle in Map.GetObstacles().Where(x => x != null))
                CollisionComponent.Insert(obstacle);
        }

        public void KillEntity(MovableEntity entity)
        {
            Debug.Print(entity.Name + " died");
            CollisionComponent.Remove(entity);
            MovableEntities.Remove(entity);
            if (entity.CorpseSpritePath != "")
                corpses.Add(new Corpse(entity.CorpseSpritePath, entity.Position));
            entity.OnDeath -= KillEntity;
            if (entity is Enemy)
                onEnemyDie?.Invoke(MovableEntities.Count - 1);
        }

        public void SpawnEntity(Func<MovableEntity> entity, int count)
        {
            for (int i = 0; i < count; i++)
                MovableEntities.Add(entity());
        }

        private void CompleteRoom()
        {
            isCompleted = true;
        }

        public void Update(GameTime gameTime)
        {
            Player.Update(gameTime);
            foreach (var entity in MovableEntities)
                entity.Update(gameTime);
            CollisionComponent.Update(gameTime);
            if(EnemiesCount == 0 && !isCompleted)
                CompleteRoom();
        }

        public void Draw(SpriteBatch spriteBatch, GameTime gameTime, bool drawBounds)
        {
            Map.Draw(gameTime, spriteBatch);
            foreach (var corpse in corpses)
                corpse.Draw(spriteBatch);
            foreach (var entity in MovableEntities)
                entity.Draw(gameTime, spriteBatch);
            if (drawBounds)
                DrawBounds(spriteBatch);
        }

        private void DrawBounds(SpriteBatch spriteBatch)
        {
            spriteBatch.DrawRectangle(Bounds, Color.Green, 5);
            foreach (var entity in MovableEntities)
                entity.DrawBounds(spriteBatch);
        }

        private Vector2 RandomPosition => Randomizer.NextVector2((int)Bounds.Width, (int)Bounds.Height);
    }
}
