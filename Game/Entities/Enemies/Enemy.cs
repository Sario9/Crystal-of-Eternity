using Microsoft.Xna.Framework;
using MonoGame.Extended;
using MonoGame.Extended.Collisions;
using MonoGame.Extended.Sprites;
using MonoGame.Extended.Timers;
using System;
using System.Diagnostics;

namespace Crystal_of_Eternity
{
    public class Enemy : MovableEntity
    {
        #region Fields
        private MovableEntity target;

        private CountdownTimer attackTimer;
        private float attackInterval;
        private bool canAttack => attackTimer.State == TimerState.Completed;
        #endregion

        public Enemy(string name, string spritePath, string corpsePath, string hitSoundPath, float maxHP,
            float moveSpeed, float damage, float attackInterval)
            : base(name, spritePath, corpsePath, hitSoundPath, maxHP, moveSpeed, damage, 0.15f)
        {
            this.moveSpeed = moveSpeed + (float)Randomizer.Random.NextDouble() * 0.05f;
            CorpseSpritePath = corpsePath;
            this.attackInterval = attackInterval;

            LoadContent();

            attackTimer = new CountdownTimer(attackInterval);
            attackTimer.Start();
        }

        public void Spawn(Vector2 position, RectangleF mapBounds, MovableEntity target)
        {
            base.Spawn(position, mapBounds);
            this.target = target;
        }

        public override void OnCollision(CollisionEventArgs collisionInfo)
        {
            if (!isSpawned) return;

            var other = collisionInfo.Other;
            if (other is Collider otherCollider)
            {
                if (otherCollider.Type == ColliderType.Obstacle)
                    Position -= collisionInfo.PenetrationVector * 1.1f +
                        collisionInfo.PenetrationVector.Rotate(-MathF.PI / 2) * 1.5f;
            }
            else if (other is Enemy otherEnemy)
            {
                if (otherEnemy.IsAlive)
                    Position -= collisionInfo.PenetrationVector * 0.1f;
            }
            else if (canGetHit && other is PlayerWeapon attack)
            {
                if (!attack.WasAttacked(this))
                {
                    attack.AddToAttacked(this);
                    TakeHit(attack.Damage);
                }
            }
            else if (canAttack && other.Equals(target) && target.IsAlive)
            {
                target.TakeHit(CollisionDamage);
                attackTimer.Restart();
            }
        }

        public override void TakeHit(float damage)
        {
            CurrentHP -= damage;
            iTimer.Restart();
            Debug.Print("{0} has {1}/{2} HP", Name, currentHP, maxHP);
            base.TakeHit(damage);
        }

        public override void Update(GameTime gameTime)
        {
            iTimer.Update(gameTime);
            attackTimer.Update(gameTime);

            base.Update(gameTime);

            if (target != null && target.IsAlive)
            {
                var directionToTarget = target.Position - Position;
                var distance = directionToTarget.Length();
                if (distance > 5)
                {
                    directionToTarget.Normalize();
                    Move(directionToTarget, gameTime);
                }
            }
        }

        public override object Clone() =>
            new Enemy(Name, spritePath, CorpseSpritePath, hitSoundPath, maxHP,
                moveSpeed, CollisionDamage, attackInterval);
    }
}
