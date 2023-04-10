using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended;
using MonoGame.Extended.Collisions;
using MonoGame.Extended.Sprites;
using MonoGame.Extended.Timers;
using System;
using System.ComponentModel;
using System.Diagnostics;

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
        public IShapeF Bounds => Sprite.GetBoundingRectangle(Position + new Vector2(0,16) * 0.5f, 0, new(0.6f, 0.6f));


        #endregion

        public Player(string name, Vector2 position, float maxHP, RectangleF mapBounds)
        {
            Name = name;
            Position = position;
            currentHP = maxHP;
            this.maxHP = maxHP;
            walkAnimation = new WalkAnimation(moveSpeed * 1.2f, 0.2f);
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
        private float maxSpeed = 1.0f;
        private Vector2 velocity;
        private bool isIdle => velocity == Vector2.Zero;    

        private SpriteEffects flip = SpriteEffects.None;
        private WalkAnimation walkAnimation;

        private Vector2 minPosition;
        private Vector2 maxPosition;

        #endregion  

        public void Move(Vector2 direction, GameTime gameTime)
        {
            velocity = direction * moveSpeed * (float)gameTime.GetElapsedSeconds() * baseMoveSpeed;
            Position = Vector2.Clamp(Position + velocity, minPosition, maxPosition);
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
            texture = content.Load<Texture2D>("Game/General/Entities/Player/Character-knight");
            Sprite = new Sprite(texture);
        }

        public void Update(GameTime gameTime)
        {
            Move(UserInput.MovePlayer(), gameTime);
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
            //spriteBatch.DrawRectangle((RectangleF)Bounds, Color.Blue, 3);

        }
    }
}
