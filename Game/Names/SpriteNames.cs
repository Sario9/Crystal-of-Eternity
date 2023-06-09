﻿namespace Crystal_of_Eternity
{
    public static class SpriteNames
    {
        #region Character

        public const string Character_knight = "Game/General/Entities/Player/Character-knight";
        public const string Character_knight_dodge = "Game/General/Entities/Player/Character-knight_dodge";

        #endregion

        #region Enemies

        //Спрайты скелетов
        public const string Skeleton_1 = "Game/General/Entities/Enemies/Skeleton/Skeleton_1";
        public const string Skeleton_corpse = "Game/General/Entities/Enemies/Skeleton/Skeleton_corpse";

        //Спрайты разбойников
        public const string Rogue_1 = "Game/General/Entities/Enemies/Rogue/Rogue_1";
        public const string Rogue_2 = "Game/General/Entities/Enemies/Rogue/Rogue_2";
        public const string Rogue_3 = "Game/General/Entities/Enemies/Rogue/Rogue_3";
        public const string Rogue_corpse = "Game/General/Entities/Enemies/Rogue/Rogue_corpse";

        //Спрайты демонов
        public const string Demon_1 = "Game/General/Entities/Enemies/Demon/Demon_1";
        public const string Demon_corpse = "Game/General/Entities/Enemies/Demon/Demon_corpse";

        //Анимация слайма
        public const string Slime1 = "Game/General/Entities/Enemies/Slime/Slime1";
        public const string Slime2 = "Game/General/Entities/Enemies/Slime/Slime2";
        public const string Slime3 = "Game/General/Entities/Enemies/Slime/Slime3";
        public const string Slime4 = "Game/General/Entities/Enemies/Slime/Slime4";
        public const string Slime5 = "Game/General/Entities/Enemies/Slime/Slime5";

        #endregion

        #region Bosses

        public const string Boss_rogue = "Game/General/Entities/Bosses/Boss_Rogue";
        public const string Boss_rogue_corpse = "Game/General/Entities/Bosses/Boss_Rogue_corpse";

        #endregion

        #region Attacks

        //Атака меча
        public const string Attack_1 = "Game/General/Entities/Player/Animations/Attack/Attack_1";
        public const string Attack_2 = "Game/General/Entities/Player/Animations/Attack/Attack_2";
        public const string Attack_3 = "Game/General/Entities/Player/Animations/Attack/Attack_3";
        public const string Attack_4 = "Game/General/Entities/Player/Animations/Attack/Attack_4";
        public const string Attack_5 = "Game/General/Entities/Player/Animations/Attack/Attack_5";
        public const string Attack_6 = "Game/General/Entities/Player/Animations/Attack/Attack_6";

        //Атака копья
        public const string SpearAttack_1 = "Game/General/Entities/Player/Animations/Attack/SpearAttack_1";
        public const string SpearAttack_2 = "Game/General/Entities/Player/Animations/Attack/SpearAttack_2";
        public const string SpearAttack_3 = "Game/General/Entities/Player/Animations/Attack/SpearAttack_3";
        public const string SpearAttack_4 = "Game/General/Entities/Player/Animations/Attack/SpearAttack_4";
        public const string SpearAttack_5 = "Game/General/Entities/Player/Animations/Attack/SpearAttack_5";
        public const string SpearAttack_6 = "Game/General/Entities/Player/Animations/Attack/SpearAttack_6";
        public const string SpearAttack_7 = "Game/General/Entities/Player/Animations/Attack/SpearAttack_7";
        public const string SpearAttack_8 = "Game/General/Entities/Player/Animations/Attack/SpearAttack_8";
        public const string SpearAttack_9 = "Game/General/Entities/Player/Animations/Attack/SpearAttack_9";

        #endregion

        #region Backgrounds

        public const string MainMenuBG = "Game/General/MainMenuBG";

        #endregion

        #region Icons

        public const string DamageIcon = "Game/General/DamageIcon";
        public const string AttackSpeedIcon = "Game/General/AttackSpeedIcon";
        public const string AttackScaleIcon = "Game/General/AttackSizeIcon";

        #endregion

        #region Interactable

        public const string InteractCloud = "Game/General/InteractCloud";

        public const string Hatch_idle = "Game/General/Environment/Hatch";
        public const string Hatch_active = "Game/General/Environment/Hatch_open";

        public const string Fountain_full = "Game/General/Environment/FountainOfLife_full";
        public const string Fountain_empty = "Game/General/Environment/FountainOfLife_empty";

        public const string Merchant = "Game/General/Environment/Merchant";
        public const string Merchant_used = "Game/General/Environment/Merchant_used";

        #endregion

        #region Dropable

        //Анимация монеты
        public const string Coin1 = "Game/General/Dropable/Coin/Coin_1";
        public const string Coin2 = "Game/General/Dropable/Coin/Coin_2";
        public const string Coin3 = "Game/General/Dropable/Coin/Coin_3";
        public const string Coin4 = "Game/General/Dropable/Coin/Coin_4";
        public const string Coin5 = "Game/General/Dropable/Coin/Coin_5";
        public const string Coin6 = "Game/General/Dropable/Coin/Coin_6";
        public const string Coin7 = "Game/General/Dropable/Coin/Coin_7";
        public static readonly string[] CoinAnimation = new[] { Coin1, Coin2, Coin3, Coin4, Coin5, Coin6, Coin7 };

        #endregion

        #region Other

        public const string Shadow = "Game/General/Entities/Shadow";

        #endregion
    }
}
