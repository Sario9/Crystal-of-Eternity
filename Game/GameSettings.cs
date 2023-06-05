using Microsoft.Xna.Framework;

namespace Crystal_of_Eternity
{
    public static class GameSettings
    {
        public static readonly Point CurrentScreenSize = new(1920, 1080);
        public static readonly bool IsFullScreen = false;

        //public static readonly Point CurrentScreenSize = new(2560, 1440);
        //public static readonly bool IsFullScreen = true;

        public static readonly float ScaleX = (float)CurrentScreenSize.X / 1920;
        public static readonly float ScaleY = (float)CurrentScreenSize.Y / 1080;
        public static readonly float Scale = (ScaleX + ScaleY) / 2;
    }
}
