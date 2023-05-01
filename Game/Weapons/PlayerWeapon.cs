﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame.Extended;
using MonoGame.Extended.Collisions;
using MonoGame.Extended.Timers;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace Crystal_of_Eternity
{
    public class PlayerWeapon : ICollisionActor
    {
        public float Damage { get; private set; }
        public bool CanAttack => !animation.IsPlaying && attackTimer.State == TimerState.Completed;

        public IShapeF Bounds { get; private set; }

        private SpritesAnimation animation;
        private string[] animationPaths;
        private SpriteEffects attackFlip;

        private Vector2 position;
        private float attackRange;

        private CollisionComponent collisionComponent;
        private CountdownTimer attackTimer;
        private List<IEntity> attackedEntities;

        public PlayerWeapon(float damage, float animationSpeed, float attackInterval, float attackRange, string[] animationPaths, float size)
        {
            Damage = damage;
            this.animationPaths = animationPaths;
            this.attackRange = attackRange;

            attackedEntities = new List<IEntity>();

            animation = new(animationSpeed, Vector2.One * size);

            collisionComponent = MyGame.Instance.CurrentLevel.collisionComponent;

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
        }

        public void MakeAttack(Vector2 direction, Vector2 playerPosition, float mouseDistance)
        {
            if(CanAttack)
            {
                attackedEntities.Clear();
                attackFlip = attackFlip == SpriteEffects.None ? SpriteEffects.FlipHorizontally : SpriteEffects.None;
                direction.Normalize();
                position = playerPosition + direction * MathHelper.Clamp(mouseDistance, 15, attackRange);
                animation.SetRotation(Vector2Extensions.ToAngle(position - playerPosition));
                animation.Play();
                Bounds.Position = position;
                collisionComponent.Insert(this);
                attackTimer.Restart();
            }
        }

        public void OnCollision(CollisionEventArgs collisionInfo)
        {
            
        }

        public void AddToAttacked(IEntity entity) => attackedEntities.Add(entity);

        public bool WasAttacked(IEntity entity) => attackedEntities.Contains(entity);

        public void Update(GameTime gameTime)
        {
            animation.Update(gameTime);
            attackTimer.Update(gameTime);
            if(CanAttack)
                collisionComponent.Remove(this);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            animation.Draw(spriteBatch, position, attackFlip);
        }

        public void DrawBounds(SpriteBatch spriteBatch)
        {
            spriteBatch.DrawCircle((CircleF)Bounds, 12, Color.Yellow, 1);
        }
    }
}