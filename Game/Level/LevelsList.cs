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
                    new DefaultRoom(LevelType.Level1, new (15,15), new (125,125),
                    2, new ()
                    {
                        new Skeleton(),
                        new Rogue(1),
                        new Rogue(2),
                    }),
                    new DefaultRoom(LevelType.Level1, new(15,15),new (255,125),
                    30, new ()
                    {
                        new Skeleton(),
                    }),
                }),

                //Уровнь 2
                new(LevelType.Level2,
                new()
                {
                    new DefaultRoom(LevelType.Level2, new(15,15), new(125,125),
                    30, new()
                    {
                        new Skeleton(),
                    }),
                    new DefaultRoom(LevelType.Level2, new(15,15), new(255,125),
                    35, new()
                    {
                        new Skeleton(),
                        new Rogue(1),
                        new Rogue(2),
                    }),
                }),
            };
        }

    }
}
