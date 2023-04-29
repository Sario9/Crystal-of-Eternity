using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame.Extended;
using MonoGame.Extended.Collisions;
using System;
using System.Diagnostics;
using System.Linq;

namespace Crystal_of_Eternity
{
    public class Weapon : ICollisionActor
    {
        public float Damage { get; private set; }
        public bool CanAttack => !animation.IsPlaying;

        public IShapeF Bounds { get; private set; }

        private SpritesAnimation animation;
        private string[] animationPaths;
        private SpriteEffects attackFlip;

        private Vector2 position;
        private float attackRange;

        private CollisionComponent collisionComponent;

        public Weapon(float damage, float attackSpeed, float attackRange, string[] animationPaths, float size)
        {
            Damage = damage;
            this.animationPaths = animationPaths;
            this.attackRange = attackRange;

            animation = new(attackSpeed, Vector2.One * size);

            collisionComponent = MyGame.Instance.CurrentLevel.collisionComponent;

            LoadContent();
            Bounds = new CircleF(animation.CurrentSprite.Bounds.Location,
                animation.CurrentSprite.Bounds.Size.X * size * 0.4f);
        }

        private void LoadContent()
        {
            var content = MyGame.Instance.Content;
            var textures = animationPaths.Select(x => content.Load<Texture2D>(x)).ToArray();
            animation.AddMany(textures);
        }

        public void MakeAttack(Vector2 direction, Vector2 playerPosition, float mouseDistance)
        {
            attackFlip = attackFlip == SpriteEffects.None ? SpriteEffects.FlipHorizontally : SpriteEffects.None;
            direction.Normalize();
            position = playerPosition + direction * MathHelper.Clamp(mouseDistance, 15, attackRange);
            animation.SetRotation(Vector2Extensions.ToAngle(position - playerPosition));
            animation.Play();
            Bounds.Position = position;
            collisionComponent.Insert(this);
        }

        public void OnCollision(CollisionEventArgs collisionInfo)
        {
            
        }

        public void Update(GameTime gameTime)
        {
            animation.Update(gameTime);
            if(CanAttack)
                collisionComponent.Remove(this);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            animation.Draw(spriteBatch, position, attackFlip);
        }

        public void DrawBounds(SpriteBatch spriteBatch)
        {
            spriteBatch.DrawCircle((CircleF)Bounds, 12, Color.Yellow, 1);
        }
    }
}
