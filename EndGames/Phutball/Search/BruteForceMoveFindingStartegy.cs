using EndGames.Phutball.Moves;
using EndGames.Phutball.Search.BoardValues;
using EndGames.Utils;

namespace EndGames.Phutball.Search
{
    public class BruteForceMoveFindingStartegy : IMoveFindingStartegy
    {
        private readonly TargetBorder _targetBorder;
        private readonly ISearchNodeVisitor<JumpNode> _defaultNodeVistor;

        public BruteForceMoveFindingStartegy(TargetBorder targetBorder):this(targetBorder, new EmptyNodeVisitor<JumpNode>())
        {
        }

        public BruteForceMoveFindingStartegy(TargetBorder targetBorder, ISearchNodeVisitor<JumpNode> defaultNodeVistor)
        {
            _targetBorder = targetBorder;
            _defaultNodeVistor = defaultNodeVistor;
        }


        public IMove<IFieldsGraph> Search(IFieldsGraph fieldsGraph)
        {
            var graphCopy = (IFieldsGraph)fieldsGraph.Clone();
            var bestValuePicker = new PickBestValueNodeVisitor(_targetBorder, graphCopy);
            var tree = new RootedBySelectingWhiteFieldBoardJumpTree(graphCopy);
            var nodeCounter = new VisitedNodesCounter<JumpNode>();
            var search = new DfsSearch<JumpNode>(_defaultNodeVistor.FollowedBy(bestValuePicker).FollowedBy(nodeCounter));
            Log.Current.Debug("Started search");
            search.Run(tree);
            Log.Current.Debug("End search");
            Log.Current.Debug("Node visited {0}".ToFormat(nodeCounter.Count));
            Log.Current.Debug("Result move {0}".ToFormat(bestValuePicker.ResultMove));
            return bestValuePicker.ResultMove;
        }
    }
}