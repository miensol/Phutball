using System;
using System.Collections.Generic;

namespace EndGames.Phutball.Search
{
    public class AlternatingJumpsMovesTree : IJumpNodeTreeWithFactory
    {
        private readonly IEnumerable<IJumpNodeTree> _children;

        public AlternatingJumpsMovesTree(JumpNode jumpNode)
            :this(jumpNode, (parent)=> new AllAlternatigJumpsTreeCollection(parent))
        {
        }

        public AlternatingJumpsMovesTree(
            JumpNode jumpNode,
            Func<IJumpNodeTreeWithFactory, IEnumerable<IJumpNodeTreeWithFactory>> childFactory)
        {
            Node = jumpNode;
            ChildFactory = childFactory;
            _children = childFactory(this);
        }

        public JumpNode Node { get; private set; }
        public Func<IJumpNodeTreeWithFactory, IEnumerable<IJumpNodeTreeWithFactory>> ChildFactory { get; set; }

        public ITree<JumpNode> Parent { get; set; }

        public IEnumerable<ITree<JumpNode>> Children
        {
            get { return _children; }
        }

        public bool IsLeaf
        {
            get { return Children.IsEmpty(); }
        }
    }
}