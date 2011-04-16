namespace EndGames.Phutball.Search
{
    public interface IMoveFinders
    {
        IMoveFindingStartegy DfsUnbounded();
        IMoveFindingStartegy BfsUnbounded();
        IMoveFindingStartegy DfsBounded();
        IMoveFindingStartegy BfsBounded();
        IMoveFindingStartegy AlphaBetaJumps();
        IMoveFindingStartegy AlphaBeta();
    }
}