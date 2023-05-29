namespace Crystal_of_Eternity
{
    public class Skeleton : Enemy
    {
        public Skeleton() :
            base("Skeleton", SpriteNames.Skeleton_1, SpriteNames.Skeleton_corpse, SoundNames.SkeletonHit, 30.0f, 0.3f, 7f, 1.5f)
        {

        }
    }
}
