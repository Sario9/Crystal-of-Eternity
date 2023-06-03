using Microsoft.Xna.Framework;
using MonoGame.Extended;

namespace Crystal_of_Eternity
{
    public class WalkAnimation
    {
        #region Fields
        public float Step { get; private set; }
        public float Clamp { get; private set; }
        public float SpriteRotation { get; private set; }
        private bool rotateDirection = true; 
        #endregion

        public WalkAnimation(float step, float clamp)
        {
            Step = step;
            Clamp = clamp;
        }

        public void Play(GameTime gameTime)
        {
            var time = (float)gameTime.GetElapsedSeconds();
            if (SpriteRotation > Clamp || SpriteRotation < -Clamp)
                rotateDirection = !rotateDirection;
            var delta = time * Step;
            SpriteRotation += rotateDirection ? delta : -delta;
        }

        public void Reset()
        {
            SpriteRotation = 0f;
        }
    }
}
