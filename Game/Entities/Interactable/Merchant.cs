using Microsoft.Xna.Framework;

namespace Crystal_of_Eternity
{
    public class Merchant : InteractableEntity
    {
        private PlayerWeapon weapon;

        public Merchant(Vector2 position, PlayerWeapon weapon) :
            base(position, SpriteNames.Merchant, SpriteNames.Merchant_used)
        {
            this.weapon = weapon;
        }

        public override void Interact(GameUI ui)
        {
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
        }

        public void IncreaseAttackSpeed(float count)
        {
            weapon.AdditionalAttackSpeedPercent += count;
            weapon.UpdateModifiers();
        }

        public void IncreaseAttackSize(float count)
        {
            weapon.AdditionalSizePercent += count;
            weapon.UpdateModifiers();
        }
    }
}
