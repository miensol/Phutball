using System.Collections.Generic;
using System.Linq;
using EndGames.Phutball.Jumpers;
using EndGames.Phutball.Moves;

namespace EndGames.Phutball.Search
{
    public class BoardJumpTree : ITree<JumpNode>
    {
        public BoardJumpTree(IFieldsGraph actualGraph, IMove<IFieldsGraph> moveToApply)
        {
            Node = new JumpNode(actualGraph, moveToApply);
        }

        public JumpNode Node { get; set; }
        public IEnumerable<ITree<JumpNode>> Children
        {
            get { return GetPossibleMoves(); }
        }

        private IEnumerable<ITree<JumpNode>> GetPossibleMoves()
        {
            var actualGraph = Node.ActualGraph;
            var whiteField = actualGraph.GetWhiteField();
            var jumpDireactions = DirectedJumpersFactory.All(actualGraph, whiteField);
            return jumpDireactions.Where(jump => jump.EndField != null)
                .Select(
                    jump => new BoardJumpTree(actualGraph, 
                                              new JumpWhiteStoneMove(whiteField, jump.GetJumpedFields(),  jump.EndField)));
        }
    }
}