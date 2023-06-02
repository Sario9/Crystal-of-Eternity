using Crystal_of_Eternity.UI;
using GeonBit.UI;
using GeonBit.UI.Entities;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

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

        private Label moneyCountText;
        private Label damageText;
        private Label attackSpeedText;
        private Label attackScaleText;

        private Texture2D coinImage;
        private Texture2D damageImage;
        private Texture2D attackSpeedImage;
        private Texture2D attackScaleImage;
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
            CreatePlayerInvenotyHUD();
            CreatePlayerStatsPanel();

            enemiesLeft = new Label("Осталось врагов: ???", Anchor.TopCenter, new(400, 50), new(0, 25)) { Scale = 1.25f };
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

        public void CreatePlayerStatsPanel()
        {
            var panel = new Panel(new(160, 300), PanelSkin.Fancy, Anchor.CenterLeft)
            {
                AdjustHeightAutomatically = true,
                Opacity = 240
            };
            var damage = new Image(damageImage, new(32, 32), ImageDrawMode.Stretch);
            damageText = new Label("xxx", Anchor.AutoInlineNoBreak) { Scale = 1.25f, Offset = new(15, 0) };

            var attackSpeed = new Image(attackSpeedImage, new(32, 32), ImageDrawMode.Stretch);
            attackSpeedText = new Label("xxx", Anchor.AutoInlineNoBreak) { Scale = 1.25f, Offset = new(15, 0) };

            var attackScale = new Image(attackScaleImage, new(32, 32), ImageDrawMode.Stretch);
            attackScaleText = new Label("xxx", Anchor.AutoInlineNoBreak) { Scale = 1.25f, Offset = new(15, 0) };

            panel.AddChild(damage);
            panel.AddChild(damageText);

            panel.AddChild(new LineSpace());

            panel.AddChild(attackSpeed);
            panel.AddChild(attackSpeedText);

            panel.AddChild(new LineSpace());

            panel.AddChild(attackScale);
            panel.AddChild(attackScaleText);

            UserInterface.Active.AddEntity(panel);
        }

        private void LoadContent()
        {
            fonts.Add("72", gameState.Content.Load<SpriteFont>("Font72"));
            fonts.Add("32", gameState.Content.Load<SpriteFont>("Font32"));
            coinImage = gameState.Content.Load<Texture2D>(SpriteNames.Coin1);
            damageImage = gameState.Content.Load<Texture2D>(SpriteNames.DamageIcon);
            attackScaleImage = gameState.Content.Load<Texture2D>(SpriteNames.AttackScaleIcon);
            attackSpeedImage = gameState.Content.Load<Texture2D>(SpriteNames.AttackSpeedIcon);
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

        public void ShowMerchantMenu(Merchant merchant)
        {
            var damage = merchant.AdditionalDamage;
            var speed = merchant.AdditionalAttackSpeed;
            var size = merchant.AdditionalAttackScale;

            var panel = new Panel(new(700, 400), PanelSkin.Default, Anchor.Center) { AdjustHeightAutomatically = true };
            var closeButton = UIHelper.CreateButton("X",
                fonts["32"],
                anchor: Anchor.TopRight,
                size: new(64, 64));

            var upgradeDamageButton = UIHelper.CreateButton
            (
                $"Увеличить урон оружия на {damage.count}",
                fonts["32"],
                anchor: Anchor.AutoCenter,
                size: new(500, 125)
            );

            var damageCostText = new Label($"{damage.price}$", anchor: Anchor.BottomRight)
            {
                FillColor = Color.Gold,
                Scale = 1.25f
            };

            var upgradeAttackSpeedButton = UIHelper.CreateButton
            (
                $"Увеличить скорость атаки на  {MathF.Round(speed.count * 100)}%",
                fonts["32"],
                anchor: Anchor.AutoCenter,
                size: new(500, 125)
            );

            var speedCostText = new Label($"{speed.price}$", anchor: Anchor.BottomRight)
            {
                FillColor = Color.Gold,
                Scale = 1.25f
            };

            var upgradeWeaponSizeButton = UIHelper.CreateButton
            (
                $"Увеличить размер оружия на {MathF.Round(size.count * 100)}%",
                fonts["32"],
                anchor: Anchor.AutoCenter,
                size: new(500, 125)
            );

            var sizeCostText = new Label($"{size.price}$", anchor: Anchor.BottomRight)
            {
                FillColor = Color.Gold,
                Scale = 1.25f
            };

            closeButton.Padding = new(0, 0);
            var playerMoney = merchant.CurrentPlayerMoney;

            upgradeDamageButton.AddChild(damageCostText);
            upgradeAttackSpeedButton.AddChild(speedCostText);
            upgradeWeaponSizeButton.AddChild(sizeCostText);

            if (playerMoney < damage.price)
            {
                upgradeDamageButton.Locked = true;
                upgradeDamageButton.FillColor = Color.DarkGray;
            }
            if (playerMoney < speed.price)
            {
                upgradeAttackSpeedButton.Locked = true;
                upgradeAttackSpeedButton.FillColor = Color.DarkGray;
            }
            if (playerMoney < size.price)
            {
                upgradeWeaponSizeButton.Locked = true;
                upgradeWeaponSizeButton.FillColor = Color.DarkGray;
            }

            upgradeDamageButton.OnClick += (btn) =>
            {
                merchant.IncreaseAttackDamage(damage.count, damage.price);
                UserInterface.Active.RemoveEntity(panel);
            };
            upgradeAttackSpeedButton.OnClick += (btn) =>
            {
                merchant.IncreaseAttackSpeed(speed.count, speed.price);
                UserInterface.Active.RemoveEntity(panel);
            };
            upgradeWeaponSizeButton.OnClick += (btn) =>
            {
                merchant.IncreaseAttackSize(size.count, size.price);
                UserInterface.Active.RemoveEntity(panel);
            };
            closeButton.OnClick += (btn) => { UserInterface.Active.RemoveEntity(panel); };

            var headerText = new Paragraph("Что сделать?", Anchor.TopCenter, Color.Green, 1.5f)
            {
                FontOverride = fonts["32"]
            };

            panel.AddChild(headerText);
            panel.AddChild(closeButton);
            panel.AddChild(new HorizontalLine());
            panel.AddChild(upgradeDamageButton);
            panel.AddChild(upgradeAttackSpeedButton);
            panel.AddChild(upgradeWeaponSizeButton);

            UserInterface.Active.AddEntity(panel);
        }

        public void CreatePlayerInvenotyHUD()
        {
            var panel = new Panel(new(150, 80), PanelSkin.None, Anchor.TopRight)
            {
                AdjustHeightAutomatically = false,
                Padding = new(5, 5),
                Opacity = 230,
                Offset = new(15, 15)
            };

            var coinPanel = new Panel(new(125, 60), PanelSkin.None, Anchor.Center);
            var coin = new Image(coinImage, new(20, 32), ImageDrawMode.Stretch, Anchor.CenterLeft);
            moneyCountText = new Label("1000", Anchor.CenterRight) { Scale = 1.25f };

            coinPanel.AddChild(coin);
            coinPanel.AddChild(moneyCountText);
            panel.AddChild(coinPanel);

            UserInterface.Active.AddEntity(panel);
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

        public void UpdateMoney(int count)
        {
            moneyCountText.Text = count.ToString();
        }

        public void UpdateStats(PlayerWeapon weapon)
        {
            damageText.Text = weapon.DamageWithModifier.ToString();
            attackSpeedText.Text = $"{MathF.Round(weapon.AttackIntervalWithModifier, 2)}";
            attackScaleText.Text = $"{MathF.Round(weapon.WeaponScaleWithModifier, 2)}";
        }
    }
}
