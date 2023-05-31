using Microsoft.Xna.Framework;
using MonoGame.Extended;

namespace Crystal_of_Eternity
{
    public class Merchant : InteractableEntity
    {
        private PlayerWeapon weapon;
        private GameUI ui;

        public Merchant(Vector2 position, PlayerWeapon weapon) :
            base(position, SpriteNames.Merchant, SpriteNames.Merchant_used)
        {
            this.weapon = weapon;
            Bounds = new RectangleF(Position - Vector2.One * 16, new(32, 32));
        }

        public override void Interact(GameUI ui)
        {
            this.ui = ui;
            base.Interact(ui);
            if (!isUsed)
            {
                ui.ShowMerchantMenu(this);
                isActive = true;
                isUsed = true;
            }
        }

        public void IncreaseAttackDamage(float count)
        {
            weapon.AdditionalDamage += count;
            weapon.UpdateModifiers();
            ui.UpdateStats(weapon);
        }

        public void IncreaseAttackSpeed(float count)
        {
            weapon.AdditionalAttackSpeedPercent += count;
            weapon.UpdateModifiers();
            ui.UpdateStats(weapon);
        }

        public void IncreaseAttackSize(float count)
        {
            weapon.AdditionalSizePercent += count;
            weapon.UpdateModifiers();
            ui.UpdateStats(weapon);
        }
    }
}
