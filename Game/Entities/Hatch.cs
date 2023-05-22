using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using System.Diagnostics;

namespace Crystal_of_Eternity
{
    public class Hatch : InteractableEntity
    {
        private Player player;

        public Hatch(Vector2 position, ContentManager content, Player player) :
            base(position, SpriteNames.Hatch_idle, SpriteNames.Hatch_active, content)
        {
            this.player = player;
        }

        public override void Update(GameTime gameTime)
        {
            var playerDistance = Vector2.Distance(player.Position, Position);
            canInteract = playerDistance < 20;
            if (canInteract)
                Debug.Print("Can interact");

        }
    }
}
