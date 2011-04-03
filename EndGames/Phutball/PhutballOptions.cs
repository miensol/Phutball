using System;

namespace EndGames.Phutball
{
    public class PhutballOptions : IPhutballOptions
    {
        public PhutballOptions()
        {
            RowCount = 19;
            ColumnCount = 15;
            DfsSearchDepth = 50;
        }

        public int RowCount { get; set; }

        public int ColumnCount { get; set; }

        public int DfsSearchDepth { get; set; }
    }
}