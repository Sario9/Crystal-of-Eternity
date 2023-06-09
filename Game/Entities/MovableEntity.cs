﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended;
using MonoGame.Extended.Collisions;
using MonoGame.Extended.Sprites;
using MonoGame.Extended.Timers;
using System.Data;

namespace Crystal_of_Eternity
{
    public class MovableEntity : IEntity
    {
        #region Fields
        public string Name { get; protected set; }

        public Vector2 Position { get; set; }
        public float CollisionDamage { get; protected set; }

        public virtual float CurrentHP
        {
            get => currentHP;
            set
            {
                if(IsAlive)
                {
                    if (value > maxHP) currentHP = maxHP;
                    else if (value <= 0)
                    {
                        currentHP = 0;
                        Die();
                    }
                    else currentHP = value;
                }
            }
        }
        public virtual float MaxHP { get { return maxHP; } protected set { maxHP = value; } }
        public bool IsAlive = true;

        public Sprite Sprite { get; protected set; }
        public string CorpseSpritePath { get; protected set; }
        public IShapeF Bounds { get; protected set; }

        public delegate void DeathHandler(MovableEntity entity);
        public DeathHandler OnDeath;

        protected string spritePath;
        protected string hitSoundPath;
        protected Sprite shadow;
        protected float shadowScale = 1.0f;

        protected SoundEffect hitSound;

        protected float currentHP;
        protected float maxHP;

        protected readonly float baseMoveSpeed = 100.0f;
        protected float moveSpeed = 0.5f;
        protected Vector2 velocity;

        protected SpriteEffects flip = SpriteEffects.None;
        protected WalkAnimation walkAnimation;

        protected Vector2 minPosition;
        protected Vector2 maxPosition;

        protected bool canGetHit => iTimer.State == TimerState.Completed;

        private float iIntevral;
        protected CountdownTimer iTimer;

        protected bool isSpawned = false;

        protected ContentManager content;
        #endregion

        public MovableEntity(string name, string spritePath, string corpsePath, string hitSoundPath, float maxHP, float moveSpeed,
            float damage, float iInterval)
        {
            Name = name;
            this.maxHP = maxHP;
            currentHP = maxHP;
            this.moveSpeed = moveSpeed;
            CollisionDamage = damage;
            iIntevral = iInterval;
            walkAnimation = new WalkAnimation(moveSpeed * 2f, 0.15f);
            CorpseSpritePath = corpsePath;
            this.spritePath = spritePath;
            this.hitSoundPath = hitSoundPath;
            LoadContent();

            minPosition = Vector2.Zero;
            Bounds = Sprite.GetBoundingRectangle(Position, 0, new(0.8f, 0.8f));

            iTimer = new CountdownTimer(iIntevral);
            iTimer.Start();
        }

        public virtual void Spawn(Vector2 position, RectangleF mapBounds)
        {
            Position = position;
            SetMapBounds(mapBounds);

            isSpawned = true;
        }

        public virtual void LoadContent()
        {
            content = MyGame.Instance.Content;
            Sprite = new Sprite(content.Load<Texture2D>(spritePath));
            shadow = new Sprite(content.Load<Texture2D>(SpriteNames.Shadow));
            if (hitSoundPath != "")
                hitSound = content.Load<SoundEffect>(hitSoundPath);
        }

        public virtual void Move(Vector2 direction, GameTime gameTime)
        {
            velocity = direction * moveSpeed * (float)gameTime.GetElapsedSeconds() * baseMoveSpeed;
            Bounds.Position = Position - new Vector2(16, 16) * 0.8f;
        }

        public virtual void SetMapBounds(RectangleF bounds) => maxPosition = bounds.BottomRight;

        public virtual void OnCollision(CollisionEventArgs collisionInfo)
        {
            if (!isSpawned) return;

            var other = collisionInfo.Other;
            if (other is Collider || other is InteractableEntity)
                Position -= collisionInfo.PenetrationVector * 1.1f;
        }

        protected virtual void Die()
        {
            IsAlive = false;
            OnDeath?.Invoke(this);
        }

        public virtual void TakeHit(float damage)
        {
            hitSound?.Play();
        }

        public virtual void Update(GameTime gameTime)
        {
            if (!isSpawned) return;

            Position = Vector2.Clamp(Position + velocity, minPosition, maxPosition);
        }

        public virtual void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            if (!isSpawned) return;

            walkAnimation.Play(gameTime);

            if (velocity.X > 0)
                flip = SpriteEffects.FlipHorizontally;
            if (velocity.X < 0)
                flip = SpriteEffects.None;
            Sprite.Effect = flip;

            Sprite.Color = !canGetHit ? Color.Red : Color.White;

            shadow.Draw(spriteBatch, Position + new Vector2(0,12), 0, Vector2.One * shadowScale);
            Sprite.Draw(spriteBatch, Position, walkAnimation.SpriteRotation, new(1, 1));
        }

        public virtual void DrawBounds(SpriteBatch spriteBatch)
        {
            spriteBatch.DrawRectangle((RectangleF)Bounds, Color.Red);
        }

        public virtual object Clone() => throw new DataException();
    }
}
