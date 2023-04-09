using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace Crystal_of_Eternity
{
    public class UserInput
    {
        public KeyboardState KeyboardState { get; private set; }

        private readonly Game game;

        public UserInput(Game game)
        {
            this.game = game;
        }

        public void Update(GameTime gameTime)
        {
            KeyboardState = Keyboard.GetState();
        }
    }
}
