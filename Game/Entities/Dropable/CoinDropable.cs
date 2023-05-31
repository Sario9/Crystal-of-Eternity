using Microsoft.Xna.Framework;

namespace Crystal_of_Eternity
{
    public class CoinDropable : DropableEntity
    {
        public CoinDropable(Vector2 position, Player player) : base(position, player, SpriteNames.CoinAnimation)
        {
        }

        public override void Interact(Invenory invenory)
        {
            base.Interact(invenory);
            invenory.AddMoney(1);
        }
    }
}
