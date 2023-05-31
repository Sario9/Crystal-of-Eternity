using Microsoft.Xna.Framework;
using MonoGame.Extended;

namespace Crystal_of_Eternity
{
    public class Rogue : Enemy
    {
        public Rogue(int type) :
            base("Rogue", type == 1 ? SpriteNames.Rogue_1 : type == 2 ? SpriteNames.Rogue_2 : SpriteNames.Rogue_3,
                SpriteNames.Rogue_corpse, SoundNames.HumanHit, 50.0f, 0.45f, 5f, 1f)
        {

        }
    }
}
