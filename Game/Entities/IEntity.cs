using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Crystal_of_Eternity
{
    public interface IEntity
    {
        public string Name { get; }

        public Vector2 Position { get; }

        public float CurrentHP { get; }

        public Texture2D Sprite { get; }

        public void TakeHit();
    }
}
