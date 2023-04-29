using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
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
        private Player player;

        private CountdownTimer attackTimer;
        private bool canAttack => attackTimer.State == TimerState.Completed;

        public Enemy(string name, string spritePath, string corpsePath, Vector2 position, float maxHP,
            float moveSpeed, float damage, float attackInterval, RectangleF mapBounds)
            : base(name, spritePath, corpsePath, position, maxHP, moveSpeed, damage, 0.2f, mapBounds)
        {
            Name = name;
            Position = position;
            this.maxHP = maxHP;
            currentHP = maxHP;
            this.moveSpeed = moveSpeed + (float)Randomizer.Random.NextDouble() * 0.05f;
            CollisionDamage = damage;
            walkAnimation = new WalkAnimation(moveSpeed * 2f, 0.2f);
            player = MyGame.Instance.CurrentLevel.Player;
            CorpseSpritePath = corpsePath;
            this.spritePath = spritePath;
            LoadContent();

            minPosition = Vector2.Zero;
            maxPosition = mapBounds.BottomRight;
            Bounds = Sprite.GetBoundingRectangle(Position, 0, new(0.8f, 0.8f));
            attackTimer = new CountdownTimer(attackInterval);
            attackTimer.Start();
        }

        public override void OnCollision(CollisionEventArgs collisionInfo)
        {
            var other = collisionInfo.Other;
            if (other is Collider)
            {
                var otherCollider = (Collider)other;
                if (otherCollider.Type == ColliderType.Obstacle)
                    Position -= collisionInfo.PenetrationVector * 1.1f +
                        collisionInfo.PenetrationVector.Rotate(-MathF.PI / 2) * 1.5f;
            }
            else if (other is Enemy)
            {
                var otherEnemy = (Enemy)other;
                if(otherEnemy.IsAlive)
                    Position -= collisionInfo.PenetrationVector * 0.1f;
            }
            else if (canGetHit && other is Weapon)
            {
                var attack = (Weapon)other;
                TakeHit(attack.Damage);
            }
            else if (canAttack && other is Player)
            {
                player.TakeHit(CollisionDamage);
                attackTimer.Restart();
            }
        }

        public override void TakeHit(float damage)
        {
            CurrentHP -= damage;
            iTimer.Restart();
            Debug.Print("{0} has {1}/{2} HP", Name, currentHP, maxHP);
        }

        public override void Update(GameTime gameTime)
        {
            iTimer.Update(gameTime);
            attackTimer.Update(gameTime);
            if (player != null && player.IsAlive)
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
    }
}
