using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended.Sprites;
using MonoGame.Extended.Timers;
using System;
using System.ComponentModel;

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
                if (currentHP <= 0)
                    Die();
            }
        }

        public Sprite Sprite { get; private set; }

        #endregion

        public Player(string name, Vector2 position, float currentHP)
        {
            Name = name;
            Position = position;
            CurrentHP = currentHP;
            walkAnimation = new WalkAnimation(moveSpeed * 1.2f, 0.2f);
            LoadContent();
        }

        #region Private

        private Texture2D texture;
        private float currentHP;
        private readonly float baseMoveSpeed = 100.0f;
        private float moveSpeed = 1.0f;
        private Vector2 velocity;
        private SpriteEffects flip = SpriteEffects.None;
        private WalkAnimation walkAnimation;
        private bool isIdle => velocity == Vector2.Zero;

        #endregion

        public void Move(Vector2 direction, GameTime gameTime)
        {
            velocity = direction * moveSpeed
                * (float)gameTime.ElapsedGameTime.TotalSeconds * baseMoveSpeed;
            Position += velocity;
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
        }
    }
}
