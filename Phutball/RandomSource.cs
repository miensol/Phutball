using System;

namespace Phutball
{
    public static class RandomSource
    {
        private static int _seed = 3;
        private static Random rng = new Random(_seed);

        public static int Next()
        {
            return rng.Next();
        }

        public static int Next(int lowerInclusive, int upperExclusive)
        {
            return rng.Next(lowerInclusive, upperExclusive);
        }

        public static double NextDouble()
        {
            return rng.NextDouble();
        }

        public static int Next(int max)
        {
            return rng.Next(max);
        }

        public static void Reset()
        {
            rng = new Random(_seed);
        }
    }
}