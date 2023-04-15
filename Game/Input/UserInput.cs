using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace Crystal_of_Eternity
{
    public static class UserInput
    {
        public static KeyboardState KeyboardState { get; private set; }

        public static MouseState MouseState { get; private set; }
        private static MouseState lastMouseState;

        private static MyGame game;

        static UserInput()
        {
            game = MyGame.Instance;
        }

        private static void General()
        {
            if (KeyboardState.IsKeyDown(KeyBinds.Exit))
                game.Exit();
        }

        public static float MakeZoom(float maxSpeed)
        {
            return MathHelper.Clamp(MouseState.ScrollWheelValue - lastMouseState.ScrollWheelValue, -maxSpeed, maxSpeed);
        }

        public static Point GetMousePosition() => MouseState.Position;
        public static bool IsLMBPressed()
        {
            return lastMouseState.LeftButton == MouseState.LeftButton ? false : MouseState.LeftButton == ButtonState.Pressed;
        }

        public static Vector2 MovePlayer()
        {
            var direction = Vector2.Zero;

            if (KeyboardState.IsKeyDown(KeyBinds.PlayerMoveUp))
                direction += new Vector2(0, -1);
            if (KeyboardState.IsKeyDown(KeyBinds.PlayerMoveDown))
                direction += new Vector2(0, 1);
            if (KeyboardState.IsKeyDown(KeyBinds.PlayerMoveLeft))
                direction += new Vector2(-1, 0);
            if (KeyboardState.IsKeyDown(KeyBinds.PlayerMoveRight))
                direction += new Vector2(1, 0);

            return direction;
        }

        public static void Update(GameTime gameTime)
        {
            KeyboardState = Keyboard.GetState();
            lastMouseState = MouseState;
            MouseState = Mouse.GetState();
            General();
        }
    }
}
