using Microsoft.Xna.Framework;
using System.Collections.Generic;
using System.Linq;

namespace Crystal_of_Eternity
{
    public class DefaultRoom : Room
    {
        public override int EnemiesCount
        {
            get
            {
                var count = entities.Where(x => x is Enemy).Count();
                IsCompleted = count == 0;
                if(IsCompleted) Complete();
                return count;
            }
        }

        public DefaultRoom(LevelType levelType, Point size, Vector2 playerStartPosition,
            int enemiesCount, List<Enemy> enemiesTypes)
            : base(levelType, size, playerStartPosition, enemiesCount, enemiesTypes)
        {
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }

        protected override void Complete()
        {
            base.Complete();
        }
    }
}
