using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended;
using MonoGame.Extended.Collisions;
using MonoGame.Extended.Sprites;
using MonoGame.Extended.TextureAtlases;
using System.Diagnostics;

namespace Crystal_of_Eternity
{
    public class Player : MovableEntity
    {
        public PlayerWeapon PlayerAttack { get; private set; }
        private bool isIdle => velocity == Vector2.Zero;

        public override float CurrentHP
        {
            get => currentHP;
            set 
            { 
                base.CurrentHP = value;
                onHealthChanged?.Invoke(value, maxHP);
            }
        }

        public delegate void HitHandler(float currentHealth, float maxHealth);
        public event HitHandler onHealthChanged;

        public Player(float maxHP, float moveSpeed, float damage) :
            base("Player", SpriteNames.Character_knight, SpriteNames.Rogue_corpse, "", new(0, 0), maxHP,
                moveSpeed, damage, 0.05f, new(0, 0, 500, 500))
        {
            UserInput.OnLMBPressed += Attack;
            UserInput.OnMove += Move;
        }

        public void Initialize(Vector2 position, RectangleF mapBounds, CollisionComponent collisionComponent)
        {
            Position = position;
            maxPosition = mapBounds.BottomRight;
            PlayerAttack = new Spear(2f, collisionComponent);
            TakeHit(0);
        }

        public override void TakeHit(float damage)
        {
            CurrentHP -= damage;
            Debug.Print("{0}/{1}", CurrentHP, maxHP);
        }

        private void Attack()
        {
            if (PlayerAttack.CanAttack)
            {
                var camera = MyGame.Instance.Camera.Main;
                var mouseWorldPosition = camera.ScreenToWorld(UserInput.GetMousePosition().ToVector2());
                var distance = (mouseWorldPosition - Position).Length();
                PlayerAttack.MakeAttack(mouseWorldPosition - Position, Position, distance);
            }
        }

        protected override void Die()
        {
            OnDeath.Invoke(this);

            UserInput.OnLMBPressed -= Attack;
            UserInput.OnMove -= Move;
        }

        public void Restart()
        {
            CurrentHP = maxHP;
        }

        public override void Update(GameTime gameTime)
        {
            PlayerAttack.Update(gameTime);
            iTimer.Update(gameTime);
        }

        public override void OnCollision(CollisionEventArgs collisionInfo)
        {
            var other = collisionInfo.Other;
            if (other is Collider)
                Position -= collisionInfo.PenetrationVector * 1.1f;
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            if (!isIdle)
                walkAnimation?.Play(gameTime);
            else
                walkAnimation?.Reset();

            if (velocity.X > 0)
                flip = SpriteEffects.FlipHorizontally;
            if (velocity.X < 0)
                flip = SpriteEffects.None;
            Sprite.Effect = flip;
            Sprite.Draw(spriteBatch, Position, walkAnimation.SpriteRotation, new(1, 1));
            PlayerAttack.Draw(spriteBatch);
        }

        public override void DrawBounds(SpriteBatch spriteBatch)
        {
            spriteBatch.DrawRectangle((RectangleF)Bounds, Color.Blue, 3);
            PlayerAttack.DrawBounds(spriteBatch);
        }
    }
}
