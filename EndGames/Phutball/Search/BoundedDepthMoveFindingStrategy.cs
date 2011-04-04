using System;
using EndGames.Phutball.Moves;
namespace EndGames.Phutball.Search
{
    public class BoundedDepthMoveFindingStrategy : IMoveFindingStartegy
    {
        private readonly BruteForceMoveFindingStartegy _bruteForceSearch;

        public BoundedDepthMoveFindingStrategy(IPlayersState playersState, int maxDepth, MovesFactory movesFactory, Func<ISearchNodeVisitor<JumpNode>, ITreeSearch<JumpNode>> searchFactory)
        {
            _bruteForceSearch = new BruteForceMoveFindingStartegy(new StopOnDepthNodeVisitor<JumpNode>(maxDepth), 
                searchFactory,
                playersState,
                movesFactory);
        }

        public IPhutballMove Search(IFieldsGraph fieldsGraph)
        {
            return _bruteForceSearch.Search(fieldsGraph);
        }
    }
}