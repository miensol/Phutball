using System;

namespace EndGames.Phutball.Jumpers
{
    public class StoneMover
    {
        private readonly Tuple<int, int> _delta;

        public StoneMover(Tuple<int, int> delta)
        {
            _delta = delta;
        }

        public Tuple<int, int> Next(Tuple<int, int> current)
        {
            return new Tuple<int, int>(current.Item1 + _delta.Item1, current.Item2 + _delta.Item2);
        }
    }
}