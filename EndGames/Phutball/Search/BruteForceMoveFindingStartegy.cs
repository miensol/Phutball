using System;
using EndGames.Phutball.Moves;
using EndGames.Phutball.PlayerMoves;
using log4net;
namespace EndGames.Phutball.Search
{
    public class BruteForceMoveFindingStartegy : IMoveFindingStartegy
    {
        private static readonly ILog Logger = LogManager.GetLogger(typeof(BruteForceMoveFindingStartegy));

        private readonly ISearchNodeVisitor<JumpNode> _defaultNodeVistor;
        private readonly Func<ISearchNodeVisitor<JumpNode>, ITreeSearch<JumpNode>> _searchFactory;
        private readonly IPlayersState _playersState;
        private MovesFactory _movesFactory;

        public BruteForceMoveFindingStartegy(
            ISearchNodeVisitor<JumpNode> defaultNodeVistor, 
            Func<ISearchNodeVisitor<JumpNode>,ITreeSearch<JumpNode>> searchFactory,
            IPlayersState playersState, MovesFactory movesFactory)
        {
            _defaultNodeVistor = defaultNodeVistor;
            _movesFactory = movesFactory;
            _searchFactory = searchFactory;
            _playersState = playersState;
        }
        


        public IPhutballMove Search(IFieldsGraph fieldsGraph)
        {
            var graphCopy = (IFieldsGraph)fieldsGraph.Clone();
            var tree = _movesFactory.GetMovesTree(graphCopy);
            var targetBorder = _playersState.CurrentPlayer.GetTargetBorder(fieldsGraph);
            var bestValuePicker = new PickBestValueNodeVisitor(targetBorder, graphCopy, new PerformMoves(graphCopy, _playersState));
            var nodeCounter = new VisitedNodesCounter<JumpNode>();
            var search = _searchFactory(_defaultNodeVistor.FollowedBy(bestValuePicker).FollowedBy(nodeCounter));
            Logger.Debug("Started search");
            search.Run(tree);
            Logger.Debug("End search");
            Logger.DebugFormat("Node visited {0}", nodeCounter.Count);
            Logger.DebugFormat("Result move {0}", bestValuePicker.ResultMove);
            return bestValuePicker.ResultMove;
        }
    }
    public class MovesFactory
    {
        public RootedBySelectingWhiteFieldBoardJumpTree GetMovesTree(IFieldsGraph graphCopy)
        {
            return new RootedBySelectingWhiteFieldBoardJumpTree(graphCopy);
        }
    }
}