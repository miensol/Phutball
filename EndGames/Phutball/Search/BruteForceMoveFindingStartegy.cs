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
        private OneTimeValueThenDefault<bool > _enterChilren = new OneTimeValueThenDefault<bool>(true, true);
        private OneTimeValueThenDefault<bool> _shouldStop = new OneTimeValueThenDefault<bool>(false, false);

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

        public void OnEnter(JumpNode node)
        {
            var lastMove = node.LastMove;
            _acutalMoves.Push(lastMove);
            lastMove.Perform(node.ActualGraph);
            var actualValue = _valueOfGraph.GetValue(node.ActualGraph);
            if(actualValue == _targetBorder.LooseValue)
            {
                _enterChilren = new OneTimeValueThenDefault<bool>(false, true);
            } else
            {
                UpdateMaxValue(actualValue);
            }
        }

        private void UpdateMaxValue(int actualValue)
        {
            if (actualValue == _targetBorder.WinValue)
            {
                _shouldStop = new OneTimeValueThenDefault<bool>(true, true);
            }
            if (actualValue > _currentMaxValue)
            {
                _resultMove = new CompositeMove<IFieldsGraph>(_acutalMoves.ToArray().Reverse());
                _currentMaxValue = actualValue;
            }
        }

        public void OnLeave(JumpNode node)
        {
            node.LastMove.Undo(node.ActualGraph);
            _acutalMoves.Pop();
        }

        public bool ShouldStop(JumpNode node)
        {
            return _shouldStop.GetValue();
        }

        public bool ShouldEnterChildrenOf(JumpNode node)
        {
            return _enterChilren.GetValue();
        }
    }
}