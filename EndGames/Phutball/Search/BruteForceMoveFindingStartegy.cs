using System.Collections.Generic;
using EndGames.Phutball.Moves;
using EndGames.Phutball.Search.BoardValues;
using System.Linq;

namespace EndGames.Phutball.Search
{
    public class BruteForceMoveFindingStartegy : IMoveFindingStartegy, IDfsSearchStartegy<JumpNode>
    {
        private readonly TargetBorder _targetBorder;
        private readonly Stack<IMove<IFieldsGraph>> _acutalMoves = new Stack<IMove<IFieldsGraph>>();
        private readonly IValueOfGraph _valueOfGraph;
        private int _currentMaxValue;
        private IMove<IFieldsGraph> _resultMove;

        public BruteForceMoveFindingStartegy(TargetBorder targetBorder)
        {
            _targetBorder = targetBorder;
            _valueOfGraph = new WhiteStoneToBorderDistanceValue(_targetBorder);
        }


        public IMove<IFieldsGraph> Search(IFieldsGraph fieldsGraph)
        {
            var graphCopy = (IFieldsGraph)fieldsGraph.Clone();
            var whiteField = graphCopy.GetWhiteField();
            _currentMaxValue = _valueOfGraph.GetValue(graphCopy);
            var tree = new BoardJumpTree(graphCopy, new SelectWhiteFieldMove(whiteField));
            var search = new DfsSearch<JumpNode>(this);
            search.Run(tree);
            return _resultMove;
        }

        public void OnEnter(JumpNode node, IDfsContinuation dfsContinuation)
        {
            int actualValue = CountActualValue(node);
            if(actualValue == _targetBorder.LooseValue)
            {
                dfsContinuation.DontEnterChildren();
            } else
            {
                UpdateMaxValue(actualValue, dfsContinuation);
            }
        }

        private int CountActualValue(JumpNode node)
        {
            var lastMove = node.LastMove;
            _acutalMoves.Push(lastMove);
            lastMove.Perform(node.ActualGraph);
            return _valueOfGraph.GetValue(node.ActualGraph);
        }

        private void UpdateMaxValue(int actualValue, IDfsContinuation dfsContinuation)
        {
            if (actualValue <= _currentMaxValue)
            {
                return;
            }
            if (actualValue == _targetBorder.WinValue)
            {
                dfsContinuation.Stop();
            }
            _resultMove = new CompositeMove<IFieldsGraph>(_acutalMoves.ToArray().Reverse());
            _currentMaxValue = actualValue;
        }

        public void OnLeave(JumpNode node, IDfsContinuation dfsContinuation)
        {
            node.LastMove.Undo(node.ActualGraph);
            _acutalMoves.Pop();
        }
    }
}