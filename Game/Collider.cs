using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended;
using MonoGame.Extended.Collisions;
using System;
using System.Diagnostics;

namespace Crystal_of_Eternity
{
    public class Collider : ICollisionActor
    {
        public IShapeF Bounds { get; private set; }
        public readonly ColliderType Type;

        public Collider(RectangleF bounds, ColliderType type)
        {
            Bounds = bounds;
            Type = type;
        }

        public void OnCollision(CollisionEventArgs collisionInfo)
        {
            
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.DrawRectangle((RectangleF)Bounds, Color.Red, 3);
        }
    }
}
