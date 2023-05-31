using GeonBit.UI;

namespace Crystal_of_Eternity
{
    public class Invenory
    {
        public int Money
        {
            get => money;
            set
            {
                money = value;
                gameUI.UpdateMoney(money);
            }
        }

        private int money;
        private GameUI gameUI;

        public Invenory(GameUI gameUI)
        {
            this.gameUI = gameUI;
            gameUI.UpdateMoney(money);
        }

        public void AddMoney(int count) => Money += count;
    }
}
