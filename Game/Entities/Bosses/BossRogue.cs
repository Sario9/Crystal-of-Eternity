using Microsoft.Xna.Framework;
using System;

namespace Crystal_of_Eternity
{
    public class BossRogue : Boss
    {
        public BossRogue() : base
            (
                "Главарь бандитов",
                SpriteNames.Boss_rogue,
                SpriteNames.Boss_rogue_corpse,
                SoundNames.HumanHit, 200.0f,
                0.5f,
                25.0f,
                2.5f
            )
        {
            skills.Add(() => DashToPlayer());
        }

        private void DashToPlayer()
        {
            Vector2 offset;
            while(true)
            {
                offset = new Vector2(Randomizer.Random.Next(-128, 128), Randomizer.Random.Next(-128, 128));
                if (MathF.Abs(offset.X) > 64 && MathF.Abs(offset.Y) > 64)
                    break;
            }

            Position = player.Position + offset;
        }
    }
}