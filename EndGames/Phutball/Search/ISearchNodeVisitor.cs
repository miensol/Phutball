namespace EndGames.Phutball.Search
{
    public interface ISearchNodeVisitor<in TNode>
    {
        void OnEnter(TNode node, ITreeSearchContinuation treeSearchContinuation);
        void OnLeave(TNode node, ITreeSearchContinuation treeSearchContinuation);        
    }
}