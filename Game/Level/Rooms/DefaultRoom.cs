using Microsoft.Xna.Framework;
using System.Collections.Generic;

namespace Crystal_of_Eternity
{
    public class DefaultRoom : Room
    {
        public DefaultRoom(LevelType levelType, Point size, Vector2 playerStartPosition,
            int enemiesCount, List<Enemy> enemiesTypes)
            : base(levelType, size, playerStartPosition, enemiesCount, enemiesTypes)
        {
        }
    }
}
