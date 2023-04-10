using Microsoft.Xna.Framework;
using MonoGame.Extended.Sprites;

namespace Crystal_of_Eternity
{
    public interface IEntity
    {
        public string Name { get; }

        public Vector2 Position { get; }

        public float CurrentHP { get; }

        public Sprite Sprite { get; }

        public void TakeHit();
    }
}
