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

        public IShapeF Bounds { get; private set; }

        protected string idleSpritePath;
        protected string activeSpritePath;

        protected Sprite idleSprite;
        protected Sprite activeSprite;

        protected float interactDistance;
        protected bool canInteract;

        protected Player player;

        protected bool isActive = false;
        #endregion

        protected InteractableEntity(Vector2 position, string idleSpritePath, string activeSpritePath, Player player, float interactDistance)
        {
            Position = position;
            this.activeSpritePath = activeSpritePath;
            this.idleSpritePath = idleSpritePath;
            this.player = player;
            this.interactDistance = interactDistance;

            LoadContent();
        }

        public virtual void OnCollision(CollisionEventArgs collisionInfo)
        {

        }

        public virtual void Interact()
        {
            
        }

        protected virtual void LoadContent()
        {
            var content = MyGame.Instance.Content;

            idleSprite = new Sprite(content.Load<Texture2D>(idleSpritePath));
            activeSprite = new Sprite(content.Load<Texture2D>(activeSpritePath));

            Bounds = idleSprite.GetBoundingRectangle(Position, 0, Vector2.One);
        }

        public virtual void Update(GameTime gameTime)
        {
            var playerDistance = Vector2.Distance(player.Position, Position);
            canInteract = playerDistance < interactDistance;
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
