﻿using System;

namespace EndGames.Phutball
{
    [Serializable]
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

        public void Update(IPhutballOptions options)
        {
            RowCount = options.RowCount;
            ColumnCount = options.ColumnCount;
            DfsSearchDepth = options.DfsSearchDepth;
            BfsSearchDepth = options.BfsSearchDepth;
            AlphaBeta = options.AlphaBeta;
        }
    }
}