namespace Phutball
{
    public interface IAlphaBetaOptions
    {
        int JumpsMaxDepth { get; set; }
        int StoneRadius { get; set; }
        int SearchDepth { get; set; }
        int SkipShortMoves { get; set; }
        int BlackStonesToBorderWeight { get; set; }
        int DistanceToBorderWeight { get; set; }
    }
}