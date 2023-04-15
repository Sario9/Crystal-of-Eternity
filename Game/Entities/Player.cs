using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame.Extended;
using MonoGame.Extended.Collisions;
using MonoGame.Extended.Sprites;
using MonoGame.Extended.TextureAtlases;
using System;

namespace Crystal_of_Eternity
{
    public class Player : IEntity
    {
        #region Public

        public string Name { get; }

        public Vector2 Position { get; private set; }

        public float CurrentHP
        {
            get => currentHP;
            private set
            {
                currentHP = value;
                if (currentHP > maxHP)
                    currentHP = maxHP;
                if (currentHP <= 0)
                    Die();
            }
        }

        public Sprite Sprite { get; private set; }
        public IShapeF Bounds => Sprite.GetBoundingRectangle(Position + new Vector2(0, 16) * 0.5f, 0, new(0.6f, 0.6f));
        

        #endregion

        public Player(string name, Vector2 position, float maxHP, RectangleF mapBounds)
        {
            Name = name;
            Position = position;
            currentHP = maxHP;
            this.maxHP = maxHP;
            walkAnimation = new WalkAnimation(moveSpeed * 1.2f, 0.2f);
            attackAnimation = new(0.02f, new(2,2f));
            LoadContent();

            minPosition = Vector2.Zero;
            maxPosition = mapBounds.BottomRight;
        }

        #region Private

        private Texture2D texture;

        private float currentHP;
        private float maxHP;

        private readonly float baseMoveSpeed = 100.0f;
        private float moveSpeed = 1.0f;
        private Vector2 velocity;
        private bool isIdle => velocity == Vector2.Zero;

        private SpriteEffects flip = SpriteEffects.None;
        private WalkAnimation walkAnimation;

        private Vector2 minPosition;
        private Vector2 maxPosition;

        private SpritesAnimation attackAnimation;
        private Obstacle attackCollider;

        #endregion  

        public void Move(Vector2 direction, GameTime gameTime)
        {
            velocity = direction * moveSpeed * (float)gameTime.GetElapsedSeconds() * baseMoveSpeed;
            Position = Vector2.Clamp(Position + velocity, minPosition, maxPosition);
        }

        public void Attack(Vector2 direction, float attackRange, Vector2 size)
        {
            direction.Normalize();
            var attackPosition = Position + direction * attackRange;
            attackAnimation.SetRotation(Vector2Extensions.ToAngle(attackPosition - Position));
            attackCollider = new(new(attackPosition, size * attackAnimation.Scale));
            attackAnimation.Play();
        }

        public void TakeHit()
        {
            throw new NotImplementedException();
        }

        private void Die()
        {
            throw new NotImplementedException();
        }

        public void LoadContent()
        {
            var content = MyGame.Instance.Content;
            texture = content.Load<Texture2D>(SpriteNames.Character_knight);
            Sprite = new Sprite(texture);
            attackAnimation.AddMany
            (
                content.Load<Texture2D>(SpriteNames.Attack_1),
                content.Load<Texture2D>(SpriteNames.Attack_2),
                content.Load<Texture2D>(SpriteNames.Attack_3),
                content.Load<Texture2D>(SpriteNames.Attack_4),
                content.Load<Texture2D>(SpriteNames.Attack_5),
                content.Load<Texture2D>(SpriteNames.Attack_6)
            );
        }

        public void Update(GameTime gameTime)
        {
            Move(UserInput.MovePlayer(), gameTime);
            if(UserInput.IsLMBPressed())
            {
                var camera = MyGame.Instance.Camera.Main;
                var mouseWorldPosition = camera.ScreenToWorld(UserInput.GetMousePosition().ToVector2());
                Attack(mouseWorldPosition - Position, 35, new(16, 8));
            }
            attackAnimation.Update(gameTime);
        }

        public void OnCollision(CollisionEventArgs collisionInfo)
        {
            Position -= collisionInfo.PenetrationVector * 1.1f;
        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            if (!isIdle)
                walkAnimation.Play(gameTime);
            else
                walkAnimation.Reset();

            if (velocity.X > 0)
                flip = SpriteEffects.FlipHorizontally;
            if (velocity.X < 0)
                flip = SpriteEffects.None;
            Sprite.Effect = flip;
            Sprite.Draw(spriteBatch, Position, walkAnimation.SpriteRotation, new(1, 1));
            if(attackCollider != null)
            {
                //attackCollider.Draw(spriteBatch);
                attackAnimation.Draw(spriteBatch, attackCollider.Bounds.Position);
            }

            //spriteBatch.DrawRectangle((RectangleF)Bounds, Color.Blue, 3);

        }
    }
}
