using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace Crystal_of_Eternity
{
    public static class UserInput
    {

        public static KeyboardState KeyboardState { get; private set; }
        private static KeyboardState lastKeyboardState;

        public static MouseState MouseState { get; private set; }
        private static MouseState lastMouseState;

        public delegate void MouseButtonsHandler();
        public static MouseButtonsHandler OnLMBPressed;
        public static MouseButtonsHandler OnLMBReleased;
        public static MouseButtonsHandler OnRMBPressed;
        public static MouseButtonsHandler OnRMBReleased;

        public delegate void MouseWheelHandler(float delta);
        public static MouseWheelHandler OnWheel;

        public delegate void MovementHandler(Vector2 direction, GameTime gameTime);
        public static MovementHandler OnMove;

        private static MyGame game;

        static UserInput()
        {
            game = MyGame.Instance;
        }

        private static void General()
        {
            if (KeyboardState.IsKeyDown(KeyBinds.Exit))
                game.Exit();
            if (KeyboardState.IsKeyDown(Keys.R) && !lastKeyboardState.IsKeyDown(Keys.R))
                game.RestartLevel();
            if (KeyboardState.IsKeyDown(Keys.T) && !lastKeyboardState.IsKeyDown(Keys.T))
                game.ChangeRoom();
            if (KeyboardState.IsKeyDown(Keys.Y) && !lastKeyboardState.IsKeyDown(Keys.Y))
                game.ChangeLevel(1);
        }

        public static Point GetMousePosition() => MouseState.Position;
        public static void UpdateMouseState(GameTime gameTime)
        {
            lastMouseState = MouseState;
            MouseState = Mouse.GetState();

            var isLMBStateChaged = MouseState.LeftButton != lastMouseState.LeftButton;
            var isRMBStateChaged = MouseState.RightButton != lastMouseState.RightButton;
            if (isLMBStateChaged)
            {
                if (MouseState.LeftButton == ButtonState.Pressed)
                    OnLMBPressed?.Invoke();
                if (MouseState.LeftButton == ButtonState.Released)
                    OnLMBReleased?.Invoke();
            }
            if (isRMBStateChaged)
            {
                if (MouseState.RightButton == ButtonState.Pressed)
                    OnRMBPressed?.Invoke();
                if (MouseState.RightButton == ButtonState.Released)
                    OnRMBReleased?.Invoke();
            }

            if (MouseState.ScrollWheelValue != 0)
                OnWheel.Invoke(MouseState.ScrollWheelValue - lastMouseState.ScrollWheelValue);
        }

        public static void UpdateKeyboardState(GameTime gameTime)
        {
            lastKeyboardState = KeyboardState;
            KeyboardState = Keyboard.GetState();

            Move(gameTime);
        }

        private static void Move(GameTime gameTime)
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

             OnMove?.Invoke(direction, gameTime);
        }

        public static void Update(GameTime gameTime)
        {
            UpdateKeyboardState(gameTime);
            UpdateMouseState(gameTime);
            General();
        }
    }
}
