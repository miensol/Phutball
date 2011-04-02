namespace EndGames.Phutball.Search
{
    public class EmptyNodeVisitor<TNode> : ISearchNodeVisitor<TNode>
    {
        public void OnEnter(ITree<TNode> node, ITreeSearchContinuation treeSearchContinuation)
        {
        }

        public void OnLeave(ITree<TNode> node, ITreeSearchContinuation treeSearchContinuation)
        {
        }
    }
}