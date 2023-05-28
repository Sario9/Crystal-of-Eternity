using Microsoft.Xna.Framework;
using MonoGame.Extended;
using System;

namespace Crystal_of_Eternity
{
    internal class FountainOfLife : InteractableEntity
    {
        private Player player;
        private readonly float additionalHealthPrecent = 0.25f;

        public FountainOfLife(Vector2 position, Player player) :
            base(position, SpriteNames.Fountain_idle, SpriteNames.Fountain_active)
        {
            this.player = player;
            Bounds = new RectangleF(position - new Vector2(16, 12), new(32, 24));
        }

        public FountainOfLife(Player player) : this(new(400, 240), player)
        {

        }

        public override void Interact()
        {
            if (!isUsed)
            {
                player.ChangeMaxHealth(MathF.Round(player.MaxHP * (additionalHealthPrecent + 1)));
                isActive = true;
                isUsed = true;
            }
        }

        public override object Clone() => new FountainOfLife(Position, player);
    }
}
