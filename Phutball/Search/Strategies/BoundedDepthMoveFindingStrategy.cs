using System;
using Phutball.Search.Visitors;

namespace Phutball.Search.Strategies
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

        public PhutballMoveScore Search(IFieldsGraph fieldsGraph)
        {
            return _bruteForceSearch.Search(fieldsGraph);
        }
    }
}