using System;

namespace EndGames.Phutball
{
    [Serializable]
    public class AlphaBetaOptions : IAlphaBetaOptions
    {
        public int JumpsMaxDepth { get; set; }
        public int StoneRadius { get; set; }
        public int SearchDepth { get; set; }

        public static AlphaBetaOptions Defaults()
        {
            return new AlphaBetaOptions
                       {
                           SearchDepth = 6,
                           JumpsMaxDepth = 9,
                           StoneRadius = 1,
                           SkipShortMoves = 1,
                           DistanceToBorderWeight = 7,
                           BlackStonesToBorderWeight = 2
                       };
        }

        public IAlphaBetaOptions AllowNoMoveToBeTaken()
        {
            return new AlphaBetaOptions
                       {
                           SearchDepth = SearchDepth,
                           BlackStonesToBorderWeight = BlackStonesToBorderWeight,
                           DistanceToBorderWeight = DistanceToBorderWeight,
                           JumpsMaxDepth = JumpsMaxDepth,
                           SkipShortMoves = 0,
                           StoneRadius = StoneRadius
                       };
        }

        public int SkipShortMoves { get; set; }

        public int BlackStonesToBorderWeight { get; set; }
        public int DistanceToBorderWeight { get; set; }
    }
}