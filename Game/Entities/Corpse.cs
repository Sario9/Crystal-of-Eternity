using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended.Sprites;

namespace Crystal_of_Eternity
{
    public class Corpse
    {
        #region Fields
        private Sprite sprite;
        private string spritePath;

        private Vector2 position;

        private float rotation; 
        #endregion

        public Corpse(string spritePath, Vector2 position)
        {
            this.spritePath = spritePath;
            rotation = Randomizer.Random.Next(10);
            this.position = position;

            LoadContent();
        }

        private void LoadContent()
        {
            var content = MyGame.Instance.Content;
            var texture = content.Load<Texture2D>(spritePath);
            sprite = new(texture);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            sprite.Draw(spriteBatch, position, rotation, Vector2.One);
        }
    }
}
