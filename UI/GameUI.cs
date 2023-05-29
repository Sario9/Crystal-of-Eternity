using Crystal_of_Eternity.UI;
using GeonBit.UI;
using GeonBit.UI.Entities;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace Crystal_of_Eternity
{
    public class GameUI
    {
        #region Fields
        private Panel playerHealthPanel;
        private ProgressBar healthBar;
        private Paragraph healthText;

        private Paragraph enemiesLeft;

        private Panel playerDeathPanel;
        private Paragraph playerDeathText;
        private GameState gameState;

        private readonly Dictionary<string, SpriteFont> fonts; 
        #endregion

        public GameUI(GameState gameState)
        {
            this.gameState = gameState;
            fonts = new Dictionary<string, SpriteFont>();
            LoadContent();
            Initialize();
        }

        private void Initialize()
        {
            UserInterface.Active.Clear();

            CreatePlayerHealthPanel();

            enemiesLeft = new Paragraph("Осталось врагов: ???", Anchor.TopRight, new(300, 50), new(25, 25), 1f);
            UserInterface.Active.AddEntity(enemiesLeft);
        }

        private void CreatePlayerHealthPanel()
        {
            playerHealthPanel = new Panel(new(700, 100), PanelSkin.None, Anchor.TopLeft);
            healthBar = new ProgressBar(0, 100, new(500, 50), Anchor.Auto)
            {
                Opacity = 250,
                Value = 100
            };
            healthText = new Paragraph("XXX/XXX", Anchor.Center, scale: 1.2f)
            {
                TextStyle = FontStyle.Bold,
            };

            healthBar.AddChild(healthText);

            playerHealthPanel.AddChild(healthBar);

            UserInterface.Active.AddEntity(playerHealthPanel);
        }

        private void LoadContent()
        {
            fonts.Add("72", gameState.Content.Load<SpriteFont>("Font72"));
            fonts.Add("32", gameState.Content.Load<SpriteFont>("Font32"));
        }

        public void UpdateHealth(float currentHP, float maxHP)
        {
            currentHP = MathF.Round(currentHP);
            maxHP = MathF.Round(maxHP);
            healthBar.Value = (int)(currentHP / maxHP * 100);
            healthText.Text = string.Format("{0}/{1}", currentHP, maxHP);
        }

        public void UpdateEnemies(int count)
        {
            enemiesLeft.Text = string.Format("Осталось врагов: {0}", count);
        }

        public void ShowPlayerDeathPanel(MovableEntity player)
        {
            var restartButton = UIHelper.CreateButton("В меню", fonts["32"], anchor: Anchor.BottomCenter, size: new(250, 70));
            restartButton.OnClick += (btn) =>
            {
                UserInterface.Active.Clear();
                UserInput.ExitToMenu();
            };

            playerDeathPanel = new Panel(new(1200, 400), PanelSkin.None, Anchor.Center);
            playerDeathText = new Paragraph("ПОМЕР", Anchor.Center, Color.DarkRed, 1.5f)
            {
                FontOverride = fonts["72"]
            };
            playerDeathPanel.AddChild(restartButton);
            playerDeathPanel.AddChild(playerDeathText);

            UserInterface.Active.AddEntity(playerDeathPanel);
        }

        public void ShowFountainMenu(FountainOfLife fountain)
        {
            var panel = new Panel(new(1200, 400), PanelSkin.Default, Anchor.Center);
            var healButton = UIHelper.CreateButton("Восполнить 50% здоровья", fonts["32"], anchor: Anchor.BottomLeft, size: new(500, 125));
            var addMaxHealthButton = UIHelper.CreateButton("Увеличить максимальное здоровье на 25%", fonts["32"], anchor: Anchor.BottomRight, size: new(500, 125));
            healButton.OnClick += (btn) =>
            {
                fountain.Heal();
                UserInterface.Active.RemoveEntity(panel);
            };
            addMaxHealthButton.OnClick += (btn) =>
            {
                fountain.AddMaxHealth();
                UserInterface.Active.RemoveEntity(panel);
            };

            var headerText = new Paragraph("Что сделать?", Anchor.TopCenter, Color.Green, 1.5f)
            {
                FontOverride = fonts["32"]
            };
            panel.AddChild(headerText);
            panel.AddChild(new HorizontalLine());
            panel.AddChild(healButton);
            panel.AddChild(addMaxHealthButton);

            UserInterface.Active.AddEntity(panel);
        }
    }
}
