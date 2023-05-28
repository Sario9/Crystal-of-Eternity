using Microsoft.Xna.Framework;

namespace Crystal_of_Eternity
{
    public class Hatch : InteractableEntity
    {
        private GameState gameState;
        private bool isInteracted = false;

        public Hatch(Vector2 position, GameState gameState) :
            base(position, SpriteNames.Hatch_idle, SpriteNames.Hatch_active)
        {
            this.gameState = gameState;
        }

        public override void Interact()
        {
            gameState.NextRoom();
        }

        public override object Clone() => new Hatch(Position, gameState);
    }
}
