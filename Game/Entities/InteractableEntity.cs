using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended;
using MonoGame.Extended.Collisions;
using MonoGame.Extended.Sprites;
using System;
using System.Data;

namespace Crystal_of_Eternity
{
    public abstract class InteractableEntity : IEntity
    {
        #region Fields

        public Vector2 Position { get; set; }

        public IShapeF Bounds { get; private set; }

        protected string idleSpritePath;
        protected string activeSpritePath;

        protected Sprite idleSprite;
        protected Sprite activeSprite;

        protected bool canInteract;
        protected Action onInteract;

        private bool isActive = false;

        private ContentManager content;
        #endregion

        protected InteractableEntity(Vector2 position, string idleSpritePath, string activeSpritePath, ContentManager content)
        {
            Position = position;
            this.content = content;
            this.activeSpritePath = activeSpritePath;
            this.idleSpritePath = idleSpritePath;

            LoadContent();
        }

        public virtual void OnCollision(CollisionEventArgs collisionInfo)
        {

        }

        public virtual void Interact()
        {
            if(canInteract)
                onInteract?.Invoke();
        }

        protected virtual void LoadContent()
        {
            idleSprite = new Sprite(content.Load<Texture2D>(idleSpritePath));
            activeSprite = new Sprite(content.Load<Texture2D>(activeSpritePath));
        }

        public virtual void Update(GameTime gameTime)
        {

        }

        public virtual void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            if (!isActive)
                idleSprite.Draw(spriteBatch, Position, 0, Vector2.One);
            else
                activeSprite.Draw(spriteBatch, Position, 0, Vector2.One);
        }

        public void DrawBounds(SpriteBatch spriteBatch)
        {
            
        }

        public virtual object Clone() => throw new DataException();
    }
}
