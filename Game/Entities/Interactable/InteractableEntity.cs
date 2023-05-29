using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended;
using MonoGame.Extended.Collisions;
using MonoGame.Extended.Sprites;
using System.Data;

namespace Crystal_of_Eternity
{
    public abstract class InteractableEntity : IEntity
    {
        #region Fields

        public Vector2 Position { get; set; }

        public IShapeF Bounds { get; protected set; }

        public bool isUsed = false;

        protected string idleSpritePath;
        protected string activeSpritePath;

        protected Sprite idleSprite;
        protected Sprite activeSprite;

        protected bool canInteract = false;
        protected bool isActive = false;
        #endregion

        protected InteractableEntity(Vector2 position, string idleSpritePath, string activeSpritePath)
        {
            Position = position;
            this.activeSpritePath = activeSpritePath;
            this.idleSpritePath = idleSpritePath;

            LoadContent();
        }

        public virtual void OnCollision(CollisionEventArgs collisionInfo)
        {

        }

        public virtual void Interact(GameUI ui)
        {

        }

        public void SetActive() => canInteract = true;
        public void SetNotActive() => canInteract = false;

        protected virtual void LoadContent()
        {
            var content = MyGame.Instance.Content;

            idleSprite = new Sprite(content.Load<Texture2D>(idleSpritePath));
            activeSprite = new Sprite(content.Load<Texture2D>(activeSpritePath));

            Bounds = idleSprite.GetBoundingRectangle(Position, 0, Vector2.One);
        }

        public virtual void Update(GameTime gameTime)
        {
            
        }

        public virtual void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            var currentSprite = isActive ? activeSprite : idleSprite;
            currentSprite.Draw(spriteBatch, Position, 0, Vector2.One);
        }

        public void DrawBounds(SpriteBatch spriteBatch)
        {
            spriteBatch.DrawRectangle((RectangleF)Bounds, Color.Blue);
        }

        public virtual object Clone() => throw new DataException();
    }
}
