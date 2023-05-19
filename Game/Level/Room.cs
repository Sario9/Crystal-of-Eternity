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
        #region Fields
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

        private Func<MovableEntity>[] enemiesTypes;
        private List<Func<MovableEntity>> enemies;
        private int startEnemiesCount;
        #endregion

        public Room(LevelType levelType, Point size, Vector2 playerStartPosition, int enemiesCount, params Func<MovableEntity>[] enemiesTypes)
        {
            Map = new TileMap(levelType, size.X, size.Y);
            enemies = new List<Func<MovableEntity>>();
            Bounds = new RectangleF(Vector2.Zero - Vector2.One * 4, new(Map.Size.X * 31, Map.Size.Y * 31));
            CollisionComponent = new CollisionComponent(Bounds);
            this.playerStartPosition = playerStartPosition;
            this.enemiesTypes = enemiesTypes;
            startEnemiesCount = enemiesCount;
        }

        public Room(RoomPreferences prefs) : this(prefs.Level, prefs.Size, prefs.PlayerStartPosition, prefs.EnemiesCount, prefs.Enemies)
        {

        }

        public void Initialize(Player player)
        {
            MovableEntities = new List<MovableEntity>();
            corpses = new List<Corpse>();
            player.Initialize(playerStartPosition, Bounds, CollisionComponent);
            Player = player;

            AddEnemies(startEnemiesCount);
            SpawnEntities(enemies.ToArray());
            SpawnEntities(() => player);

            foreach (var entity in MovableEntities)
            {
                CollisionComponent.Insert(entity);
                if (!(entity is Player))
                    entity.OnDeath += KillEntity;
            }

            foreach (var obstacle in Map.GetObstacles().Where(x => x != null))
                CollisionComponent.Insert(obstacle);
        }

        private void AddEnemies(int count)
        {
            for (int i = 0; i < count; i++)
            {
                var enemy = Randomizer.RandomFromList(enemiesTypes.ToList());
                enemies.Add(enemy);
            }
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

        public void SpawnEntities(params Func<MovableEntity>[] spawners)
        {
            foreach (var spawner in spawners)
                MovableEntities.Add(spawner());
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
            if (EnemiesCount == 0 && !isCompleted)
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
    }
}
