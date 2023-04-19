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
        public IShapeF Bounds => Sprite.GetBoundingRectangle(Position + new Vector2(0, 16) * 0.5f, 0, new(0.6f, 0.6f));


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

        private SpritesAnimation attackAnimation;
        private Collider attackCollider;
        private bool canAttack => !attackAnimation.IsPlaying;
        private SpriteEffects attackFlip = SpriteEffects.None;

        private CollisionComponent collisionComponent;

        private float iTimer;
        private bool canGetHit;

        #endregion

        public Player(string name, Vector2 position, float maxHP, RectangleF mapBounds)
        {
            Name = name;
            Position = position;
            currentHP = maxHP;
            this.maxHP = maxHP;
            walkAnimation = new WalkAnimation(moveSpeed * 1.2f, 0.2f);
            attackAnimation = new(0.01f, new(2, 2f));
            IsAlive = true;
            LoadContent();

            minPosition = Vector2.Zero;
            maxPosition = mapBounds.BottomRight;

            collisionComponent = MyGame.Instance.CurrentLevel.collisionComponent;
        }

        public void Move(Vector2 direction, GameTime gameTime)
        {
            velocity = direction * moveSpeed * (float)gameTime.GetElapsedSeconds() * baseMoveSpeed;
            Position = Vector2.Clamp(Position + velocity, minPosition, maxPosition);
        }

        public void Attack(Vector2 direction, float attackRange, Vector2 size)
        {
            attackFlip = attackFlip == SpriteEffects.None ? SpriteEffects.FlipHorizontally : SpriteEffects.None;
            direction.Normalize();
            var attackPosition = Position + direction * attackRange;
            attackAnimation.SetRotation(Vector2Extensions.ToAngle(attackPosition - Position));
            attackAnimation.Play();
            attackCollider = new(new(attackPosition - attackAnimation.CurrentSprite.Bounds.Size.ToVector2() / 2,
                size * attackAnimation.Scale), ColliderType.Attack);
            collisionComponent.Insert(attackCollider);
        }

        public void TakeHit()
        {

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
            if (UserInput.IsLMBPressed() && canAttack)
            {
                var camera = MyGame.Instance.Camera.Main;
                var mouseWorldPosition = camera.ScreenToWorld(UserInput.GetMousePosition().ToVector2());
                Attack(mouseWorldPosition - Position, 35, new(8, 8));
            }
            attackAnimation.Update(gameTime);
            if (attackCollider != null && !attackAnimation.IsPlaying && collisionComponent.Contains(attackCollider))
                collisionComponent.Remove(attackCollider);

            if (iTimer >= 0)
            {
                iTimer += gameTime.GetElapsedSeconds();
                if (iTimer >= 0.2f)
                {
                    canGetHit = true;
                    iTimer = -1;
                }
            }
        }

        public void OnCollision(CollisionEventArgs collisionInfo)
        {
            var other = collisionInfo.Other;
            if (other is Collider)
                Position -= collisionInfo.PenetrationVector * 1.1f;
            if (canGetHit && other is Enemy)
            {
                var enemy = (Enemy)other;
                if (enemy.IsAlive)
                {
                    TakeHit();
                    Position -= collisionInfo.PenetrationVector * 1.1f;
                    Debug.Print(enemy.Name);
                }
            }
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

            if (attackCollider != null && attackAnimation.IsPlaying)
            {
                //attackCollider.Draw(spriteBatch);
                attackAnimation.Draw(spriteBatch, attackCollider.Bounds.Position + attackAnimation.CurrentSprite.Bounds.Size.ToVector2() / 2, attackFlip);
            }

            //spriteBatch.DrawRectangle((RectangleF)Bounds, Color.Blue, 3);

        }
    }
}
