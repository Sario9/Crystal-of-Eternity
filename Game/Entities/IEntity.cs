using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended.Collisions;
using System;

namespace Crystal_of_Eternity
{
    public interface IEntity : ICollisionActor, ICloneable
    {
        public Vector2 Position { get; set; }

        public void Update(GameTime gameTime);

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch);

        public void DrawBounds(SpriteBatch spriteBatch);
    }
}
