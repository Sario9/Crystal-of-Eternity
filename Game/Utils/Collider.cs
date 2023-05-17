using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended;
using MonoGame.Extended.Collisions;

namespace Crystal_of_Eternity
{
    public class Collider : ICollisionActor
    {
        #region Fields
        public IShapeF Bounds { get; private set; }
        public float CollideDamge { get; private set; }
        public readonly ColliderType Type; 
        #endregion

        public Collider(RectangleF bounds, ColliderType type, float damage)
        {
            Bounds = bounds;
            Type = type;
            CollideDamge = damage;
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
