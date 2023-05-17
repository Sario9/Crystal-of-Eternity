using GeonBit.UI.Entities;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Crystal_of_Eternity.UI
{
    public static class UIHelper
    {
        public static Button CreateButton(string text, SpriteFont font, ButtonSkin skin = ButtonSkin.Default,
            Anchor anchor = Anchor.Auto, Vector2? size = null, Vector2? offset = null)
        {
            var button = new Button("", skin, anchor, size, offset);

            var buttonText = new Label(text, Anchor.Center);
            buttonText.FontOverride = font;
            buttonText.Locked = true;

            button.ClearChildren();
            button.AddChild(buttonText);

            return button;
        }
    }
}
