using Microsoft.Xna.Framework;
using MonoGame.Extended;
using System;

namespace Crystal_of_Eternity
{
    public class FountainOfLife : InteractableEntity
    {
        private Player player;
        private readonly float additionalHealthPrecent = 0.25f;

        public FountainOfLife(Vector2 position, Player player) :
            base(position, SpriteNames.Fountain_full, SpriteNames.Fountain_empty)
        {
            this.player = player;
            Bounds = new RectangleF(position - new Vector2(27, 20), new(54, 40));
        }

        public FountainOfLife(Player player) : this(new(400, 240), player)
        {

        }

        public override void Interact(GameUI ui)
        {
            if (!isUsed)
            {
                ui.ShowFountainMenu(this);
                isActive = true;
                isUsed = true;
            }
        }

        public void AddMaxHealth()
        {
            player.ChangeMaxHealth(MathF.Round(player.MaxHP * (additionalHealthPrecent + 1)));
        }

        public void Heal()
        {
            player.CurrentHP += player.MaxHP * 0.5f;
        }

        public override object Clone() => new FountainOfLife(Position, player);
    }
}
