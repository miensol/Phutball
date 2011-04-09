using System.Collections.Generic;
using EndGames.Phutball.PlayerMoves;

namespace EndGames.Phutball.Search
{
    public class AlternatingAllJumpsMovesTree : ITree<JumpNode>
    {
        private readonly IPerformMoves _performMoves;

        public AlternatingAllJumpsMovesTree(IPerformMoves performMoves)
        {
            _performMoves = performMoves;
        }

        public JumpNode Node { get; set; }

        public ITree<JumpNode> Parent { get; set; }

        public IEnumerable<ITree<JumpNode>> Children
        {
            get { return new AllAlternatigJumpsTreeCollection<JumpNode>(_performMoves, this); }
        }

        public bool IsLeaf
        {
            get { return Children.IsEmpty(); }
        }
    }
}