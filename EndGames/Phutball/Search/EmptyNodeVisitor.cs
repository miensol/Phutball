namespace EndGames.Phutball.Search
{
    public class EmptyNodeVisitor<TNode> : ISearchNodeVisitor<TNode>
    {
        public void OnEnter(TNode node, ITreeSearchContinuation treeSearchContinuation)
        {
        }

        public void OnLeave(TNode node, ITreeSearchContinuation treeSearchContinuation)
        {
        }
    }
}