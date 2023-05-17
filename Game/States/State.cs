using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Crystal_of_Eternity
{
    public abstract class State
    {

        #region Fields

        public ContentManager Content { get; private set; }

        protected GraphicsDevice graphicsDevice;

        protected MyGame game;

        #endregion

        public abstract void Draw(GameTime gameTime, SpriteBatch spriteBatch);

        public abstract void PostUpdate(GameTime gameTime);

        public State(MyGame game, ContentManager content, GraphicsDevice graphicsDevice)
        {
            this.game = game;
            this.Content = content;
            this.graphicsDevice = graphicsDevice;
        }

        public abstract void Update(GameTime gameTime);
    }
}
