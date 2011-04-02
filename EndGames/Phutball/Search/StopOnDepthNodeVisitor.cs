namespace EndGames.Phutball.Search
{
    public class StopOnDepthNodeVisitor<TNode> : ISearchNodeVisitor<TNode>
    {
        private readonly int _maxDepth;
        private DepthCounterNodeVisitor<TNode> _depthCounter;

        public StopOnDepthNodeVisitor(int maxDepth)
        {
            _maxDepth = maxDepth;
            _depthCounter = new DepthCounterNodeVisitor<TNode>();
        }

        public void OnEnter(ITree<TNode> node, ITreeSearchContinuation treeSearchContinuation)
        {
            _depthCounter.OnEnter(node, treeSearchContinuation);
            if (_depthCounter.CurrentDepth >= _maxDepth)
            {
                treeSearchContinuation.DontEnterChildren();
            }
        }

        public void OnLeave(ITree<TNode> node, ITreeSearchContinuation treeSearchContinuation)
        {
            _depthCounter.OnLeave(node, treeSearchContinuation);
        }
    }
}