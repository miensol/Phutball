namespace EndGames.Phutball.Search
{
    public class VisitedNodesCounter<TNode> : ISearchNodeVisitor<TNode>
    {
        private int _counter;

        public int Count
        {
            get { return _counter; }
        }

        public void OnEnter(TNode node, ITreeSearchContinuation treeSearchContinuation)
        {
            ++_counter;
        }

        public void OnLeave(TNode node, ITreeSearchContinuation treeSearchContinuation)
        {            
        }
    }
}