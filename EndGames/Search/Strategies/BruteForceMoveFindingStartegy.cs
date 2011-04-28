using System;
using Phutball.PlayerMoves;
using Phutball.Search.BoardValues;
using Phutball.Search.Visitors;

namespace Phutball.Search.Strategies
{
    public class BruteForceMoveFindingStartegy : IMoveFindingStartegy
    {
        private readonly ISearchNodeVisitor<JumpNode> _defaultNodeVistor;
        private readonly Func<ISearchNodeVisitor<JumpNode>, IPerformMoves, TargetBorder, ITreeSearch<JumpNode>> _searchFactory;
        private readonly IPlayersState _playersState;
        private MovesFactory _movesFactory;

        public BruteForceMoveFindingStartegy(
            ISearchNodeVisitor<JumpNode> defaultNodeVistor, 
            Func<ISearchNodeVisitor<JumpNode>,ITreeSearch<JumpNode>> searchFactory,
            IPlayersState playersState, 
            MovesFactory movesFactory)
        {
            _defaultNodeVistor = defaultNodeVistor;
            _movesFactory = movesFactory;
            _searchFactory = (visotors,perfomer, target) => searchFactory(visotors);
            _playersState = playersState;
        }

        public BruteForceMoveFindingStartegy(ISearchNodeVisitor<JumpNode> defaultNodeVistor, 
            Func<ISearchNodeVisitor<JumpNode>, IPerformMoves, TargetBorder,ITreeSearch<JumpNode>> searchFactory,
            IPlayersState playersState, 
            MovesFactory movesFactory)
        {
            _defaultNodeVistor = defaultNodeVistor;
            _movesFactory = movesFactory;
            _searchFactory = searchFactory;
            _playersState = playersState;
        }
        


        public PhutballMoveScore Search(IFieldsGraph fieldsGraph)
        {
            var graphCopy = (IFieldsGraph)fieldsGraph.Clone();
            var tree = _movesFactory.GetMovesTree(graphCopy);
            var targetBorder = _playersState.CurrentPlayer.GetTargetBorder(fieldsGraph);
            var performMoves = new PerformMoves(graphCopy, _playersState);
            var bestValuePicker = new PickBestValueNodeVisitor(targetBorder, graphCopy, performMoves);
            var nodeCounter = new VisitedNodesCounter<JumpNode>();
            var depthCounter = new DepthCounterNodeVisitor<JumpNode>();
            var searchNodeVisitor = _defaultNodeVistor.FollowedBy(bestValuePicker).FollowedBy(nodeCounter).FollowedBy(depthCounter);
            var search = _searchFactory(searchNodeVisitor, performMoves, targetBorder);
            search.Run(tree);
            return new PhutballMoveScore( bestValuePicker.ResultMove, bestValuePicker.CurrentMaxValue)
                       {
                           VisitedNodesCount = nodeCounter.Count,
                           MaxDepth = depthCounter.MaxDepth
                       };
        }
    }

 

}