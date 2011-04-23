using System;

namespace Phutball.Search
{
    public class CuttofsMoveFindingStrategy : IMoveFindingStartegy
    {
        private readonly ISearchNodeVisitor<JumpNode> _defaultNodeVistor;
        private readonly Func<ISearchNodeVisitor<JumpNode>, ITreeSearch<JumpNode>> _searchFactory;
        private readonly IPlayersState _playersState;
        private MovesFactory _movesFactory;

        public CuttofsMoveFindingStrategy(
            ISearchNodeVisitor<JumpNode> defaultNodeVistor, 
            Func<ISearchNodeVisitor<JumpNode>,ITreeSearch<JumpNode>> searchFactory,
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
            var depthCounter = new DepthCounterNodeVisitor<JumpNode>();
            var stopOnMaxNodesVisited = new StopOnVisitedNodesCount<JumpNode>(MaxVisitedNodes);
            var searchNodeVisitor = stopOnMaxNodesVisited
                .FollowedBy(cuttoffVisitor)
                .FollowedBy(depthCounter)
                .FollowedBy(_defaultNodeVistor);
            var search = _searchFactory(searchNodeVisitor);
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