using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
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
                    currentHP = value;
                    if (currentHP <= 0)
                    {
                        currentHP = 0;
                        Die();
                    }
                    if (currentHP > maxHP) currentHP = maxHP;
                }
            }
        }
        public float MaxHP { get { return maxHP; } }
        public bool IsAlive = true;

        public Sprite Sprite { get; protected set; }
        public string CorpseSpritePath { get; protected set; }
        public IShapeF Bounds { get; protected set; }

        public delegate void DeathHandler(MovableEntity entity);
        public DeathHandler OnDeath;

        protected string spritePath;
        protected string hitSoundPath;

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
        #endregion

        public MovableEntity(string name, string spritePath, string corpsePath, string hitSoundPath, Vector2 position, float maxHP,
            float moveSpeed, float damage, float iInterval, RectangleF mapBounds)
        {
            Name = name;
            Position = position;
            this.maxHP = maxHP;
            currentHP = maxHP;
            this.moveSpeed = moveSpeed;
            CollisionDamage = damage;
            iIntevral = iInterval;
            walkAnimation = new WalkAnimation(moveSpeed * 2f, 0.2f);
            CorpseSpritePath = corpsePath;
            this.spritePath = spritePath;
            this.hitSoundPath = hitSoundPath;
            LoadContent();

            minPosition = Vector2.Zero;
            maxPosition = mapBounds.BottomRight;
            Bounds = Sprite.GetBoundingRectangle(Position, 0, new(0.8f, 0.8f));

            iTimer = new CountdownTimer(iIntevral);
            iTimer.Start();
        }

        public virtual void LoadContent()
        {
            var content = MyGame.Instance.Content;
            Sprite = new Sprite(content.Load<Texture2D>(spritePath));
            if (hitSoundPath != "")
                hitSound = content.Load<SoundEffect>(hitSoundPath);
        }

        public virtual void Move(Vector2 direction, GameTime gameTime)
        {
            velocity = direction * moveSpeed * (float)gameTime.GetElapsedSeconds() * baseMoveSpeed;
            Position = Vector2.Clamp(Position + velocity, minPosition, maxPosition);
            Bounds.Position = Position - new Vector2(16, 16) * 0.8f;
        }

        public virtual void SetMapBounds(RectangleF bounds) => maxPosition = bounds.BottomRight;

        public virtual void OnCollision(CollisionEventArgs collisionInfo)
        {

        }

        protected virtual void Die()
        {
            IsAlive = false;
            OnDeath.Invoke(this);
        }

        public virtual void TakeHit(float damage)
        {
            hitSound?.Play();
        }

        public virtual void Update(GameTime gameTime)
        {

        }

        public virtual void Draw(GameTime gameTime, SpriteBatch spriteBatch)
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

        public virtual void DrawBounds(SpriteBatch spriteBatch)
        {
            spriteBatch.DrawRectangle((RectangleF)Bounds, Color.Red);
        }

        public virtual object Clone() => throw new DataException();
    }
}
