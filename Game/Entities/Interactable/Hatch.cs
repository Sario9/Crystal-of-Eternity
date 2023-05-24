using Microsoft.Xna.Framework;
using System.Diagnostics;

namespace Crystal_of_Eternity
{
    public class Hatch : InteractableEntity
    {
        private GameState gameState;

        public Hatch(Vector2 position, Player player, GameState gameState) :
            base(position, SpriteNames.Hatch_idle, SpriteNames.Hatch_active, player, 50)
        {
            this.player = player;
            this.gameState = gameState;
        }

        public override void Interact()
        {
            Debug.Print("Change");
            gameState.NextRoom();
            UserInput.OnInteract -= Interact;
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            if (canInteract && UserInput.OnInteract == null)
                UserInput.OnInteract += Interact;

            else if (!canInteract && UserInput.OnInteract != null)
                    UserInput.OnInteract -= Interact;

            isActive = canInteract;
        }

        public override object Clone() => new Hatch(Position, player, gameState);
    }
}
