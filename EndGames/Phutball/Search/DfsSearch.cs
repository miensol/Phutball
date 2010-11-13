using System;

namespace EndGames.Phutball.Search
{
    public interface IDfsSearchStartegy<in TNode>
    {
        void OnEnter(TNode node, IDfsContinuation dfsContinuation);
        void OnLeave(TNode node, IDfsContinuation dfsContinuation);        
    }

    public interface IDfsContinuation
    {
        void Stop();
        void DontEnterChildren();
    }

    public class DfsSearch<TNode> : IDfsContinuation
    {
        private readonly IDfsSearchStartegy<TNode> _searchStartegy;
        private bool _stopSearch;
        private bool _dontEnterChildren;

        public DfsSearch(IDfsSearchStartegy<TNode> searchStartegy)
        {
            _searchStartegy = searchStartegy;
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
            _searchStartegy.OnEnter(node, this);            
            
            if(false == _stopSearch && false == _dontEnterChildren)
            {                
                tree.Children.Each(subTree => Run(subTree));      
            }
            _searchStartegy.OnLeave(node, this);
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