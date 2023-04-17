using System;
using System.Collections.Generic;
using System.Linq;

namespace Crystal_of_Eternity
{
    public static class Randomizer
    {
        public static Random Random { get; private set; }

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
            if (chances.Sum() != 1)
                throw new ArgumentException("Сумма вероятностей должа быть равна 1");
            for (int i = 0; i < chances.Length; i++)
                elements.Add(new(i, chances[i]));

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
