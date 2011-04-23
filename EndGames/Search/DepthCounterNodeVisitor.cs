using System;

namespace Phutball.Search
{
    public class DepthCounterNodeVisitor<TNode> : ISearchNodeVisitor<TNode>
    {
        public void OnEnter(ITree<TNode> node, ITreeSearchContinuation treeSearchContinuation)
        {
            CurrentDepth++;
            MaxDepth = Math.Max(CurrentDepth, MaxDepth);
        }

        public int CurrentDepth { get; set; }
        public int MaxDepth { get; set; }
        
        public void OnLeave(ITree<TNode> node, ITreeSearchContinuation treeSearchContinuation)
        {
            CurrentDepth--;
        }
    }
}