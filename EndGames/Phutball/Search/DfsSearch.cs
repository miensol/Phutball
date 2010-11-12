namespace EndGames.Phutball.Search
{
    public interface IDfsSearchStartegy<in TNode>
    {
        void OnEnter(TNode node);
        void OnLeave(TNode node);
        bool ShouldStop(TNode node);
        bool ShouldEnterChildrenOf(TNode node);
    }

    public class DfsSearch<TNode>        
    {
        private readonly IDfsSearchStartegy<TNode> _searchStartegy;
        private bool _stopSearch;

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
            _searchStartegy.OnEnter(node);            
            
            _stopSearch = _searchStartegy.ShouldStop(node);
            
            if(_searchStartegy.ShouldEnterChildrenOf(node))
            {
                tree.Children.Each(subTree => Run(subTree));      
            }
            _searchStartegy.OnLeave(node);
        }
    }
}