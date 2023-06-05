using Microsoft.Xna.Framework;
using MonoGame.Extended;
using MonoGame.Extended.Collisions;
using MonoGame.Extended.Timers;
using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace Crystal_of_Eternity
{
    public class Boss : MovableEntity
    {
        #region Fields

        protected Player player;

        protected CountdownTimer skillsCooldownTimer;
        protected float skillsInterval;
        protected List<Action> skills;

        public delegate void HitHandler(Boss boss);
        public event HitHandler OnHealthChanged;

        #endregion

        protected Boss(string name, string spritePath, string corpsePath, string hitSoundPath,
            float maxHP, float moveSpeed, float damage, float skillsInterval) :
            base(name, spritePath, corpsePath, hitSoundPath, maxHP, moveSpeed, damage, 0.15f)
        {
            skills = new();
            this.skillsInterval = skillsInterval;
            iTimer.Start();
            skillsCooldownTimer = new(skillsInterval);
        }

        public void Spawn(Vector2 position, RectangleF mapBounds, Player player)
        {
            base.Spawn(position, mapBounds);
            this.player = player;
            shadowScale = 2.0f;
        }

        public override void Update(GameTime gameTime)
        {
            iTimer.Update(gameTime);
            skillsCooldownTimer.Update(gameTime);
            base.Update(gameTime);

            if (player != null && player.IsAlive)
            {
                var direction = player.Position - Position;
                var distance = direction.Length();
                if (distance > 5.0f)
                {
                    direction.Normalize();
                    Move(direction, gameTime);
                    UseRandomSkill(skills);
                }
            }
        }

        public override void TakeHit(float damage)
        {
            CurrentHP -= damage;
            iTimer.Restart();
            Debug.Print("{0} has {1}/{2} HP", Name, currentHP, maxHP);
            base.TakeHit(damage);

            OnHealthChanged?.Invoke(this);
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
                    TakeHit(attack.DamageWithModifier);
                }
            }
        }

        protected virtual void UseRandomSkill(List<Action> skills)
        {
            if (skillsCooldownTimer.State == TimerState.Completed)
            {
                var skill = Randomizer.RandomFromList(skills);
                skill();
                skillsCooldownTimer.Restart();
            }
        }

        public override object Clone() =>
            new Boss(Name, spritePath, CorpseSpritePath, hitSoundPath, maxHP,
                moveSpeed, CollisionDamage, skillsInterval);
    }
}
