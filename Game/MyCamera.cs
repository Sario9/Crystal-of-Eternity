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
            Main.Zoom = 2.0f;
            Main.MaximumZoom = 2.5f;
            Main.MinimumZoom = 1.5f;
        }

        public void Update(GameTime gameTime, Player player)
        {
            Main.ZoomIn(UserInput.MakeZoom(0.1f));
            Main.LookAt(player.Position);
        }
    }
}
