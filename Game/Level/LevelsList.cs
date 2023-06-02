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
                LevelsParser.ParseFromFile("Level_1"),
                //Уровнь 2
                LevelsParser.ParseFromFile("Level_2")
            };
        }

    }
}
