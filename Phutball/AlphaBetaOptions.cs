using System;

namespace Phutball
{
    [Serializable]
    public class AlphaBetaOptions : IAlphaBetaOptions
    {
        public int JumpsMaxDepth { get; set; }
        public int StoneRadius { get; set; }
        public int SearchDepth { get; set; }
        public int SmartSearchDepth { get; set; }

        public static AlphaBetaOptions Defaults()
        {
            return new AlphaBetaOptions
                       {
                           SearchDepth = 6,
                           SmartSearchDepth = 6,
                           JumpsMaxDepth = 9,
                           StoneRadius = 1,
                           SkipShortMoves = 1,
                           DistanceToBorderWeight = 7,
                           BlackStonesToBorderWeight = 2
                       };
        }

        public AlphaBetaOptions AllowNoMoveToBeTaken()
        {
            return new AlphaBetaOptions
                       {
                           SearchDepth = SearchDepth,
                           SmartSearchDepth = SmartSearchDepth,
                           BlackStonesToBorderWeight = BlackStonesToBorderWeight,
                           DistanceToBorderWeight = DistanceToBorderWeight,
                           JumpsMaxDepth = JumpsMaxDepth,
                           SkipShortMoves = 0,
                           StoneRadius = StoneRadius
                       };
        }

        public IAlphaBetaOptions HalfDepth()
        {
            return new AlphaBetaOptions
            {
                SearchDepth = SearchDepth / 2,
                SmartSearchDepth = SmartSearchDepth/2,
                BlackStonesToBorderWeight = BlackStonesToBorderWeight,
                DistanceToBorderWeight = DistanceToBorderWeight,
                JumpsMaxDepth = JumpsMaxDepth,
                SkipShortMoves = SkipShortMoves,
                StoneRadius = StoneRadius
            };
        }

        public int SkipShortMoves { get; set; }

        public int BlackStonesToBorderWeight { get; set; }
        public int DistanceToBorderWeight { get; set; }

        public IAlphaBetaOptions UseSmartSearchDepth()
        {
            return new AlphaBetaOptions
                       {
                           SearchDepth = SmartSearchDepth,
                           SmartSearchDepth = SmartSearchDepth,
                           BlackStonesToBorderWeight = BlackStonesToBorderWeight,
                           DistanceToBorderWeight = DistanceToBorderWeight,
                           JumpsMaxDepth = JumpsMaxDepth,
                           SkipShortMoves = 1,
                           StoneRadius = StoneRadius
                       };
        }

        public AlphaBetaOptions SetToBegginer()
        {
            SearchDepth = 4;
            SmartSearchDepth = 0;
            BlackStonesToBorderWeight = 1;
            DistanceToBorderWeight = 1;
            JumpsMaxDepth = 5;
            SkipShortMoves = 1;
            StoneRadius = 1;
            return this;
        }

        public AlphaBetaOptions SetToLearner()
        {
            SearchDepth = 4;
            SmartSearchDepth = 4;
            BlackStonesToBorderWeight = 2;
            DistanceToBorderWeight = 7;
            JumpsMaxDepth = 6;
            SkipShortMoves = 1;
            StoneRadius = 1;
            return this;
        }

        public AlphaBetaOptions SetToAdvanced()
        {
            SearchDepth = 5;
            SmartSearchDepth = 7;
            BlackStonesToBorderWeight = 2;
            DistanceToBorderWeight = 7;
            JumpsMaxDepth = 10;
            SkipShortMoves = 1;
            StoneRadius = 2;
            return this;
        }
    }
}