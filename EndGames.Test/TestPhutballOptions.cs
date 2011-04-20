namespace Phutball.Tests
{
    public class TestPhutballOptions : IPhutballOptions
    {
        public int RowCount { get; set; }

        public int ColumnCount { get; set; }

        public int DfsSearchDepth { get; set; }
        public int BfsSearchDepth { get; set; }

        public AlphaBetaOptions AlphaBeta { get; set; }
    }
}