namespace Phutball.Search.Visitors
{
    public class StopOnVisitedNodesCount<T> : ISearchNodeVisitor<JumpNode>
    {
        private readonly int _maxVisitedNodex;
        public int VistedNodesCount { get; set; }

        public StopOnVisitedNodesCount(int maxVisitedNodex)
        {
            _maxVisitedNodex = maxVisitedNodex;
        }

        public void OnEnter(ITree<JumpNode> node, ITreeSearchContinuation treeSearchContinuation)
        {
            VistedNodesCount++;
            if(VistedNodesCount > _maxVisitedNodex)
            {
                treeSearchContinuation.Stop();
            }
        }

        public void OnLeave(ITree<JumpNode> node, ITreeSearchContinuation treeSearchContinuation)
        {            
        }
    }
}