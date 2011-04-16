using System;
using System.Collections.Generic;
using EndGames.Phutball.PlayerMoves;

namespace EndGames.Phutball.Search
{
    public class AlternatingAllJumpsMovesTree : IJumpNodeTreeWithFactory
    {
        private readonly IPerformMoves _performMoves;
        private readonly IEnumerable<IJumpNodeTree> _children;

        public AlternatingAllJumpsMovesTree(IPerformMoves performMoves, JumpNode jumpNode)
            :this(performMoves, jumpNode, (perform,parent)=> new AllAlternatigJumpsTreeCollection(perform, parent))
        {
        }

        public AlternatingAllJumpsMovesTree(IPerformMoves performMoves, 
            JumpNode jumpNode,
            Func<IPerformMoves, IJumpNodeTreeWithFactory, IEnumerable<IJumpNodeTreeWithFactory>> childFactory)
        {
            _performMoves = performMoves;
            Node = jumpNode;
            ChildFactory = childFactory;
            _children = childFactory(_performMoves, this);
        }

        public JumpNode Node { get; private set; }
        public Func<IPerformMoves, IJumpNodeTreeWithFactory, IEnumerable<IJumpNodeTreeWithFactory>> ChildFactory { get; set; }

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