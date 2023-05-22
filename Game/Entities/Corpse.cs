using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended;
using MonoGame.Extended.Collisions;
using MonoGame.Extended.Sprites;
using System.Data;

namespace Crystal_of_Eternity
{
    public class Corpse : IEntity
    {
        #region Fields
        public Vector2 Position { get; set; }
        public IShapeF Bounds { get; protected set; }

        private Sprite sprite;
        private string spritePath;

        private float rotation;

        #endregion

        public Corpse(string spritePath, Vector2 position)
        {
            this.spritePath = spritePath;
            rotation = Randomizer.Random.Next(10);
            Position = position;

            LoadContent();
        }

        private void LoadContent()
        {
            var content = MyGame.Instance.Content;
            sprite = new(content.Load<Texture2D>(spritePath));
        }

        public void Update(GameTime gameTime)
        {

        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            sprite.Draw(spriteBatch, Position, rotation, Vector2.One);
        }

        public void DrawBounds(SpriteBatch spriteBatch)
        {

        }

        public void OnCollision(CollisionEventArgs collisionInfo)
        {

        }

        public virtual object Clone() => throw new DataException();
    }
}
