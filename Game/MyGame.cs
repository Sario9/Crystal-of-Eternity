using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended.Collisions;

namespace Crystal_of_Eternity
{
    public class MyGame : Game
    {
        public static MyGame Instance { get; private set; }
        public Level CurrentLevel { get; private set; }
        public Player Player { get; private set; }
        public MyCamera Camera { get; private set; }

        private GraphicsDeviceManager graphics;
        private SpriteBatch spriteBatch;
        private MyControls controls;

        public MyGame()
        {
            graphics = new GraphicsDeviceManager(this);
            graphics.PreferredBackBufferWidth = GameSettings.DefaultScreenSize.X;
            graphics.PreferredBackBufferHeight = GameSettings.DefaultScreenSize.Y;
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
            Instance = this;
        }

        protected override void Initialize()
        {
            controls = new MyControls(this);
            Components.Add(controls);
            CurrentLevel = new Level(LevelType.Level1, new(32, 32), Vector2.One * 128);
            CurrentLevel.Initialize();
            Player = CurrentLevel.Player;
            Player.onTakehit += controls.UpdatePlayerHP;
            Player.OnDeath += controls.EndGame;
            CurrentLevel.onEnemyDie += controls.UpdateEnemyCount;
            Camera = new MyCamera(GraphicsDevice);
            base.Initialize();

            controls.UpdateEnemyCount(CurrentLevel.MovableEntities.Count - 1);
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
        }

        protected override void Update(GameTime gameTime)
        {
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
            base.Draw(gameTime);
        }
    }
}