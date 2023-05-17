using GeonBit.UI;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;

namespace Crystal_of_Eternity
{
    public class MyGame : Game
    {
        public static MyGame Instance { get; private set; }
        public Level CurrentLevel => Levels[currentLevelIndex];
        public List<Level> Levels { get; private set; }
        public Player Player { get; private set; }
        public MyCamera Camera { get; private set; }

        private GraphicsDeviceManager graphics;
        private SpriteBatch spriteBatch;
        private UI ui;
        private int currentLevelIndex = 0;

        public MyGame()
        {
            graphics = new GraphicsDeviceManager(this);
            graphics.PreferredBackBufferWidth = GameSettings.DefaultScreenSize.X;
            graphics.PreferredBackBufferHeight = GameSettings.DefaultScreenSize.Y;
            Content.RootDirectory = "Content";
            Instance = this;
        }

        protected override void Initialize()
        {
            Levels = new List<Level>()
            {
                new Level(LevelType.Level1),
                new Level(LevelType.Level2),
            };
            CurrentLevel.Initialize();

            Player = CurrentLevel.Player;

            Camera = new MyCamera(GraphicsDevice);

            UserInterface.Initialize(Content, BuiltinThemes.hd);
            ui = new UI(this);

            Player.onTakehit += ui.UpdateHealth;
            Player.OnDeath += ui.ShowPlayerDeathText;
            CurrentLevel.currentRoom.onEnemyDie += ui.UpdateEnemies;
            Player.TakeHit(0);

            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
        }

        public void ChangeLevel(int index)
        {
            currentLevelIndex = index;
            CurrentLevel.Initialize();
            RestartLevel();
        }

        public void ChangeRoom() 
        {
            CurrentLevel.ChangeRoom(1);
            RestartLevel();
        }

        public void RestartLevel()
        {
            Player = CurrentLevel.Player;
            Player.onTakehit += ui.UpdateHealth;
            Player.OnDeath += ui.ShowPlayerDeathText;
            CurrentLevel.currentRoom.onEnemyDie += ui.UpdateEnemies;
            Camera = new MyCamera(GraphicsDevice);
        }

        protected override void Update(GameTime gameTime)
        {
            UserInterface.Active.Update(gameTime);
            UserInput.Update(gameTime);
            Camera.Update(gameTime, Player);
            CurrentLevel.Update(gameTime);
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend,
                SamplerState.PointClamp, null, null, null, Camera.Main.GetViewMatrix());
            CurrentLevel.Draw(spriteBatch, gameTime);
            spriteBatch.End();
            UserInterface.Active.Draw(spriteBatch);
            base.Draw(gameTime);
        }
    }
}