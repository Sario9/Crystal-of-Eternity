using Microsoft.Xna.Framework;
using System;

namespace Crystal_of_Eternity
{
    internal class FountainOfLife : InteractableEntity
    {
        private Player player;
        private readonly float additionalHealthPrecent = 0.25f;

        public FountainOfLife(Vector2 position, Player player) :
            base(position, SpriteNames.Hatch_idle, SpriteNames.Hatch_active)
        {
            this.player = player;
        }

        public FountainOfLife(Player player) : this(new(240, 240), player)
        {
            
        }

        public override void Interact()
        {
            player.ChangeMaxHealth(MathF.Round(player.MaxHP * (additionalHealthPrecent + 1)));
        }

        public override object Clone() => new FountainOfLife(Position, player);
    }
}
