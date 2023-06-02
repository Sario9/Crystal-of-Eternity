using GeonBit.UI;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using System.Linq;

namespace Crystal_of_Eternity
{
    public class GameState : State
    {
        #region Fields
        public Level CurrentLevel => Levels[currentLevelIndex];
        public List<Level> Levels { get; private set; }
        public Player Player { get; private set; }
        public Invenory Invenory { get; private set; }
        public MyCamera Camera { get; private set; }
        public GameUI UI { get; private set; }

        private int currentLevelIndex = 0;
        #endregion

        public GameState(MyGame game, ContentManager content, GraphicsDevice graphicsDevice) : base(game, content, graphicsDevice)
        {
            Initialize();
        }

        public void Initialize()
        {
            UserInterface.Active.Clear();
            UI = new GameUI(this);

            Invenory = new Invenory(UI);
            Player = new Player(this, 100.0f, 0.7f, 0);
            Player.onHealthChanged += UI.UpdateHealth;
            Player.OnDeath += UI.ShowPlayerDeathPanel;
            Levels = LevelsList.GetLevels();

            CurrentLevel.Initialize(Player, this);

            Camera = new MyCamera(this, graphicsDevice);

            CurrentLevel.CurrentRoom.OnEnemyChangeState += UI.UpdateEnemies;

            UI.UpdateStats(Player.Weapon);
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend,
               SamplerState.PointClamp, null, null, null, Camera.Main.GetViewMatrix());

            CurrentLevel.Draw(spriteBatch, gameTime);

            spriteBatch.End();
            UserInterface.Active.Draw(spriteBatch);
        }

        public override void PostUpdate(GameTime gameTime)
        {
            //
        }

        public override void Update(GameTime gameTime)
        {
            UserInterface.Active.Update(gameTime);
            UserInput.Update(game, gameTime);
            UserInput.Debug(this);
            Camera.Update(gameTime, Player);
            CurrentLevel.Update(gameTime);
        }

        public void ChangeLevel(int index)
        {
            if (index >= Levels.Count)
            {
                UserInput.ExitToMenu();
                return;
            }

            currentLevelIndex = index;
            CurrentLevel.Initialize(Player, this);
            ReloadState();
        }

        public void NextRoom()
        {
            if (CurrentLevel.NextRoom(Player) == null)
                ChangeLevel(currentLevelIndex + 1);

            CurrentLevel.CurrentRoom.OnEnemyChangeState += UI.UpdateEnemies;
        }

        public void ReloadState()
        {
            Camera = new MyCamera(this, graphicsDevice);
        }
    }
}
