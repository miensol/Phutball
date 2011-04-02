namespace EndGames.Phutball
{
    public class PhutballOptions : IPhutballOptions
    {
        public PhutballOptions()
        {
            RowCount = 19;
            ColumnCount = 15;
        }

        public int RowCount { get; set; }

        public int ColumnCount { get; set; }
    }
}