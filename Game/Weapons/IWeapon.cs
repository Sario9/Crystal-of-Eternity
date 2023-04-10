using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended.Sprites;

namespace Crystal_of_Eternity
{
    public interface IWeapon
    {
        public Sprite Sprite { get; }

        public void Draw(SpriteBatch spriteBatch, Vector2 position);
    }
}
