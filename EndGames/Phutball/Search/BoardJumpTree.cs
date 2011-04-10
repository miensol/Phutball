using System;
using System.Collections.Generic;
using System.Linq;
using EndGames.Phutball.Jumpers;
using EndGames.Phutball.Moves;

namespace EndGames.Phutball.Search
{
    public class BoardJumpTree : ITree<JumpNode>
    {
        private readonly DirectedJumpersFactory _jumpersFactory;

        public BoardJumpTree(IFieldsGraph actualGraph, IPhutballMove moveToApply, ITree<JumpNode> parent)
        {
            Parent = parent;
            Node = new JumpNode(actualGraph, moveToApply);
            _jumpersFactory = new DirectedJumpersFactory(actualGraph);
        }

        public JumpNode Node { get; private set; }

        public IEnumerable<ITree<JumpNode>> Children
        {
            get { return GetPossibleMoves(); }
        }

        public ITree<JumpNode> Parent { get; private set; }

        public bool IsLeaf
        {
            get { return GetPossibleMoves().IsEmpty(); }
        }

        private IEnumerable<ITree<JumpNode>> GetPossibleMoves()
        {
            var actualGraph = Node.ActualGraph;
            var whiteField = actualGraph.GetWhiteField();
            var jumpDireactions = _jumpersFactory.All(whiteField);
            return jumpDireactions.Where(jump => jump.EndField != null)
                .Select(
                    jump => new BoardJumpTree(actualGraph, 
                                              new JumpWhiteStoneMove(whiteField, jump.GetJumpedFields(),  jump.EndField), 
                                              this));
        }

        public override string ToString()
        {
            return Node.ToString();
        }
    }
}