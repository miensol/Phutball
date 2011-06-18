using System;
using Phutball.PlayerMoves;
using Phutball.Search.BoardValues;
using Phutball.Search.Visitors;

namespace Phutball.Search.Strategies
{
    public class CuttoffsMoveFindingStrategy : IMoveFindingStartegy
    {
        private readonly ISearchNodeVisitor<JumpNode> _defaultNodeVistor;
        private readonly Func<ISearchNodeVisitor<JumpNode>, IPerformMoves, TargetBorder, ITreeSearch<JumpNode>> _searchFactory;
        private readonly IPlayersState _playersState;
        private MovesFactory _movesFactory;

        public CuttoffsMoveFindingStrategy(
            ISearchNodeVisitor<JumpNode> defaultNodeVistor, 
            Func<ISearchNodeVisitor<JumpNode>,ITreeSearch<JumpNode>> searchFactory,
            IPlayersState playersState, 
            MovesFactory movesFactory)
        {
            _defaultNodeVistor = defaultNodeVistor;
            _movesFactory = movesFactory;
            _searchFactory = (vistor,perform, target) => searchFactory(vistor);
            _playersState = playersState;
            MaxVisitedNodes = int.MaxValue;
        }
        
        public CuttoffsMoveFindingStrategy(
            ISearchNodeVisitor<JumpNode> defaultNodeVistor, 
            Func<ISearchNodeVisitor<JumpNode>, IPerformMoves, TargetBorder,ITreeSearch<JumpNode>> searchFactory,
            IPlayersState playersState, 
            MovesFactory movesFactory)
        {
            _defaultNodeVistor = defaultNodeVistor;
            _movesFactory = movesFactory;
            _searchFactory = searchFactory;
            _playersState = playersState;
            MaxVisitedNodes = int.MaxValue;
        }

        public bool CuttoffToTarget { get; set; }
        public int MaxVisitedNodes { get; set; }

        public PhutballMoveScore Search(IFieldsGraph fieldsGraph)
        {
            var graphCopy = (IFieldsGraph)fieldsGraph.Clone();
            var tree = _movesFactory.GetMovesTree(graphCopy);
            var targetBorder = _playersState.CurrentPlayer.GetTargetBorder(fieldsGraph);
            
            var cuttoffVisitor = new CuttoffPickBestValueNodeVisitor(targetBorder, graphCopy, _playersState)
                                     {
                                         CuttoffToTargetBorder = CuttoffToTarget
                                     };
            var performMoves = cuttoffVisitor.MovesPerformer;
            var depthCounter = new DepthCounterNodeVisitor<JumpNode>();
            var stopOnMaxNodesVisited = new StopOnVisitedNodesCount<JumpNode>(MaxVisitedNodes);
            var searchNodeVisitor = stopOnMaxNodesVisited
                .FollowedBy(cuttoffVisitor)
                .FollowedBy(depthCounter)
                .FollowedBy(_defaultNodeVistor);
            var search = _searchFactory(searchNodeVisitor, performMoves, targetBorder);
            search.Run(tree);
            return new PhutballMoveScore(cuttoffVisitor.PickBestValue.ResultMove, cuttoffVisitor.PickBestValue.CurrentMaxValue)
                       {
                           VisitedNodesCount = stopOnMaxNodesVisited.VistedNodesCount,
                           MaxDepth = depthCounter.MaxDepth,
                           CuttoffsCount = cuttoffVisitor.CuttoffsCount
                       };
        }
    }
}