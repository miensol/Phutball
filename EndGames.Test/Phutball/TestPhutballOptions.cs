using EndGames.Phutball;

namespace EndGames.Tests.Phutball
{
    public class TestPhutballOptions : IPhutballOptions
    {
        public int RowCount { get; set; }

        public int ColumnCount { get; set; }

        public int DfsSearchDepth { get; set; }
    }
}