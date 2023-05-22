using Microsoft.Xna.Framework;

namespace Crystal_of_Eternity
{
    public class DefaultRoom : Room
    {
        public DefaultRoom(LevelType levelType, Point size, Vector2 playerStartPosition)
            : base(levelType, RoomType.Default, size, playerStartPosition)
        {
        }
    }
}
