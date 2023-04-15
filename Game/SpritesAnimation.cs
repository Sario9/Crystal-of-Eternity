using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended;
using System.Collections.Generic;

namespace Crystal_of_Eternity
{
    public class SpritesAnimation
    {
        public bool IsPlaying { get; private set; }
        public bool IsLoop { get; private set; }
        public Vector2 Scale { get; private set; }

        private List<Texture2D> sprites;
        private readonly float tick;
        private float timer;
        private int currentFrame;
        private float rotation;

        public SpritesAnimation(float tickTime, Vector2 scale, bool playFromStart = false, bool isLoop = false) 
        {
            sprites = new List<Texture2D>();
            tick = tickTime;
            IsPlaying = playFromStart;
            IsLoop = isLoop;
            Scale = scale;
        }

        public void Add(Texture2D texture) => sprites.Add(texture);
        public void AddMany(params Texture2D[] textures)
        {
            foreach (var texture in textures)
                sprites.Add(texture);
        }

        public void Play()
        {
            IsPlaying = true;
            currentFrame = 0;
        }

        public void Stop()
        {
            IsPlaying = false;
            currentFrame = -1;
            timer = 0;
        }

        public void SetRotation(float rotation) => this.rotation = rotation;

        public void Update(GameTime gameTime)
        {
            if(IsPlaying)
            {
                timer += gameTime.GetElapsedSeconds();
                if (timer > tick)
                {
                    timer = 0;
                    currentFrame++;
                    if (currentFrame >= sprites.Count)
                    {
                        if(!IsLoop)
                            Stop();
                        else 
                            currentFrame = 0;
                    }
                }
            }
        }

        public void Draw(SpriteBatch spriteBatch, Vector2 position)
        {
            if(currentFrame != -1)
                spriteBatch.Draw(sprites[currentFrame], position, null, Color.White,
                    rotation, new(sprites[currentFrame].Width / 2, sprites[currentFrame].Height / 2), Scale, SpriteEffects.None, 0);
        }
    }
}
