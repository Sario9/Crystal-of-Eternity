using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended;
using MonoGame.Extended.Collisions;
using System.Collections.Generic;
using System.Linq;

namespace Crystal_of_Eternity
{
    public abstract class Room
    {
        #region Fields

        public CollisionComponent CollisionComponent { get; private set; }
        public TileMap Map { get; private set; }
        public readonly RectangleF Bounds;

        public int EnemiesCount => entities.Where(x => x is Enemy).Count();
        public bool IsCompleted { get; private set; }
        
        public delegate void EnemiesHandler(int count);
        public event EnemiesHandler OnEnemyDie;

        protected Player player;
        protected Vector2 playerStartPosition;

        protected List<IEntity> entities;
        protected int totalEnemies;
        protected List<Enemy> enemiesTypes;

        protected List<IEntity> corpses;
        private Point size;

        #endregion

        public Room(LevelType levelType, Point size, Vector2 playerStartPosition,
            int totalEnemies, List<Enemy> enemiesTypes)
        {
            Map = new TileMap(levelType, size.X, size.Y);
            Bounds = new RectangleF(Vector2.Zero - Vector2.One * 4, new(Map.Size.X * 31, Map.Size.Y * 31));
            CollisionComponent = new CollisionComponent(Bounds);

            this.playerStartPosition = playerStartPosition;
            this.totalEnemies = totalEnemies;
            this.enemiesTypes = enemiesTypes;
        }

        public virtual void Initialize(Player player)
        {
            CollisionComponent.Initialize();
            entities = new List<IEntity>();
            corpses = new List<IEntity>();

            SpawnEnemies(totalEnemies, enemiesTypes, player);
            SpawnPlayer(player);
            AddEntitesToColliders(entities.ToArray());
            AddObstaclesToColliders();
        }

        private void SpawnEnemies(int totalEnemies, List<Enemy> enemiesTypes, MovableEntity target)
        {
            SpawnEntities(totalEnemies, enemiesTypes.ToArray());
            var enemies = entities.Where(x => x is Enemy).Select(x => x as Enemy).ToList();
            foreach (var enemy in enemies)
            {
                enemy.OnDeath += KillEnemy;
                enemy.Spawn(RandomPosition, Bounds, target);
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

        protected void SpawnEntities(int count, params IEntity[] spawners)
        {
            var typesToSpawn = spawners.ToList();
            for (int i = 0; i < count; i++) 
            {
                var entity = Randomizer.RandomFromList(typesToSpawn).Clone() as IEntity;
                entities.Add(entity);
            }
        }

        protected void RandomSpawnEntities(int count, params IEntity[] spawners)
        {
            var typesToSpawn = spawners.ToList();
            for (int i = 0; i < count; i++)
            {
                var entity = Randomizer.RandomFromList(typesToSpawn).Clone() as IEntity;
                entity.Position = RandomPosition;
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
                    OnEnemyDie?.Invoke(EnemiesCount);
            }
            else
            {
                CollisionComponent.Remove(entity);
                entities.Remove(entity);
            }
        }

        public void Update(GameTime gameTime)
        {
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
    }
}
