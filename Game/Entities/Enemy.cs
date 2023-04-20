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
        public float Damage { get; private set; }

        public float CurrentHP
        {
            get { return currentHP; }
            set { currentHP = value; if (currentHP <= 0) Die(); }
        }
        public bool IsAlive => currentHP > 0;

        public Sprite Sprite { get; private set; }
        public string CorpseSpritePath { get; private set; }
        public IShapeF Bounds { get; private set; }

        #endregion

        #region Private

        private Texture2D texture;
        private string spritePath;

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
        private bool canAttack = true;
        private float iTimer = -1;
        private float aTimer = -1;

        private Player player;

        #endregion

        public Enemy(string name, string spritePath, string corpsePath, Vector2 position, float maxHP, float moveSpeed, float damage, RectangleF mapBounds)
        {
            Name = name;
            Position = position;
            this.maxHP = maxHP;
            currentHP = maxHP;
            this.moveSpeed = moveSpeed + (float)Randomizer.Random.NextDouble() * 0.05f;
            Damage = damage;
            walkAnimation = new WalkAnimation(moveSpeed * 2f, 0.2f);
            player = MyGame.Instance.CurrentLevel.Player;
            CorpseSpritePath = corpsePath;
            this.spritePath = spritePath;
            LoadContent();

            minPosition = Vector2.Zero;
            maxPosition = mapBounds.BottomRight;
            Bounds = Sprite.GetBoundingRectangle(Position, 0, new(0.8f, 0.8f));
        }

        public void LoadContent()
        {
            var content = MyGame.Instance.Content;
            texture = content.Load<Texture2D>(spritePath);
            Sprite = new Sprite(texture);
        }

        public void Move(Vector2 direction, GameTime gameTime)
        {
            velocity = direction * moveSpeed * (float)gameTime.GetElapsedSeconds() * baseMoveSpeed;
            Position = Vector2.Clamp(Position + velocity, minPosition, maxPosition);
            Bounds.Position = Position - new Vector2(16, 16) * 0.8f;
        }

        public void OnCollision(CollisionEventArgs collisionInfo)
        {
            var other = collisionInfo.Other;
            if (other is Collider)
            {
                var otherCollider = (Collider)other;
                if (otherCollider.Type == ColliderType.Attack && canGetHit)
                    TakeHit(otherCollider.CollideDamge);
                else if (otherCollider.Type == ColliderType.Obstacle)
                    Position -= collisionInfo.PenetrationVector * 1.1f +
                        collisionInfo.PenetrationVector.Rotate(-MathF.PI / 2) * 1.5f;
            }
            else if (other is Enemy)
            {
                Position -= collisionInfo.PenetrationVector * .1f;
            }
            else if (canAttack && other is Player)
            {
                player.TakeHit(Damage);
                canAttack = false;
                aTimer = 0;
            }
        }

        private void Die()
        {
            var level = MyGame.Instance.CurrentLevel;
            level.KillEntity(this);
        }

        public void TakeHit(float damage)
        {
            CurrentHP -= damage;
            canGetHit = false;
            iTimer = 0;
            //Debug.Print("{0} has {1}/{2} HP", Name, currentHP, maxHP);
        }

        public void Update(GameTime gameTime)
        {
            if (iTimer >= 0)
            {
                iTimer += gameTime.GetElapsedSeconds();
                if (iTimer >= 0.2f)
                {
                    canGetHit = true;
                    iTimer = -1;
                }
            }

            if(aTimer >= 0)
            {
                aTimer += gameTime.GetElapsedSeconds();
                if(aTimer >= 0.2f)
                {
                    canAttack = true;
                    aTimer = -1;
                }
            }

            if (player != null)
            {
                var directionToPlayer = player.Position - Position;
                var distance = directionToPlayer.Length();
                if (distance > 1)
                {
                    directionToPlayer.Normalize();
                    Move(directionToPlayer, gameTime);
                }
            }
        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            walkAnimation.Play(gameTime);

            if (velocity.X > 0)
                flip = SpriteEffects.FlipHorizontally;
            if (velocity.X < 0)
                flip = SpriteEffects.None;
            Sprite.Effect = flip;

            if (!canGetHit)
                Sprite.Color = Color.Red;
            else
                Sprite.Color = Color.White;

            Sprite.Draw(spriteBatch, Position, walkAnimation.SpriteRotation, new(1, 1));
        }

        public void DrawBounds(SpriteBatch spriteBatch) 
        {
            spriteBatch.DrawRectangle((RectangleF)Bounds, Color.Red);
        }
    }
}
