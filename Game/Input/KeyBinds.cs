using Microsoft.Xna.Framework.Input;

namespace Crystal_of_Eternity
{
    public static class KeyBinds
    {
        #region General

        public static readonly Keys Exit = Keys.Escape;

        #endregion

        #region Player

        //Движение
        public const Keys PlayerMoveUp = Keys.W;
        public const Keys PlayerMoveDown = Keys.S;
        public const Keys PlayerMoveLeft = Keys.A;
        public const Keys PlayerMoveRight = Keys.D;
        public const Keys PlayerDodge = Keys.Space;

        //Интерактив
        public const Keys Interact = Keys.E;
        #endregion

    }
}
