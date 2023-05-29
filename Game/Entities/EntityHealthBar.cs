using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended;
using System;

namespace Crystal_of_Eternity
{
    public class EntityHealthBar
    {
        #region Fields
        public Color FillColor { get; set; }
        public Color BackgroundColor { get; set; }
        public Vector2 Size { get; set; }

        public float Value
        {
            get => value;
            set
            {
                if (value > 1) this.value = entity.MaxHP;
                else if (value < 0) this.value = 0;
                else this.value = value;
            }
        }

        private float value;
        private readonly MovableEntity entity;
        private Texture2D pixel;

        #endregion

        public EntityHealthBar(MovableEntity entity, Vector2 size, Color fillColor, Color backgroundColor)
        {
            this.entity = entity;
            Size = size;
            FillColor = fillColor;
            BackgroundColor = backgroundColor;

            LoadContent();
        }

        public void LoadContent()
        {
            pixel = new Texture2D(MyGame.Instance.GraphicsDevice, 1, 1);
            Color[] colorData = { Color.White };
            pixel.SetData(colorData);
        }

        public void UpdateValue()
        {
            value = entity.CurrentHP / entity.MaxHP;
        }

        public void Draw(SpriteBatch spriteBatch, Vector2 offset)
        {
            var start = entity.Position + offset;
            var widthDone = Value / 1.0f * Size.X;
            var widthLeft = Size.X - widthDone;

            spriteBatch.Draw(pixel, new Rectangle((int)start.X, (int)start.Y, (int)widthDone, (int)Size.Y), FillColor);
            spriteBatch.Draw(pixel, new Rectangle((int)(start.X + widthDone), (int)start.Y, (int)widthLeft, (int)Size.Y), BackgroundColor);
        }
    }
}
