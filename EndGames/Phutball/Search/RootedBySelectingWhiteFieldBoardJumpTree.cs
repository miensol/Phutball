using System;
using System.Collections.Generic;
using EndGames.Phutball.Moves;

namespace EndGames.Phutball.Search
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
            return new BoardJumpTree(_fieldsGraph, new SelectWhiteFieldMove(whiteField), this);
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
    }
}