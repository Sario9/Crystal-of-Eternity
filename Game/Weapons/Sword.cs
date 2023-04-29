using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crystal_of_Eternity
{
    public class Sword : Weapon
    {
        public Sword(float size) :
            base
            (
                15.0f, 0.03f, 35.0f,
                new[]
                {
                    SpriteNames.Attack_1,
                    SpriteNames.Attack_2,
                    SpriteNames.Attack_3,
                    SpriteNames.Attack_4,
                    SpriteNames.Attack_5,
                    SpriteNames.Attack_6
                }, size)
        {

        }
        
    }
}
