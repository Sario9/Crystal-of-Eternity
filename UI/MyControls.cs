using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using MonoGame.UI.Forms;


namespace Crystal_of_Eternity
{
    public class MyControls : ControlManager
    {
        private Progressbar playerHealthBar;
        private Label enemyCounter;

        public MyControls(Game game) : base(game)
        {
            
        }

        public override void InitializeComponent()
        {
            playerHealthBar = new Progressbar()
            {
                Size = new(250, 25),
                Location = new(25, 25),
                BackgroundColor = Color.DarkGray,
                BarColor = Color.IndianRed,
                Max = 100,
                Value = 100,
            };
            enemyCounter = new Label()
            {
                Size = new(250, 25),
                Location = new(Game.GraphicsDevice.Viewport.Width - 350, 25),
                Text = "Enemies left: xxx",
                FontName = "Font32"
            };
            Controls.Add(playerHealthBar);
            Controls.Add(enemyCounter);
        }

        public void UpdatePlayerHP(float hp, float maxHP)
        {
            playerHealthBar.Value = (int)(hp/maxHP * 100);
            Debug.Print(playerHealthBar.Value.ToString());
        }

        public void EndGame(IEntity entity)
        {
            var label = new Label()
            {
                Text = "YOU ARE DEAD",
                TextColor = Color.DarkRed,
                Location = Game.GraphicsDevice.Viewport.Bounds.Center.ToVector2() - new Vector2(320, 72),
                FontName = "Font72"
            };

            Controls.Add(label);
        }

        public void UpdateEnemyCount(int count)
        {
            enemyCounter.Text = string.Format("Enemies left: {0}", count);
        }
    }
}
