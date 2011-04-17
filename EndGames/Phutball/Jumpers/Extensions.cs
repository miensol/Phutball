using System;
using System.Collections.Generic;
using System.Linq;

namespace EndGames.Phutball.Jumpers
{
    public static class Extensions
    {
        private static readonly Random rng = new Random();

        public static Tuple<int,int> Multiply(this Tuple<int,int> current, int mul)
        {
            return Tuple.Create(current.Item1*mul, current.Item2*mul);
        }

        public static IEnumerable<T> Shuffle<T>(this IEnumerable<T> sequence)
        {
            var all = sequence.ToList();
            all.Shuffle();
            return all;
        }

        private static void Shuffle<T>(this IList<T> list)
        {
            int n = list.Count;
            while (n > 1)
            {
                n--;
                int k = rng.Next(n + 1);
                T value = list[k];
                list[k] = list[n];
                list[n] = value;
            }
        }
    }
}