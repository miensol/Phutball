namespace EndGames.Phutball.Search
{
    public interface IMoveFinders
    {
        IMoveFindingStartegy DfsUnbounded(IPlayersState playersState);
        IMoveFindingStartegy BfsUnbounded(IPlayersState playersState);
        IMoveFindingStartegy DfsBounded(IPlayersState playersState, int maxDepth);
        IMoveFindingStartegy BfsBounded(IPlayersState playersState, int bfsSearchDepth);
        IMoveFindingStartegy AlphaBeta(IPlayersState playersState, int alphaBetaSearchDepth);
    }
}