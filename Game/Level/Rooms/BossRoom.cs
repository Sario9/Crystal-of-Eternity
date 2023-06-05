using Microsoft.Xna.Framework;
using System.Collections.Generic;
using System.Linq;

namespace Crystal_of_Eternity
{
    public class BossRoom : Room
    {
        private Boss boss;
        public override int EnemiesCount
        {
            get
            {
                var count = entities.Where(x => x is Enemy || x is Boss).Count();
                IsCompleted = count == 0;
                if (IsCompleted) Complete();
                return count;
            }
        }

        public BossRoom(LevelType levelType, Point size, Vector2 playerStartPosition, Boss boss, int totalEnemies,
            List<Enemy> enemiesTypes) :
            base(levelType, size, playerStartPosition, totalEnemies, enemiesTypes)
        {
            this.boss = boss;
        }

        public override void Initialize(Player player, GameState gameState)
        {
            base.Initialize(player, gameState);
            AddEnemies(totalEnemies, enemiesTypes);
            SpawnPlayer(player);
            SpawnBoss(boss);
            AddEntitesToColliders(entities.ToArray());
            AddObstaclesToColliders();
        }
    }
}
