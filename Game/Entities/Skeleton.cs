using Microsoft.Xna.Framework;
using MonoGame.Extended;

namespace Crystal_of_Eternity
{
    public class Skeleton : Enemy
    {
        public Skeleton(Vector2 position, RectangleF mapBounds) :
            base("Skeleton", SpriteNames.Skeleton_1, SpriteNames.Skeleton_corpse, SoundNames.SkeletonHit, position, 30.0f, 0.5f, 1.5f, 0.2f, mapBounds)
        {

        }
    }
}
