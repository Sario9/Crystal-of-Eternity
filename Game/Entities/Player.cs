using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended;
using MonoGame.Extended.Collisions;
using MonoGame.Extended.Sprites;
using MonoGame.Extended.TextureAtlases;
using System;
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
        public bool IsAlive { get; private set; }

        public Sprite Sprite { get; private set; }
        public string CorpseSpritePath { get; private set; }
        public IShapeF Bounds { get; private set; }


        #endregion

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

        private Attack attack;

        #endregion

        public Player(string name, Vector2 position, float maxHP, RectangleF mapBounds)
        {
            Name = name;
            Position = position;
            currentHP = maxHP;
            this.maxHP = maxHP;
            walkAnimation = new WalkAnimation(moveSpeed * 1.2f, 0.2f);
            attack = new(10.0f, 0.05f,
            new[]
            {
                SpriteNames.Attack_1,
                SpriteNames.Attack_2,
                SpriteNames.Attack_3,
                SpriteNames.Attack_4,
                SpriteNames.Attack_5,
                SpriteNames.Attack_6
            },
            new(2, 2));
            IsAlive = true;
            LoadContent();

            minPosition = Vector2.Zero;
            maxPosition = mapBounds.BottomRight;

            Bounds = Sprite.GetBoundingRectangle(Position, 0, new(0.8f, 0.8f));
        }

        public void Move(Vector2 direction, GameTime gameTime)
        {
            velocity = direction * moveSpeed * (float)gameTime.GetElapsedSeconds() * baseMoveSpeed;
            Position = Vector2.Clamp(Position + velocity, minPosition, maxPosition);
            Bounds.Position = Position - new Vector2(16, 16) * 0.8f;
        }

        public void TakeHit(float damage)
        {
            //CurrentHP -= damage;
            Debug.Print("{0}/{1}", CurrentHP, maxHP);
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
        }

        public void Update(GameTime gameTime)
        {
            Move(UserInput.MovePlayer(), gameTime);
            attack.Update(gameTime);

            if (UserInput.IsLMBPressed() && attack.CanAttack)
            {
                var camera = MyGame.Instance.Camera.Main;
                var mouseWorldPosition = camera.ScreenToWorld(UserInput.GetMousePosition().ToVector2());
                attack.MakeAttack(mouseWorldPosition - Position, Position, 35);
            }
        }

        public void OnCollision(CollisionEventArgs collisionInfo)
        {
            var other = collisionInfo.Other;
            if (other is Collider)
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
            attack.Draw(spriteBatch);
        }

        public void DrawBounds(SpriteBatch spriteBatch)
        {
            spriteBatch.DrawRectangle((RectangleF)Bounds, Color.Blue, 3);
            attack.DrawBounds(spriteBatch);
        }
    }
}
