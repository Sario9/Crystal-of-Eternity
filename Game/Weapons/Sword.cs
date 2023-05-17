using MonoGame.Extended.Collisions;

namespace Crystal_of_Eternity
{
    public class Sword : PlayerWeapon
    {
        public Sword(float size, CollisionComponent collisionComponent) :
            base
            (
                20.0f, 0.03f, 0.75f, 35.0f,
                new[]
                {
                    SpriteNames.Attack_1,
                    SpriteNames.Attack_2,
                    SpriteNames.Attack_3,
                    SpriteNames.Attack_4,
                    SpriteNames.Attack_5,
                    SpriteNames.Attack_6
                },
                new[] { SoundNames.Sword1 },
                size, collisionComponent)
        {

        }

    }
}
