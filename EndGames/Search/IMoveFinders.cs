namespace Phutball.Search
{
    public interface IMoveFinders
    {
        IMoveFindingStartegy DfsUnbounded();
        IMoveFindingStartegy BfsUnbounded();
        IMoveFindingStartegy DfsBounded();
        IMoveFindingStartegy DfsCuttoff();
        IMoveFindingStartegy DfsCuttoffToWhite();
        IMoveFindingStartegy OrderByNodesValuesWithCuttofsToWhite();
        IMoveFindingStartegy BfsCuttoffToWhite();
        IMoveFindingStartegy DfsNodesBounded();
        IMoveFindingStartegy BfsBounded();
        IMoveFindingStartegy AlphaBetaJumps();
        IMoveFindingStartegy AlphaBetaJumpsOrStay();
        IMoveFindingStartegy AlphaBeta();
        IMoveFindingStartegy SmartAlphaBeta();
        IMoveFindingStartegy BfsNodesBounded();
        IMoveFindingStartegy OrderByNodesValues();
        IMoveFindingStartegy SmartAlphaBetaJumpOrStay();
    }
}