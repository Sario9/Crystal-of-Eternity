using Microsoft.Xna.Framework;

namespace Crystal_of_Eternity
{
    public class Level
    {
        public TileMap Map { get; private set; }

        public Vector2 PlayerStartPosition { get; private set; }

        public Level(LevelType levelType, Vector2 playerStartPosition)
        {
            Map = new TileMap(levelType, 16, 16);
            PlayerStartPosition = playerStartPosition;
        }
    }
}
