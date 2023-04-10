using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended;

namespace Crystal_of_Eternity
{
    public class MyCamera
    {
        public readonly OrthographicCamera Main;

        private GraphicsDevice device;

        public MyCamera(GraphicsDevice device)
        {
            this.device = device;
            Main = new OrthographicCamera(device);
            Initialize();
        }

        public void Initialize()
        {
            Main.Zoom = 2.5f;
            Main.MaximumZoom = 2.5f;
            Main.MinimumZoom = 2f;
        }

        public void Update(GameTime gameTime, Player player)
        {
            Main.ZoomIn(UserInput.MakeZoom(0.1f));
            Main.LookAt(MapClampedPosition(player.Position));
        }

        private Vector2 MapClampedPosition(Vector2 position)
        {
            var level = MyGame.Instance.CurrentLevel;
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
