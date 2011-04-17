using System.Collections.Generic;
using System.Linq;
using EndGames.Phutball.Jumpers;
using EndGames.Phutball.Moves;

namespace EndGames.Phutball.Search
{
    public class BoardJumpTree : ITree<JumpNode>
    {
        private readonly JumpersFactory _jumpersFactory;

        public BoardJumpTree(IFieldsGraph actualGraph, IPhutballMove moveToApply, ITree<JumpNode> parent)
        {
            Parent = parent;
            Node = new JumpNode(actualGraph, moveToApply);
            _jumpersFactory = new JumpersFactory(actualGraph);
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

            if(whiteField.IsWinningField(actualGraph.RowCount))
            {
                return new List<ITree<JumpNode>>();
            }

            var jumpDireactions = _jumpersFactory.AllJumps(whiteField);
            return jumpDireactions.Where(jump => jump.EndField != null)
                .Select(
                    jump => new BoardJumpTree(actualGraph, 
                                              new JumpWhiteStoneMove(whiteField, jump.GetJumpedFields(),  jump.EndField), 
                                              this));
        }

        public override string ToString()
        {
            if(Parent != null)
            {
                return "Parent: {0}, Node: {1}".ToFormat(Parent.Node.ToString(), Node.ToString());    
            }
            return "Parent: null, Node: {0}".ToFormat(Node.ToString());

        }        
    }
}