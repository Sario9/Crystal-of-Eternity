using Microsoft.Xna.Framework;

namespace Crystal_of_Eternity
{
    public class Hatch : InteractableEntity
    {
        private GameState gameState;

        public Hatch(Vector2 position, GameState gameState) :
            base(position, SpriteNames.Hatch_idle, SpriteNames.Hatch_active)
        {
            this.gameState = gameState;
        }

        public override void Interact(GameUI ui)
        {
            gameState.NextRoom();
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            isActive = canInteract;
        }

        public override object Clone() => new Hatch(Position, gameState);
    }
}
