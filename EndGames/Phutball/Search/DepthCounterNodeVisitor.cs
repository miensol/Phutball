namespace EndGames.Phutball.Search
{
    public class DepthCounterNodeVisitor<TNode> : ISearchNodeVisitor<TNode>
    {
        public void OnEnter(ITree<TNode> node, ITreeSearchContinuation treeSearchContinuation)
        {
            CurrentDepth++;
        }

        public int CurrentDepth { get; set; }

        public void OnLeave(ITree<TNode> node, ITreeSearchContinuation treeSearchContinuation)
        {
            CurrentDepth--;
        }
    }
}