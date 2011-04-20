namespace Phutball.Search
{
    public class DfsSearch<TNode> : ITreeSearchContinuation, ITreeSearch<TNode>
    {
        private readonly ISearchNodeVisitor<TNode> _nodeVisitor;
        private bool _stopSearch;
        private bool _dontEnterChildren;
        private bool _dontEnterNeighbours;

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
            _nodeVisitor.OnEnter(tree, this);            
            
            if(false == _stopSearch && false == _dontEnterChildren)
            {
                foreach (var child in tree.Children)
                {
                    if(_dontEnterNeighbours)
                    {
                        break;
                    }
                    Run(child);
                }
                _dontEnterNeighbours = false;
            }
            _nodeVisitor.OnLeave(tree, this);
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

        public void DontEnterNeighbours()
        {
            _dontEnterNeighbours = true;
        }
    }
}