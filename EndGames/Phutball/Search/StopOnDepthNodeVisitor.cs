namespace EndGames.Phutball.Search
{
    public class StopOnDepthNodeVisitor<TNode> : ISearchNodeVisitor<TNode>
    {
        private readonly int _maxDepth;
        private int _currentDepth;

        public StopOnDepthNodeVisitor(int maxDepth)
        {
            _maxDepth = maxDepth;
        }

        public void OnEnter(TNode node, ITreeSearchContinuation treeSearchContinuation)
        {
            _currentDepth++;
            if(_currentDepth >= _maxDepth)
            {
                treeSearchContinuation.DontEnterChildren();
            }
        }

        public void OnLeave(TNode node, ITreeSearchContinuation treeSearchContinuation)
        {
            _currentDepth--;
        }
    }
}