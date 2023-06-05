using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended;
using MonoGame.Extended.Collisions;
using MonoGame.Extended.Timers;
using System.Collections.Generic;
using System.Linq;

namespace Crystal_of_Eternity
{
    public class PlayerWeapon : ICollisionActor
    {
        #region Fields
        public float CutThroughDamage => DamageWithModifier - AttackedCount * damage * cutThroughModifier;
        public float DamageWithModifier => damage + AdditionalDamage;
        public float AttackIntervalWithModifier => attackInterval / AdditionalAttackSpeedPercent;
        public float WeaponScaleWithModifier => attackSize * AdditionalSizePercent;
        public bool CanAttack => !animation.IsPlaying && attackTimer.State == TimerState.Completed;
        public CollisionComponent CollisionComponent { get; set; }

        public IShapeF Bounds { get; private set; }

        protected float cutThroughModifier = 0.1f;
        protected float damage; 
        protected SpritesAnimation animation;
        protected string[] animationPaths;
        protected SpriteEffects attackFlip;
        protected float attackSize;

        protected Vector2 position;
        protected float attackRange;

        protected CountdownTimer attackTimer;
        protected float attackInterval;
        protected List<IEntity> attackedEntities;

        protected string[] soundPaths;
        protected List<SoundEffect> attackSound;

        //Модификаторы
        public float AdditionalDamage { get; set; } = 0.0f;
        public float AdditionalAttackSpeedPercent { get; set; } = 1.0f;
        public float AdditionalSizePercent { get; set; } = 1.0f; 
        #endregion

        public PlayerWeapon(float damage, float animationSpeed, float attackInterval, float attackRange, string[] animationPaths, string[] soundPaths,
            float size, CollisionComponent collisionComponent)
        {
            this.damage = damage;
            this.animationPaths = animationPaths;
            this.attackRange = attackRange;
            attackSize = size;

            this.soundPaths = soundPaths;
            attackSound = new List<SoundEffect>();

            attackedEntities = new List<IEntity>();

            animation = new(animationSpeed, Vector2.One * size * AdditionalSizePercent);

            this.CollisionComponent = collisionComponent;

            this.attackInterval = attackInterval;
            attackTimer = new CountdownTimer(attackInterval);

            LoadContent();
            Bounds = new CircleF(animation.CurrentSprite.Bounds.Location,
                animation.CurrentSprite.Bounds.Size.X * size * 0.4f);
        }

        private void LoadContent()
        {
            var content = MyGame.Instance.Content;
            var textures = animationPaths.Select(x => content.Load<Texture2D>(x)).ToArray();
            animation.AddMany(textures);
            foreach (var sound in soundPaths)
                attackSound.Add(content.Load<SoundEffect>(sound));
        }

        public virtual void MakeAttack(Vector2 direction, Vector2 playerPosition, float mouseDistance)
        {
            Bounds.Position = playerPosition;

            if (CanAttack)
            {
                attackedEntities.Clear();
                attackFlip = attackFlip == SpriteEffects.None ? SpriteEffects.FlipHorizontally : SpriteEffects.None;
                direction.Normalize();
                position = playerPosition + direction * 32;
                animation.SetRotation(Vector2Extensions.ToAngle(position - playerPosition));
                animation.Play();
                Randomizer.RandomFromList(attackSound).Play();
                CollisionComponent.Insert(this);
                attackTimer.Restart();
            }
        }

        public void OnCollision(CollisionEventArgs collisionInfo)
        {

        }

        public void AddToAttacked(IEntity entity) => attackedEntities.Add(entity);

        public bool WasAttacked(IEntity entity) => attackedEntities.Contains(entity);

        public void UpdateModifiers()
        {
            attackTimer = new(attackInterval / AdditionalAttackSpeedPercent);
            animation.Scale = Vector2.One * attackSize * AdditionalSizePercent;
        }

        public int AttackedCount => attackedEntities.Count;

        public virtual void Update(GameTime gameTime)
        {
            animation.Update(gameTime);
            attackTimer.Update(gameTime);
            if (!animation.IsPlaying)
                CollisionComponent.Remove(this);
            else
                Bounds.Position = position;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if(animation.IsPlaying)
                animation.Draw(spriteBatch, position, attackFlip);
        }

        public void DrawBounds(SpriteBatch spriteBatch)
        {
            if (animation.IsPlaying)
                spriteBatch.DrawCircle((CircleF)Bounds, 12, Color.Yellow, 1);
        }
    }
}
