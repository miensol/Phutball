namespace EndGames.Phutball
{
    public interface IPhutballOptions
    {
        int RowCount { get; set; }
        int ColumnCount { get; set; }
        int DfsSearchDepth { get; set; }
    }
}