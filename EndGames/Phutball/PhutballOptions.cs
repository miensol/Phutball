using System;

namespace EndGames.Phutball
{
    public class PhutballOptions : IPhutballOptions
    {
        public PhutballOptions()
        {
            RowCount = 19;
            ColumnCount = 15;
            DfsSearchDepth = 10;
            BfsSearchDepth = 10;
            AlphaBeta = AlphaBetaOptions.Defaults();       
        }

        public AlphaBetaOptions AlphaBeta { get; set; }

        public int RowCount { get; set; }

        public int ColumnCount { get; set; }

        public int DfsSearchDepth { get; set; }
        public int BfsSearchDepth { get; set; }
    }
}