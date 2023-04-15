using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended;
using MonoGame.Extended.Collisions;
using MonoGame.Extended.Sprites;
using System;
using System.Diagnostics;

namespace Crystal_of_Eternity
{
    public class Enemy : IEntity
    {
        #region Public

        public string Name { get; private set; }

        public Vector2 Position { get; private set; }

        public float CurrentHP { get; private set; }

        public Sprite Sprite { get; private set; }
        public IShapeF Bounds => Sprite.GetBoundingRectangle(Position + new Vector2(0, 16) * 0.5f, 0, new(0.6f, 0.6f));

        #endregion

        #region Private

        private Texture2D texture;

        private float currentHP;
        private float maxHP;

        private readonly float baseMoveSpeed = 100.0f;
        private float moveSpeed = 0.5f;
        private Vector2 velocity;

        private SpriteEffects flip = SpriteEffects.None;
        private WalkAnimation walkAnimation;

        private Vector2 minPosition;
        private Vector2 maxPosition;

        private bool canGetHit = true;
        private float iTimer;

        #endregion

        public Enemy(string name, Vector2 position, float maxHP, RectangleF mapBounds)
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

        public void LoadContent()
        {
            var content = MyGame.Instance.Content;
            texture = content.Load<Texture2D>(SpriteNames.Skeleton1);
            Sprite = new Sprite(texture);
        }

        public void Move(Vector2 direction, GameTime gameTime)
        {
            velocity = direction * moveSpeed * (float)gameTime.GetElapsedSeconds() * baseMoveSpeed;
            Position = Vector2.Clamp(Position + velocity, minPosition, maxPosition);
        }

        public void OnCollision(CollisionEventArgs collisionInfo)
        {
            if(canGetHit)
                TakeHit();
        }

        public void TakeHit()
        {
            Debug.Print("Get hit");
            canGetHit = false;
            iTimer = 0;
        }

        public void Update(GameTime gameTime)
        {
            if (iTimer >= 0)
            {
                iTimer += gameTime.GetElapsedSeconds();
                if(iTimer >= 0.2f)
                {
                    canGetHit = true;
                    iTimer = -1;
                }    
            }
        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            Sprite.Draw(spriteBatch, Position, 0, new(1, 1));
        }
    }
}
