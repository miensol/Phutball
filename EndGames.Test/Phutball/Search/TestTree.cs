using System;
using System.Collections.Generic;
using EndGames.Phutball.Search;

namespace EndGames.Tests.Phutball.Search
{
    public class TestTree<TNode> : ITree<TNode>
    {
        public TestTree(TNode node, IEnumerable<TestTree<TNode>> children)
        {
            Node = node;
            children.Each(child => child.Parent = this);
            Children = children;            
        }

        public TestTree(TNode node)
            : this(node, new List<TestTree<TNode>>())
        {
        }

        public TNode Node { get; private set; }

        public IEnumerable<ITree<TNode>> Children { get; private set; }

        public ITree<TNode> Parent { get; set; }
    }
}