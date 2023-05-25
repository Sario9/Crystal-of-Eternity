using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace Crystal_of_Eternity
{
    public class MyGame : Game
    {
        #region Fields

        public static MyGame Instance { get; private set; }

        public Level CurrentLevel => Levels[currentLevelIndex];
        public List<Level> Levels { get; private set; }
        public Player Player { get; private set; }
        public MyCamera Camera { get; private set; }

        public State CurrentState;

        private GraphicsDeviceManager graphics;
        private SpriteBatch spriteBatch;
        private int currentLevelIndex = 0;
        
        #endregion

        public MyGame()
        {
            graphics = new GraphicsDeviceManager(this);
            graphics.PreferredBackBufferWidth = GameSettings.CurrentScreenSize.X;
            graphics.PreferredBackBufferHeight = GameSettings.CurrentScreenSize.Y;
            graphics.IsFullScreen = GameSettings.IsFullScreen;
            Content.RootDirectory = "Content";
            Instance = this;
        }

        protected override void Initialize()
        {
            UserInput.OnMenuExit += ExitToMenu;

            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);

            CurrentState = new MenuState(this, Content, GraphicsDevice);
        }

        public void ChangeState(State state)
        {
            CurrentState = state;
        }

        private void ExitToMenu()
        {
            ChangeState(new MenuState(this, Content, GraphicsDevice));
        }

        protected override void Update(GameTime gameTime)
        {
            CurrentState.Update(gameTime);
            CurrentState.PostUpdate(gameTime);

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            CurrentState.Draw(gameTime, spriteBatch);
            base.Draw(gameTime);
        }
    }
}