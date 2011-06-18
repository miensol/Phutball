namespace Phutball.Search
{
    public interface ISearchNodeVisitor<in TNode>
    {
        void OnEnter(ITree<TNode> node, ITreeSearchContinuation treeSearchContinuation);
        void OnLeave(ITree<TNode> node, ITreeSearchContinuation treeSearchContinuation);        
    }
}