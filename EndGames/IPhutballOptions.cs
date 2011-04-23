namespace Phutball
{
    public interface IPhutballOptions
    {
        int RowCount { get; set; }
        int ColumnCount { get; set; }
        int DfsSearchDepth { get; set; }
        int BfsSearchDepth { get; set; }
        AlphaBetaOptions AlphaBeta { get; set; }
        int DfsMaxVistedNodes { get; set; }
        int BfsMaxVisitedNodes { get; set; }
    }
}