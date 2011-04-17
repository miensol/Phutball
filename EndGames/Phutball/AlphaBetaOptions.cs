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
                           SearchDepth = 5,
                           JumpsMaxDepth = 8,
                           StoneRadius = 1,
                           SkipShortMoves = 1,
                           DistanceToBorderWeight = 1
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