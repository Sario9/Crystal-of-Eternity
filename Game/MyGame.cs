using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Crystal_of_Eternity
{
    public class MyGame : Game
    {
        public static MyGame Instance { get; private set; }
        public Player Player { get; private set; }
        public Level CurrentLevel { get; private set; }
        public Point BasicScreenSize = new(1280, 1024);

        private GraphicsDeviceManager graphics;
        private SpriteBatch spriteBatch;
        private MyCamera camera;

        public MyGame()
        {
            graphics = new GraphicsDeviceManager(this);
            graphics.PreferredBackBufferWidth = BasicScreenSize.X;
            graphics.PreferredBackBufferHeight = BasicScreenSize.Y;
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
            Instance = this;
        }

        protected override void Initialize()
        {
            CurrentLevel = new Level(LevelType.Level1, Vector2.One * 128);
            Player = new Player("Player", CurrentLevel.PlayerStartPosition, 100.0f);
            camera = new MyCamera(GraphicsDevice);
            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
        }

        protected override void Update(GameTime gameTime)
        {
            UserInput.Update(gameTime);
            camera.Update(gameTime, Player);
            Player.Update(gameTime);
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend,
                SamplerState.PointClamp, null, null, null, camera.Main.GetViewMatrix());
            CurrentLevel.Map.Draw(gameTime, spriteBatch);
            Player.Draw(gameTime, spriteBatch);
            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}