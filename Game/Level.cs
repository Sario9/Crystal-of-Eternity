using Microsoft.Xna.Framework;

namespace Crystal_of_Eternity
{
    public class Level
    {
        public TileMap Map { get; private set; }

        private Vector2 playerStartPosition;

        public Level(TileMap map, Vector2 playerStartPosition)
        {
            Map = map;
            this.playerStartPosition = playerStartPosition;
        }
    }
}
