using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended.Collisions;

namespace Crystal_of_Eternity
{
    public class MyGame : Game
    {
        public static MyGame Instance { get; private set; }
        public Level CurrentLevel { get; private set; }

        private GraphicsDeviceManager graphics;
        private SpriteBatch spriteBatch;
        private MyCamera camera;
        private Player player;
        private readonly CollisionComponent collisionComponent;

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
            CurrentLevel = new Level(LevelType.Level1, new(32, 32), Vector2.One * 128);
            CurrentLevel.Initialize();
            player = CurrentLevel.Player;
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
            player.Update(gameTime);
            camera.Update(gameTime, player);
            CurrentLevel.Update(gameTime);
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend,
                SamplerState.PointClamp, null, null, null, camera.Main.GetViewMatrix());
            CurrentLevel.Map.Draw(gameTime, spriteBatch);
            player.Draw(gameTime, spriteBatch);
            CurrentLevel.Draw(spriteBatch);
            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}