using GeonBit.UI;
using GeonBit.UI.Entities;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace Crystal_of_Eternity
{
    public class UI
    {
        private Panel playerHealthPanel;
        private ProgressBar healthBar;
        private Paragraph healthText;

        private Paragraph enemiesLeft;

        private Panel playerDeathPanel;
        private Paragraph playerDeathText;
        private MyGame game;

        private readonly Dictionary<string, SpriteFont> fonts;

        public UI(MyGame game)
        {
            this.game = game;
            fonts = new Dictionary<string, SpriteFont>();
            LoadContent();
            Initialize();
        }

        private void Initialize()
        {
            CreatePlayerHealthPanel();

            enemiesLeft = new Paragraph("Enemies left: ???", Anchor.TopRight, new(300, 50), new(25, 25), 1f);

            UserInterface.Active.AddEntity(enemiesLeft);
        }

        private void CreatePlayerHealthPanel()
        {
            playerHealthPanel = new Panel(new(700, 100), PanelSkin.None, Anchor.TopLeft);
            healthBar = new ProgressBar(0, 100, new(500, 50), Anchor.Auto);
            healthBar.Opacity = 250;
            healthBar.Value = 100;
            healthText = new Paragraph("XXX/XXX", Anchor.Center, scale: 1.2f);
            healthText.TextStyle = FontStyle.Bold;

            healthBar.AddChild(healthText);

            playerHealthPanel.AddChild(healthBar);

            UserInterface.Active.AddEntity(playerHealthPanel);
        }

        private void LoadContent()
        {
            fonts.Add("72", game.Content.Load<SpriteFont>("Font72"));
            fonts.Add("32", game.Content.Load<SpriteFont>("Font32"));
        }

        public void UpdateHealth(float currentHP, float maxHP)
        {
            healthBar.Value = (int)(currentHP / maxHP * 100);
            healthText.Text = string.Format("{0}/{1}", currentHP, maxHP);
        }

        public void UpdateEnemies(int count)
        {
            enemiesLeft.Text = string.Format("Enemies left: {0}", count);
        }

        public void ShowPlayerDeathText(MovableEntity player)
        {
            playerDeathPanel = new Panel(new(1200, 400), PanelSkin.None, Anchor.Center);
            playerDeathText = new Paragraph("YOU ARE DEAD!", Anchor.CenterRight, Color.DarkRed, 1.5f);
            playerDeathText.FontOverride = fonts["72"];
            playerDeathPanel.AddChild(CreateButton("Restart", fonts["32"], anchor: Anchor.BottomCenter, size: new(250, 70)));
            playerDeathPanel.AddChild(playerDeathText);

            UserInterface.Active.AddEntity(playerDeathPanel);
        }

        private Button CreateButton(string text, SpriteFont font, ButtonSkin skin = ButtonSkin.Default,
            Anchor anchor = Anchor.Auto, Vector2? size = null, Vector2? offset = null)
        {
            var button = new Button("", skin, anchor, size, offset);

            var buttonText = new Label(text, Anchor.Center);
            buttonText.FontOverride = font;
            buttonText.Locked = true;

            button.ClearChildren();
            button.AddChild(buttonText);

            return button;
        }
    }
}
