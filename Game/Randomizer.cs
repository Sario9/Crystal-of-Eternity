using System;
using System.Collections.Generic;

namespace Crystal_of_Eternity
{
    public static class Randomizer
    {
        public static Random Random;

        static Randomizer()
        {
            Random = new Random(DateTime.Now.Millisecond);
        }

        public static void ChangeSeed(int seed)
        {
            Random = new Random(seed);
        }

        public static int SelectRandomWithProbability(params double[] chances)
        {
            var elements = new List<KeyValuePair<int, double>>();
            for (int i = 0; i < chances.Length; i++)
            {
                elements.Add(new(i + 1, chances[i]));
            }

            var roll = Random.NextDouble();

            var cumulative = 0.0;
            for (int i = 0; i < elements.Count; i++)
            {
                cumulative += elements[i].Value;
                if (roll < cumulative)
                    return elements[i].Key;
            }
            return 0;
        }
    }
}
