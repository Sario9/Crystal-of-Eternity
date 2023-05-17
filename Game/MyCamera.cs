using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended;

namespace Crystal_of_Eternity
{
    public class MyCamera
    {
        #region Fields
        public readonly OrthographicCamera Main;

        private const float maxZoomSpeed = 0.1f;

        private GraphicsDevice device; 
        private GameState gameState;
        #endregion

        public MyCamera(GameState gameState, GraphicsDevice device)
        {
            this.gameState = gameState;
            this.device = device;
            Main = new OrthographicCamera(device);
            UserInput.OnWheel += MakeZoom;
            Initialize();
        }

        public void Initialize()
        {
            var scale = GameSettings.Scale;

            Main.MaximumZoom = 3f * scale;
            Main.MinimumZoom = 2.5f * scale;
            Main.Zoom = Main.MinimumZoom;
        }

        public void Update(GameTime gameTime, Player player)
        {
            Main.LookAt(MapClampedPosition(player.Position));
        }

        private void MakeZoom(float delta)
        {
            Main.ZoomIn(MathHelper.Clamp(delta, -maxZoomSpeed, maxZoomSpeed));
        }

        private Vector2 MapClampedPosition(Vector2 position)
        {
            var level = gameState.CurrentLevel;
            var cameraMax = new Vector2(level.Map.Size.X * 31 -
                (device.Viewport.Width / Main.Zoom / 2),
                level.Map.Size.Y * 31 -
                (device.Viewport.Height / Main.Zoom / 2));

            return Vector2.Clamp(position,
               new Vector2(device.Viewport.Width / Main.Zoom / 2, device.Viewport.Height / Main.Zoom / 2) - Vector2.One * 6,
               cameraMax);
        }
    }
}
