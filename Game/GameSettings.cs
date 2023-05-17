using Microsoft.Xna.Framework;

namespace Crystal_of_Eternity
{
    public static class GameSettings
    {
        public static readonly Point CurrentScreenSize = new(1920, 1080);
        public static readonly bool IsFullScreen = false;

        public static float ScaleX = (float)CurrentScreenSize.X / 1920;
        public static float ScaleY = (float)CurrentScreenSize.Y / 1080;
        public static float Scale = (ScaleX + ScaleY) / 2;
    }
}
