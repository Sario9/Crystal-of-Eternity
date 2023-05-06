﻿using Microsoft.Xna.Framework;
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
        public List<MovableEntity> MovableEntities { get; private set; }
        public RectangleF bounds => new RectangleF(Vector2.Zero - Vector2.One * 4, new(Map.Size.X * 31, Map.Size.Y * 31));

        public Vector2 PlayerStartPosition { get; private set; }

        public readonly CollisionComponent collisionComponent;

        private List<Corpse> corpses;

        public delegate void EnemiesHandler(int count);
        public event EnemiesHandler onEnemyDie;

        public Level(LevelType levelType, Point size, Vector2 playerStartPosition)
        {
            Map = new TileMap(levelType, size.X, size.Y);
            PlayerStartPosition = playerStartPosition;
            collisionComponent = new CollisionComponent(new RectangleF(0, 0, Map.Size.X * 32, Map.Size.Y * 32));
            MovableEntities = new List<MovableEntity>();
            corpses = new List<Corpse>();
        }

        public void Initialize()
        {
            Player = new Player(RandomPosition, 100.0f, 0.7f, 0.0f, bounds);
            SpawnEntity(() => new Skeleton(RandomPosition, bounds), 25);
            SpawnEntity(() => new Rogue(1, RandomPosition, bounds), 5);
            SpawnEntity(() => new Rogue(2, RandomPosition, bounds), 5);
            SpawnEntity(() => Player, 1);

            foreach (var entity in MovableEntities)
            {
                collisionComponent.Insert(entity);
                entity.OnDeath += KillEntity;
            }

            foreach (var obstacle in Map.GetObstacles().Where(x => x != null))
                collisionComponent.Insert(obstacle);
        }

        public void KillEntity(MovableEntity entity)
        {
            Debug.Print(entity.Name + " died");
            collisionComponent.Remove(entity);
            MovableEntities.Remove(entity);
            if (entity.CorpseSpritePath != "")
                corpses.Add(new Corpse(entity.CorpseSpritePath, entity.Position));
            entity.OnDeath -= KillEntity;
            if (entity is Enemy)
                onEnemyDie.Invoke(MovableEntities.Count - 1);
        }

        public void SpawnEntity(Func<MovableEntity> entity, int count)
        {
            for (int i = 0; i < count; i++)
                MovableEntities.Add(entity());
        }

        public void Update(GameTime gameTime)
        {
            Player.Update(gameTime);
            foreach (var entity in MovableEntities)
                entity.Update(gameTime);
            collisionComponent.Update(gameTime);
        }

        public void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            Map.Draw(gameTime, spriteBatch);
            foreach (var corpse in corpses)
                corpse.Draw(spriteBatch);
            foreach (var entity in MovableEntities)
                entity.Draw(gameTime, spriteBatch);
            //DrawBounds(spriteBatch);
        }

        private void DrawBounds(SpriteBatch spriteBatch)
        {
            spriteBatch.DrawRectangle(bounds, Color.Green, 5);
            foreach (var entity in MovableEntities)
                entity.DrawBounds(spriteBatch);
        }

        private Vector2 RandomPosition => Randomizer.NextVector2((int)bounds.Width, (int)bounds.Height);
    }
}
