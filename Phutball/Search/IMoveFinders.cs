namespace Phutball.Search
{
    public interface IMoveFinders
    {
        [StrategyDesctiption("Make a jump using dfs with unbounded search detph")]
        IMoveFindingStartegy DfsUnbounded();
        [StrategyDesctiption("Make a jump using bfs with unbounded search depth")]
        IMoveFindingStartegy BfsUnbounded();
        [StrategyDesctiption("Maka a jump using dfs with maximum search depth given in options")]
        IMoveFindingStartegy DfsBounded();
        [StrategyDesctiption(@"Make a jump using dfs. The search will stop when there is no way to win in current move or the best
jump found already cannot be improved. The maximum number of visited nodes is configured in options")]        
        IMoveFindingStartegy DfsCuttoff();
        [StrategyDesctiption(@"Make a jump using dfs. The search will stop when there is no way to improve the best jump found already.
The maximum number of visited nodes is configured in options")]
        IMoveFindingStartegy DfsCuttoffToWhite();
        [StrategyDesctiption(@"Make a jump using search that firstly visits nodes that improve positions the most and 
stops when there is no way to improve the best jump found already. The maximum number of visited nodes is configured in options")]
        IMoveFindingStartegy OrderByNodesValuesWithCuttofsToWhite();
        [StrategyDesctiption(@"Make a jump using bfs. The search will stop when there is no way to improve the best jump found already.
The maximum number of visited nodes is configured in options")]
        IMoveFindingStartegy BfsCuttoffToWhite();
        [StrategyDesctiption(@"Make a jump using dfs with a limit on the number of nodes visited that is given in options")]
        IMoveFindingStartegy DfsNodesBounded();
        [StrategyDesctiption(@"Make a jump using bfs with maximum search depth given in options")]
        IMoveFindingStartegy BfsBounded();
        [StrategyDesctiption(@"Make a jump using alfa-beta search. The mini-max tree depth and the maximum depth of a single jump is configured in options")]
        IMoveFindingStartegy AlphaBetaJumps();
        [StrategyDesctiption(@"Make a jump using alpha-beta search or don't move the ball if you can't improve position. 
The mini-max tree depth and the maximum depth of a single jump is configured in options")]
        IMoveFindingStartegy AlphaBetaJumpsOrStay();
        [StrategyDesctiption(@"Make a move using alpha-beta search. The mini-max tree depth, the maximum depth of a single jump 
and the maximum distance between the ball and an intersection where a player can be placed is configured in options")]
        IMoveFindingStartegy AlphaBeta();
        [StrategyDesctiption(@"Make a move using alpha-beta search. The mini-max tree depth and the maximum depth of a single jump are given in options.
Black stones will be placed in the neighbourhood of possible ball positions toward the target goal line")]
        IMoveFindingStartegy SmartAlphaBeta();
        [StrategyDesctiption(@"Make a jump using bfs search with a limit on the number of visited nodes that is given in options")]
        IMoveFindingStartegy BfsNodesBounded();
        [StrategyDesctiption(@"Make a jump using search that firstly visited nodes that improve position the most. The maximum number of visited nodes is given in options")]
        IMoveFindingStartegy OrderByNodesValues();

        [StrategyDesctiption(@"Make a jump using alpha-beta search or don't move the ball if you can't improve position. 
The mini-max tree depth and the maximum depth of a single jump are given in options.
Black stones will be placed in the neighbourhood of possible ball positions toward the target goal line")]
        IMoveFindingStartegy SmartAlphaBetaJumpOrStay();
    }
}