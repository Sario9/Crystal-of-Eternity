using GeonBit.UI;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended;
using System.Collections.Generic;

namespace Crystal_of_Eternity
{
    public class GameState : State
    {
        #region Fields
        public Level CurrentLevel => Levels[currentLevelIndex];
        public List<Level> Levels { get; private set; }
        public Player Player { get; private set; }
        public MyCamera Camera { get; private set; }

        private GameUI ui;
        private int currentLevelIndex = 0;
        #endregion

        public GameState(MyGame game, ContentManager content, GraphicsDevice graphicsDevice) : base(game, content, graphicsDevice)
        {
            Initialize();
        }

        public void Initialize()
        {
            UserInterface.Active.Clear();
            UserInterface.Initialize(Content, BuiltinThemes.hd);
            ui = new GameUI(this);

            Player = new Player(this, 100.0f, 0.7f, 0);
            Player.onHealthChanged += ui.UpdateHealth;
            Player.OnDeath += ui.ShowPlayerDeathText;

            Levels = new List<Level>()
            {
                new Level(LevelType.Level1),
                new Level(LevelType.Level2),
            };
            CurrentLevel.Initialize(Player);

            Camera = new MyCamera(this, graphicsDevice);

            CurrentLevel.currentRoom.onEnemyDie += ui.UpdateEnemies;
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
            UserInput.InGameGeneral(this);
            Camera.Update(gameTime, Player);
            CurrentLevel.Update(gameTime);
        }

        public void ChangeLevel(int index)
        {
            currentLevelIndex = index;
            CurrentLevel.Initialize(Player);
            RestartLevel();
        }

        public void ChangeRoom(int index)
        {
            CurrentLevel.ChangeRoom(index);
            RestartLevel();
        }

        public void RestartLevel()
        {
            CurrentLevel.currentRoom.onEnemyDie -= ui.UpdateEnemies;
            CurrentLevel.Initialize(Player);
            Player.Restart();
            CurrentLevel.currentRoom.onEnemyDie += ui.UpdateEnemies;
            Camera = new MyCamera(this, graphicsDevice);
        }

    }
}
