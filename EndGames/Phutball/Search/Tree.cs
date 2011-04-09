using System.Collections.Generic;

namespace EndGames.Phutball.Search
{
    public interface ITree<out TNode>
    {
        TNode Node { get; }
        IEnumerable<ITree<TNode>> Children { get; }
        ITree<TNode> Parent { get; }
        bool IsLeaf { get; }
    }
}