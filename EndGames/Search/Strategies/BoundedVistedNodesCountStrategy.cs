using System;
using Phutball.PlayerMoves;
using Phutball.Search.BoardValues;
using Phutball.Search.Visitors;

namespace Phutball.Search.Strategies
{
    public class BoundedVistedNodesCountStrategy : IMoveFindingStartegy
    {
        private BruteForceMoveFindingStartegy _bruteForceSearch;

        public BoundedVistedNodesCountStrategy(IPlayersState playersState, int maxVistedNOdesCount, MovesFactory movesFactory, Func<ISearchNodeVisitor<JumpNode>, ITreeSearch<JumpNode>> searchFactory)
        {
            _bruteForceSearch = new BruteForceMoveFindingStartegy(new StopOnVisitedNodesCount<JumpNode>(maxVistedNOdesCount),
                                                                  searchFactory,
                                                                  playersState,
                                                                  movesFactory);
        }

        public BoundedVistedNodesCountStrategy(
            IPlayersState playersState, 
            int maxVistedNOdesCount, 
            MovesFactory movesFactory, 
            Func<ISearchNodeVisitor<JumpNode>, IPerformMoves, TargetBorder, ITreeSearch<JumpNode>> searchFactory)
        {
            _bruteForceSearch = new BruteForceMoveFindingStartegy(new StopOnVisitedNodesCount<JumpNode>(maxVistedNOdesCount),
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