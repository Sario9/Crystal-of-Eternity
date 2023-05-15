using GeonBit.UI;
using GeonBit.UI.Entities;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Crystal_of_Eternity
{
    public class UI
    {
        private Panel panel;

        public UI() 
        {
            Initialize();
        }

        private void Initialize()
        {
            panel = new Panel(new(500, 500), PanelSkin.Simple, Anchor.Center);
            UserInterface.Active.AddEntity(panel);

            panel.AddChild(new Header("Text"));
            panel.AddChild(new HorizontalLine());
            panel.AddChild(new Paragraph("Text"));

            panel.AddChild(new Button("Click Me!", ButtonSkin.Default, Anchor.BottomCenter));
        }
    }
}
