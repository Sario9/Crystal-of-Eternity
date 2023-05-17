﻿using Microsoft.Xna.Framework;
using MonoGame.Extended;

namespace Crystal_of_Eternity
{
    public class Rogue : Enemy
    {
        public Rogue(int type, Vector2 position, RectangleF mapBounds, Player player) :
            base("Rogue", type == 1 ? SpriteNames.Rogue_1 : SpriteNames.Rogue_2,
                SpriteNames.Rogue_corpse, SoundNames.HumanHit, position, 50.0f, 0.75f, 1.0f, 0.2f, mapBounds, player)
        {

        }
    }
}