using System;
using EndGames.Phutball.Moves;
using EndGames.Phutball.PlayerMoves;
using EndGames.Utils;

namespace EndGames.Phutball.Search
{
    public class BruteForceMoveFindingStartegy : IMoveFindingStartegy
    {
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
            Log.Current.Debug("Started search");
            search.Run(tree);
            Log.Current.Debug("End search");
            Log.Current.Debug("Node visited {0}".ToFormat(nodeCounter.Count));
            Log.Current.Debug("Result move {0}".ToFormat(bestValuePicker.ResultMove));
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