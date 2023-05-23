using Autofac.Core;
using Crystal_of_Eternity.UI;
using GeonBit.UI;
using GeonBit.UI.Entities;
using Microsoft.Xna.Framework;
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

        private Texture2D backgroundImage;

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
            UserInterface.Active.Clear();

            var scale = UserInterface.Active.GlobalScale = GameSettings.Scale;
            var background = new Image(backgroundImage, device.Viewport.Bounds.Size.ToVector2() / scale);
            var panel = new Panel(new(device.Viewport.Width * 0.35f / scale, device.Viewport.Height / scale), PanelSkin.Default, Anchor.TopLeft);
            var label = new Label("CRYSTAL OF ETERNITY", Anchor.TopCenter);
            var playButton = UIHelper.CreateButton("Играть", fonts["32"]);
            var quitButton = UIHelper.CreateButton("Выйти из игры", fonts["32"]);
            ((Label)quitButton.Children[0]).FillColor = Color.IndianRed;

            playButton.OnClick += (btn) => game.ChangeState(new GameState(game, content, device));
            quitButton.OnClick += (btn) => game.Exit();

            label.FontOverride = fonts["52"];
            label.FillColor = Color.Cyan;
            label.AlignToCenter = true;

            panel.AddChild(label);
            panel.AddChild(new HorizontalLine());
            panel.AddChild(playButton);
            panel.AddChild(quitButton);
            panel.Opacity = 250;

            UserInterface.Active.AddEntity(background);
            UserInterface.Active.AddEntity(panel);

            //ShowSettingsPanel(scale);
        }

        private void ShowSettingsPanel(float scale)
        {
            var panel = new Panel(new(device.Viewport.Width * 0.6f / scale, device.Viewport.Height / scale), PanelSkin.Default, Anchor.Center);
            var label = new Label("Настройки", Anchor.TopCenter);

            var resolutionPanel = new Panel(new(700, 100), PanelSkin.Simple);
            var resolutionList = new DropDown(new(500, 75), Anchor.Auto, null, PanelSkin.Fancy);

            panel.Offset = new(device.Viewport.Width * 0.15f, 0);
            label.FontOverride = fonts["32"];

            resolutionPanel.AddChild(resolutionList);

            panel.AddChild(label);
            panel.AddChild(new HorizontalLine());
            panel.AddChild(resolutionPanel);

            UserInterface.Active.AddEntity(panel);
        }

        private void LoadContent()
        {
            fonts.Add("72", game.Content.Load<SpriteFont>("Font72"));
            fonts.Add("52", game.Content.Load<SpriteFont>("Font52"));
            fonts.Add("32", game.Content.Load<SpriteFont>("Font32"));

            backgroundImage = game.Content.Load<Texture2D>(SpriteNames.MainMenuBG);
        }

        private void Restart()
        {
            UserInterface.Active.Clear();
            Initialize();
        }
    }
}
