using GeonBit.UI;
using GeonBit.UI.Entities;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace Crystal_of_Eternity
{
    public class MyGame : Game
    {
        public static MyGame Instance { get; private set; }
        public Level CurrentLevel { get; private set; }
        public List<Level> Levels { get; private set; }
        public Player Player { get; private set; }
        public MyCamera Camera { get; private set; }

        private GraphicsDeviceManager graphics;
        private SpriteBatch spriteBatch;
        private UI ui;

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
            UserInterface.Initialize(Content, BuiltinThemes.hd);
            ui = new UI();
            Levels = new List<Level>()
            {
                new Level(LevelType.Level1),
                new Level(LevelType.Level2),
            };
            if (CurrentLevel == null)
                CurrentLevel = Levels[1];
            CurrentLevel.Initialize();
            Player = CurrentLevel.Player;
            Camera = new MyCamera(GraphicsDevice);
            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
        }

        private void ChangeLevel(int index)
        {
            CurrentLevel = Levels[index];
            Initialize();
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