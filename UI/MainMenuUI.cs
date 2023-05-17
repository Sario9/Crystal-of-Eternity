using Crystal_of_Eternity.UI;
using GeonBit.UI;
using GeonBit.UI.Entities;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace Crystal_of_Eternity
{
    public class MainMenuUI
    {
        #region Fields

        private MyGame game;
        private ContentManager content;
        private GraphicsDevice device;

        private readonly Dictionary<string, SpriteFont> fonts;

        #endregion

        public MainMenuUI(MyGame game, ContentManager content, GraphicsDevice device)
        {
            this.game = game;
            this.content = content;
            this.device = device;

            fonts = new Dictionary<string, SpriteFont>();

            LoadContent();
            Initialize();
        }

        private void Initialize()
        {
            var panel = new Panel(new(500, 500));
            var playButton = UIHelper.CreateButton("Play", fonts["32"]);
            var quitButton = UIHelper.CreateButton("Quit", fonts["32"]);

            playButton.OnClick += (btn) => game.ChangeState(new GameState(game, content, device));
            quitButton.OnClick += (btn) => game.Exit();

            panel.AddChild(new Label("MainMenu", Anchor.TopCenter));
            panel.AddChild(new HorizontalLine());
            panel.AddChild(playButton);
            panel.AddChild(quitButton);

            UserInterface.Active.AddEntity(panel);
        }

        private void LoadContent()
        {
            fonts.Add("72", game.Content.Load<SpriteFont>("Font72"));
            fonts.Add("32", game.Content.Load<SpriteFont>("Font32"));
        }

        private void Restart()
        {
            UserInterface.Active.Clear();
            Initialize();
        }
    }
}
