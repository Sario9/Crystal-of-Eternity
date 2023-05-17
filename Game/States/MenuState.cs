using GeonBit.UI;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Crystal_of_Eternity
{
    public class MenuState : State
    {
        #region Fields

        #endregion

        public MenuState(MyGame game, ContentManager content, GraphicsDevice graphicsDevice)
            : base(game, content, graphicsDevice)
        {
            UserInterface.Initialize(content, BuiltinThemes.hd);
            var ui = new MainMenuUI(game, content, graphicsDevice);
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            UserInterface.Active.Draw(spriteBatch);
        }

        public override void PostUpdate(GameTime gameTime)
        {
            //удаляем спрайт, когда не нужен
        }

        public override void Update(GameTime gameTime)
        {
            UserInterface.Active.Update(gameTime);
        }
    }
}
