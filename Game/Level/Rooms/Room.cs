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
        protected List<IEntity> entitesToSpawn;

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
            entitesToSpawn = new();
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
            if (enemiesToSpawn.Count != 0 && player.IsAlive && spawnTimer.State == TimerState.Completed)
            {
                var enemy = enemiesToSpawn.Dequeue();
                enemy.OnDeath += DeleteEntity;

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

        protected virtual void SpawnBoss(Boss boss)
        {
            entitesToSpawn.Add(boss);
            boss.Spawn(RandomPositionAwayFromPlayer(), Bounds, player);
            boss.OnDeath += DeleteEntity;
            AddEntitesToColliders(boss);
        }

        protected void SpawnPlayer(Player player)
        {
            this.player = player;
            player.Spawn(playerStartPosition, Bounds, CollisionComponent);
            CollisionComponent.Insert(player);

            player.OnDeath += DeleteEntity;
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

        protected void SpawnDropable(DropableEntity dropable)
        {
            dropable.OnInteract += DeleteEntity;
            entitesToSpawn.Add(dropable);
        }

        public void DeleteEntity(IEntity entity)
        {
            if (entity is MovableEntity movable)
            {
                CollisionComponent.Remove(entity);
                entities.Remove(entity);
                if (movable.CorpseSpritePath != "")
                    corpses.Add(new Corpse(movable.CorpseSpritePath, entity.Position));
                movable.OnDeath -= DeleteEntity;

                if (entity is Enemy )
                {
                    OnEnemyChangeState?.Invoke(EnemiesCount);
                    var i = Randomizer.Random.Next(10);
                    if(i < 8)
                        SpawnDropable(new CoinDropable(entity.Position, player));
                }
                
                else if (entity is Boss )
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
            CollisionComponent.Update(gameTime);
            if (entitesToSpawn.Count > 0)
            {
                var toSpawn = entitesToSpawn.ToArray();
                SpawnEntities(toSpawn);
                AddEntitesToColliders(toSpawn);
                entitesToSpawn.Clear();

                OnEnemyChangeState.Invoke(EnemiesCount);
            }

            SpawnEnemies(gameTime);
            var interactables = interactableEntities
                .Where(x => Vector2.Distance(x.Position, player.Position) <= player.InteractDistance)
                .OrderBy(x => Vector2.Distance(x.Position, player.Position));
            player.CurrentInteractable = interactables.FirstOrDefault();   

            player.Update(gameTime);
            foreach (var entity in entities)
                entity.Update(gameTime);
        }

        public void Draw(SpriteBatch spriteBatch, GameTime gameTime, bool drawBounds)
        {
            Map.Draw(spriteBatch);

            foreach (var corpse in corpses)
                corpse.Draw(gameTime, spriteBatch);

            Map.DrawObstacles(spriteBatch);

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
            Map.DrawBounds(spriteBatch);
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
