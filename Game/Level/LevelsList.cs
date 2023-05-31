using System.Collections.Generic;

namespace Crystal_of_Eternity
{
    public static class LevelsList
    {
        public static List<Level> GetLevels()
        {
            return new()
            {
                //Уровнь 1
                new(LevelType.Level1,
                new()
                {
                    //new DefaultRoom(LevelType.Level1, new (25,15), new (125,125),
                    //15, new ()
                    //{
                    //    new Rogue(1),
                    //    new Rogue(2),
                    //    new Rogue(3),
                    //}),
                    //new DefaultRoom(LevelType.Level1, new(25,20),new (255,125),
                    //5, new ()
                    //{
                    //    new Skeleton(),
                    //}),
                    new SpecialRoom(LevelType.Level1, new(400,120))
                }),

                //Уровнь 2
                new(LevelType.Level2,
                new()
                {
                    new DefaultRoom(LevelType.Level2, new(25,15), new(125,125),
                    25, new()
                    {
                        new Skeleton(),
                        new Demon()
                    }),
                    new DefaultRoom(LevelType.Level2, new(25,25), new(255,125),
                    25, new()
                    {
                        new Skeleton(),
                        new Demon()
                    }),
                    new SpecialRoom (LevelType.Level2, new(400,120))
                }),
            };
        }

    }
}
