using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended;
using MonoGame.Extended.Collisions;

namespace Crystal_of_Eternity
{
    public class Spear : PlayerWeapon
    {
        private Vector2 attackEndPoint;

        public Spear(CollisionComponent collisionComponent) :
            base
            (
                15.0f, 0.04f, 0.6f, 100.0f,
                new[]
                {
                    SpriteNames.SpearAttack_1,
                    SpriteNames.SpearAttack_2,
                    SpriteNames.SpearAttack_3,
                    SpriteNames.SpearAttack_4,
                    SpriteNames.SpearAttack_5,
                    SpriteNames.SpearAttack_6,
                    SpriteNames.SpearAttack_7,
                },
                new[] { SoundNames.Sword2 },
                1.25f, collisionComponent)
        {

        }

        public override void MakeAttack(Vector2 direction, Vector2 playerPosition, float mouseDistance)
        {
            Bounds.Position = playerPosition;

            if(CanAttack)
            {
                attackedEntities.Clear();
                attackFlip = attackFlip == SpriteEffects.None ? SpriteEffects.FlipHorizontally : SpriteEffects.None;
                direction.Normalize();
                position = playerPosition + direction;
                attackEndPoint = position + direction * MathHelper.Clamp(mouseDistance, 5, attackRange - 16);
                animation.SetRotation(Vector2Extensions.ToAngle(position - playerPosition));
                animation.Play();
                Randomizer.RandomFromList(attackSound).Play();
                CollisionComponent.Insert(this);
                attackTimer.Restart();
            }
        }

        public override void Update(GameTime gameTime)
        {
            if (!CanAttack)
                position = Vector2.Lerp(position, attackEndPoint, 0.04f);
            base.Update(gameTime);
        }

    }
}
