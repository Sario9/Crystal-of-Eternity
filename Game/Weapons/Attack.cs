using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended.Collisions;
using MonoGame.Extended;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace Crystal_of_Eternity
{
    public class Attack : ICollisionActor
    {
        public float Damage { get; private set; }
        public bool CanAttack => !animation.IsPlaying;

        public IShapeF Bounds { get; private set; }

        private SpritesAnimation animation;
        private string[] animationPaths;
        private SpriteEffects attackFlip;

        private float attackSpeed;
        private Vector2 size;
        private Vector2 position;

        private CollisionComponent collisionComponent;

        public Attack(float damage, float attackSpeed, string[] animationPaths, Vector2 size)
        {
            Damage = damage;
            this.size = size;
            this.attackSpeed = attackSpeed;
            this.animationPaths = animationPaths;

            animation = new(attackSpeed, size);

            collisionComponent = MyGame.Instance.CurrentLevel.collisionComponent;

            LoadContent();
            Bounds = new RectangleF(animation.CurrentSprite.Bounds.Location, animation.CurrentSprite.Bounds.Size);
        }

        private void LoadContent()
        {
            var content = MyGame.Instance.Content;
            var textures = animationPaths.Select(x => content.Load<Texture2D>(x)).ToArray();
            animation.AddMany(textures);
        }

        public void MakeAttack(Vector2 direction, Vector2 playerPosition, float attackRange)
        {
            attackFlip = attackFlip == SpriteEffects.None ? SpriteEffects.FlipHorizontally : SpriteEffects.None;
            direction.Normalize();
            position = playerPosition + direction * attackRange;
            animation.SetRotation(Vector2Extensions.ToAngle(position - playerPosition));
            animation.Play();
            Bounds.Position = position;
        }

        public void OnCollision(CollisionEventArgs collisionInfo)
        {
            Debug.Print("Attack!");
        }

        public void Update(GameTime gameTime)
        {
            animation.Update(gameTime);
            collisionComponent.Remove(this);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            animation.Draw(spriteBatch, position, attackFlip);
        }

        public void DrawBounds(SpriteBatch spriteBatch) 
        {
            spriteBatch.DrawRectangle((RectangleF)Bounds, Color.Yellow, 1);
        }
    }
}
