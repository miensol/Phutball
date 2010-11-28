namespace EndGames.Phutball.Search
{
    public interface ITreeSearch<in TNode>
    {
        void Run<TTree>(TTree tree)
            where TTree : ITree<TNode>;
    }
}