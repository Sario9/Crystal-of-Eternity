using Microsoft.Xna.Framework;
using MonoGame.Extended;

namespace Crystal_of_Eternity
{
    public class Merchant : InteractableEntity
    {
        public (float count, int price) AdditionalDamage = (5.0f, 40);
        public (float count, int price) AdditionalAttackSpeed = (0.25f, 50);
        public (float count, int price) AdditionalAttackScale = (0.30f, 30);
        public int CurrentPlayerMoney => invenory.Money;

        private PlayerWeapon weapon;
        private GameUI ui;
        private Invenory invenory;

        public Merchant(Vector2 position, PlayerWeapon weapon, Invenory invenory) :
            base(position, SpriteNames.Merchant, SpriteNames.Merchant_used)
        {
            this.weapon = weapon;
            Bounds = new RectangleF(Position - Vector2.One * 16, new(32, 32));
            this.invenory = invenory;
        }

        public override void Interact(GameUI ui)
        {
            this.ui = ui;
            base.Interact(ui);
            if (!isUsed)
                ui.ShowMerchantMenu(this);
        }

        public void IncreaseAttackDamage(float count, int price)
        {
            weapon.AdditionalDamage += count;
            MakePurchase(price);
        }

        public void IncreaseAttackSpeed(float count, int price)
        {
            weapon.AdditionalAttackSpeedPercent += count;
            MakePurchase(price);
        }

        public void IncreaseAttackSize(float count, int price)
        {
            weapon.AdditionalSizePercent += count;
            MakePurchase(price);
        }

        private void MakePurchase(int price)
        {
            isActive = true;
            isUsed = true;
            weapon.UpdateModifiers();
            ui.UpdateStats(weapon);
            invenory.PayMoney(price);
        }
    }
}
