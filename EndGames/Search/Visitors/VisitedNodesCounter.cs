namespace Phutball.Search.Visitors
{
    public class VisitedNodesCounter<TNode> : ISearchNodeVisitor<TNode>
    {
        private int _counter;

        public int Count
        {
            get { return _counter; }
        }

        public void OnEnter(ITree<TNode> node, ITreeSearchContinuation treeSearchContinuation)
        {
            ++_counter;
        }

        public void OnLeave(ITree<TNode> node, ITreeSearchContinuation treeSearchContinuation)
        {            
        }
    }
}