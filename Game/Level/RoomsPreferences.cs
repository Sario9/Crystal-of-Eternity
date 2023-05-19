using Microsoft.Xna.Framework;
using System;

namespace Crystal_of_Eternity
{
    public class RoomPreferences
    {
        public readonly LevelType Level;
        public readonly Point Size;
        public readonly Vector2 PlayerStartPosition;
        public readonly Func<MovableEntity>[] Enemies;
        public readonly int EnemiesCount;

        public RoomPreferences(LevelType level, Point size, Vector2 playerStartPosition, int enemiesCount, params Func<MovableEntity>[] enemies)
        {
            Level = level;
            Size = size;
            PlayerStartPosition = playerStartPosition;
            Enemies = enemies;
            EnemiesCount = enemiesCount;
        }
    }
}
