namespace EndGames.Phutball.Search
{
    public class CompisiteSearchNodeVisitor<TNode> : ISearchNodeVisitor<TNode>
    {
        private readonly ISearchNodeVisitor<TNode> _first;
        private readonly ISearchNodeVisitor<TNode> _second;

        public CompisiteSearchNodeVisitor(ISearchNodeVisitor<TNode> first, ISearchNodeVisitor<TNode> second)
        {
            _first = first;
            _second = second;
        }

        public void OnEnter(ITree<TNode> node, ITreeSearchContinuation treeSearchContinuation)
        {
            _first.OnEnter(node, treeSearchContinuation);
            _second.OnEnter(node, treeSearchContinuation);
        }

        public void OnLeave(ITree<TNode> node, ITreeSearchContinuation treeSearchContinuation)
        {
            _second.OnLeave(node, treeSearchContinuation);
            _first.OnLeave(node, treeSearchContinuation);
        }
    }
}