namespace EndGames.Phutball
{
    public interface IAlphaBetaOptions
    {
        int JumpsMaxDepth { get; set; }
        int StoneRadius { get; set; }
        int SearchDepth { get; set; }
    }

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
                           StoneRadius = 1
                       };
        }
    }
}