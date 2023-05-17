using GeonBit.UI;
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
        private State nextState;

        private GraphicsDeviceManager graphics;
        private SpriteBatch spriteBatch;
        private GameUI ui;
        private int currentLevelIndex = 0;
        
        #endregion

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
            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);

            CurrentState = new MenuState(this, Content, GraphicsDevice);
        }

        public void ChangeState(State state)
        {
            nextState = state;
        }

        protected override void Update(GameTime gameTime)
        {
            if(nextState != null)
            {
                CurrentState = nextState;
                nextState = null;
            }

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