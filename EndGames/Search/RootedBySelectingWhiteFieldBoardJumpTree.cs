using System.Collections.Generic;
using Phutball.Moves;

namespace Phutball.Search
{
    public class RootedBySelectingWhiteFieldBoardJumpTree : ITree<JumpNode>
    {
        private readonly IFieldsGraph _fieldsGraph;
        private readonly BoardJumpTree _realBoard;

        public RootedBySelectingWhiteFieldBoardJumpTree(IFieldsGraph fieldsGraph)
        {
            _fieldsGraph = fieldsGraph;
            _realBoard = SelectWhiteFieldNode();
        }

        private BoardJumpTree SelectWhiteFieldNode()
        {
            var whiteField = _fieldsGraph.GetWhiteField();
            if(whiteField.Selected == false)
            {
                return new BoardJumpTree(_fieldsGraph, new SelectWhiteFieldMove(whiteField), null);    
            }
            return new BoardJumpTree(_fieldsGraph, new EmptyPhutballMove(), null);
        }

        public JumpNode Node { get { return _realBoard.Node; } }

        public IEnumerable<ITree<JumpNode>> Children
        {
            get { return _realBoard.Children; }
        }

        public ITree<JumpNode> Parent
        {
            get { return null; }
        }

        public bool IsLeaf
        {
            get { return _realBoard.IsLeaf; }
        }

        public override bool Equals(object obj)
        {
            return _realBoard == obj;
        }
    }
}