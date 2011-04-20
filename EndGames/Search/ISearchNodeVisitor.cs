using Phutball.PlayerMoves;

namespace Phutball.Search
{
    public interface ISearchNodeVisitor<in TNode>
    {
        void OnEnter(ITree<TNode> node, ITreeSearchContinuation treeSearchContinuation);
        void OnLeave(ITree<TNode> node, ITreeSearchContinuation treeSearchContinuation);        
    }

    public class PerformMovesNodeVisitor : ISearchNodeVisitor<JumpNode>
    {
        private IPerformMoves _performMoves;

        public PerformMovesNodeVisitor(IPerformMoves performMoves)
        {
            _performMoves = performMoves;
        }

        public void OnEnter(ITree<JumpNode> node, ITreeSearchContinuation treeSearchContinuation)
        {
            _performMoves.Perform(node.Node.LastMove);
        }

        public void OnLeave(ITree<JumpNode> node, ITreeSearchContinuation treeSearchContinuation)
        {
            _performMoves.Undo(node.Node.LastMove);
        }
    }
}