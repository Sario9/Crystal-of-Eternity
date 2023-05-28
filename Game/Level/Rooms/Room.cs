using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended;
using MonoGame.Extended.Collisions;
using MonoGame.Extended.Timers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;

namespace Crystal_of_Eternity
{
    public abstract class Room
    {
        #region Fields

        public CollisionComponent CollisionComponent { get; protected set; }
        public TileMap Map { get; protected set; }
        public readonly RectangleF Bounds;
        public readonly LevelType LevelType;

        public virtual int EnemiesCount => entities.Where(x => x is Enemy).Count();
        public bool IsCompleted { get; protected set; }
        
        public delegate void EnemiesHandler(int count);
        public event EnemiesHandler OnEnemyChangeState;

        protected Player player;
        protected Vector2 playerStartPosition;

        protected List<IEntity> entities;
        protected int totalEnemies;
        protected List<Enemy> enemiesTypes;
        protected Queue<Enemy> enemiesToSpawn;

        protected List<InteractableEntity> interactableEntities;
        protected List<IEntity> corpses;
        private Point size;

        protected GameState gameState;

        protected CountdownTimer spawnTimer;
        protected readonly float enemySpawnInterval = 0.5f;

        #endregion

        public Room(LevelType levelType, Point size, Vector2 playerStartPosition,
            int totalEnemies, List<Enemy> enemiesTypes)
        {
            Map = new TileMap(levelType, size.X, size.Y);
            Bounds = new RectangleF(Vector2.Zero - Vector2.One * 4, new(Map.Size.X * 31, Map.Size.Y * 31));
            CollisionComponent = new CollisionComponent(Bounds);
            LevelType = levelType;

            this.playerStartPosition = playerStartPosition;
            this.totalEnemies = totalEnemies;
            this.enemiesTypes = enemiesTypes;

            spawnTimer = new(enemySpawnInterval);
            spawnTimer.Stop();
        }

        public virtual void Initialize(Player player, GameState gameState)
        {
            this.gameState = gameState;

            CollisionComponent.Initialize();
            entities = new();
            corpses = new();
            enemiesToSpawn = new();
            interactableEntities = new();
        }

        protected virtual void Complete()
        {
            CreateInteractable(new Hatch(player.Position, gameState));
            OnEnemyChangeState = null;
        }

        public virtual void CreateInteractable(InteractableEntity interactable)
        {
            SpawnEntities(interactable);
            interactableEntities.Add(interactable);
        }

        protected void AddEnemies(int totalEnemies, List<Enemy> enemiesTypes)
        {
            for (int i = 0; i < totalEnemies; i++)
            {
                var entity = Randomizer.RandomFromList(enemiesTypes).Clone() as Enemy;
                enemiesToSpawn.Enqueue(entity);
            }
            spawnTimer.Start();
        }

        protected void SpawnEnemies(GameTime gameTime)
        {
            spawnTimer.Update(gameTime);
            if (enemiesToSpawn.Count != 0 && spawnTimer.State == TimerState.Completed)
            {
                var enemy = enemiesToSpawn.Dequeue();
                enemy.OnDeath += KillEnemy;

                enemy.Spawn(RandomPositionAwayFromPlayer(), Bounds, player);
                SpawnEntities(enemy);
                AddEntitesToColliders(enemy);
                OnEnemyChangeState.Invoke(EnemiesCount);

                spawnTimer.Restart();
            }
        }

        protected void AddObstaclesToColliders()
        {
            foreach (var obstacle in Map.GetObstacles().Where(x => x != null))
                CollisionComponent.Insert(obstacle);
        }

        protected void AddEntitesToColliders(params IEntity[] entities)
        {
            foreach (var entity in entities)
                CollisionComponent.Insert(entity);
        }

        protected void SpawnPlayer(Player player)
        {
            this.player = player;
            player.Spawn(playerStartPosition, Bounds, CollisionComponent);
            CollisionComponent.Insert(player);
        }

        protected void SpawnEntities(params IEntity[] spawners)
        {
            foreach (var entity in spawners)
                entities.Add(entity);
        }

        protected void SpawnRandomEntities(int count, params IEntity[] spawners)
        {
            var typesToSpawn = spawners.ToList();
            for (int i = 0; i < count; i++)
            {
                var entity = Randomizer.RandomFromList(typesToSpawn).Clone() as IEntity;
                entities.Add(entity);
            }
        }

        public void KillEnemy(IEntity entity)
        {
            if (entity is MovableEntity)
            {
                var movable = (MovableEntity)entity;

                CollisionComponent.Remove(entity);
                entities.Remove(entity);
                if (movable.CorpseSpritePath != "")
                    corpses.Add(new Corpse(movable.CorpseSpritePath, entity.Position));
                movable.OnDeath -= KillEnemy;

                if (entity is Enemy)
                    OnEnemyChangeState?.Invoke(EnemiesCount);
            }
            else
            {
                CollisionComponent.Remove(entity);
                entities.Remove(entity);
            }
        }

        public virtual void Update(GameTime gameTime)
        {
            SpawnEnemies(gameTime);
            var interactables = interactableEntities
                .Where(x => Vector2.Distance(x.Position, player.Position) <= player.InteractDistance)
                .OrderBy(x => Vector2.Distance(x.Position, player.Position));
            player.CurrentInteractable = interactables.FirstOrDefault();   

            player.Update(gameTime);
            foreach (var entity in entities)
                entity.Update(gameTime);
            CollisionComponent.Update(gameTime);
        }

        public void Draw(SpriteBatch spriteBatch, GameTime gameTime, bool drawBounds)
        {
            Map.Draw(gameTime, spriteBatch);

            foreach (var corpse in corpses)
                corpse.Draw(gameTime, spriteBatch);

            Map.DrawObstacles(gameTime, spriteBatch);

            foreach (var entity in entities)
                entity.Draw(gameTime, spriteBatch);

            player.Draw(gameTime, spriteBatch);

            if (drawBounds)
                DrawBounds(spriteBatch);
        }

        private void DrawBounds(SpriteBatch spriteBatch)
        {
            spriteBatch.DrawRectangle(Bounds, Color.Green, 5);
            player.DrawBounds(spriteBatch);
            foreach (var entity in entities)
                entity.DrawBounds(spriteBatch);
        }

        protected Vector2 RandomPosition => Randomizer.NextVector2((int)Bounds.Width, (int)Bounds.Height);

        protected Vector2 RandomPositionAwayFromPlayer()
        {
            while (true) 
            {
                var position = Randomizer.NextVector2((int)Bounds.Width, (int)Bounds.Height);
                if(Vector2.Distance(position, player.Position) > 100)
                    return position;
            }
        }
    }
}
