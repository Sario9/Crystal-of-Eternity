using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended.Collisions;
using MonoGame.Extended.Sprites;

namespace Crystal_of_Eternity
{
    public interface IEntity : ICollisionActor
    {
        public string Name { get; }

        public Vector2 Position { get; }

        public float CurrentHP { get; }

        public Sprite Sprite { get; }

        public void TakeHit();

        public void Update(GameTime gameTime);

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch);
    }
}
