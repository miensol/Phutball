using System.Collections.Generic;
using EndGames.Phutball.PlayerMoves;

namespace EndGames.Phutball.Search
{
    public class AlternatingAllJumpsMovesTree : ITree<JumpNode>
    {
        private readonly IPerformMoves _performMoves;
        private AllAlternatigJumpsTreeCollection<JumpNode> _children;

        public AlternatingAllJumpsMovesTree(IPerformMoves performMoves, JumpNode jumpNode)
        {
            _performMoves = performMoves;
            Node = jumpNode;
            _children = new AllAlternatigJumpsTreeCollection<JumpNode>(_performMoves, this);
        }

        public JumpNode Node { get; private set; }

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