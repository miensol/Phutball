using System;

namespace EndGames.Phutball.Search
{
    public class DfsSearch<TNode> : ITreeSearchContinuation, ITreeSearch<TNode>
    {
        private readonly ISearchNodeVisitor<TNode> _nodeVisitor;
        private bool _stopSearch;
        private bool _dontEnterChildren;

        public DfsSearch(ISearchNodeVisitor<TNode> nodeVisitor)
        {
            _nodeVisitor = nodeVisitor;
        }

        public void Run<TTree>(TTree tree)
            where TTree : ITree<TNode>
        {
            if(_stopSearch)
            {
                return;
            }
            ContinueSearch(tree);
        }

        private void ContinueSearch<TTree>(TTree tree)
            where TTree : ITree<TNode>
        {
            var node = tree.Node;
            _nodeVisitor.OnEnter(node, this);            
            
            if(false == _stopSearch && false == _dontEnterChildren)
            {                
                tree.Children.Each(subTree => Run(subTree));      
            }
            _nodeVisitor.OnLeave(node, this);
            _dontEnterChildren = false;
        }

        public void Stop()
        {
            _stopSearch = true;
        }

        public void DontEnterChildren()
        {
            _dontEnterChildren = true;
        }
    }
}