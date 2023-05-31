using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended;
using MonoGame.Extended.Collisions;
using System;

namespace Crystal_of_Eternity
{
    public abstract class DropableEntity : IEntity
    {
        #region Fields

        public Vector2 Position { get; set; }
        public IShapeF Bounds { get; set; }

        public delegate void InteractHandler(DropableEntity entity);
        public InteractHandler OnInteract;

        protected SpritesAnimation animation;
        protected string[] spritesPath;

        protected Player player;

        #endregion

        public DropableEntity(Vector2 position, Player player, string[] spritesPath)
        {
            Position = position;
            Bounds = new RectangleF(position, new(2, 2));
            this.spritesPath = spritesPath;
            this.player = player;

            LoadContent();
        }

        protected virtual void LoadContent()
        {
            var content = MyGame.Instance.Content;
            animation = new(0.1f, Vector2.One, true, true);
            foreach(var path in spritesPath) 
                animation.Add(content.Load<Texture2D>(path));
        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            animation.Draw(spriteBatch, Position, SpriteEffects.None);
        }

        public void DrawBounds(SpriteBatch spriteBatch)
        {
            spriteBatch.DrawRectangle((RectangleF)Bounds, Color.Gold, 1);
        }

        public virtual void Interact(Invenory invenory)
        {
            OnInteract.Invoke(this);
        }

        protected virtual void MoveToPlayer()
        {
            if(player != null && player.IsAlive)
            {
                var distance = Vector2.Distance(Position, player.Position);
                if(distance < 64)
                {
                    Position = Vector2.Lerp(Position, player.Position, 0.06f);
                    Bounds.Position = Position;
                }
            }
        }

        public virtual void OnCollision(CollisionEventArgs collisionInfo)
        {
            
        }

        public virtual void Update(GameTime gameTime)
        {
            animation.Update(gameTime);
            MoveToPlayer();
        }

        public virtual object Clone() => throw new ArgumentNullException();
    }
}
