using System;
using System.Collections.Generic;
using EndGames.Phutball.Search;

namespace EndGames.Tests.Phutball.Search
{
    public class TestTree<TNode> : ITree<TNode>
    {
        private IEnumerable<TestTree<TNode>> _children;

        public TestTree(TNode node, IEnumerable<TestTree<TNode>> children)
        {
            Node = node;
            _children  = children;            
        }

        public TestTree(TNode node)
            : this(node, new List<TestTree<TNode>>())
        {
        }

        public TNode Node { get; private set; }

        public IEnumerable<ITree<TNode>> Children
        {
            get
            {
                foreach (var child in _children)
                {
                    child.Parent = this;
                    yield return child;
                }
            }
        }

        public ITree<TNode> Parent { get; set; }

        public bool IsLeaf
        {
            get { return Children.IsEmpty(); }
        }
    }
}