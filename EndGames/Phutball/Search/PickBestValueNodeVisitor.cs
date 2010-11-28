using System.Collections.Generic;
using System.Linq;
using EndGames.Phutball.Moves;
using EndGames.Phutball.Search.BoardValues;

namespace EndGames.Phutball.Search
{
    public class PickBestValueNodeVisitor : ISearchNodeVisitor<JumpNode>
    {
        private readonly Stack<IMove<IFieldsGraph>> _acutalMoves = new Stack<IMove<IFieldsGraph>>();
        private readonly TargetBorder _targetBorder;
        private readonly IValueOfGraph _valueOfGraph;
        private readonly IFieldsGraph _graphCopy;

        public IMove<IFieldsGraph> ResultMove { get; private set; }

        private int CurrentMaxValue { get; set; }

        public PickBestValueNodeVisitor(TargetBorder targetBorder, IFieldsGraph graphCopy)
        {
            _targetBorder = targetBorder;
            _valueOfGraph = new WhiteStoneToBorderDistanceValue(targetBorder);
            _graphCopy = graphCopy;
            CurrentMaxValue = _valueOfGraph.GetValue(_graphCopy);
        }

        public void OnEnter(JumpNode node, ITreeSearchContinuation treeSearchContinuation)
        {
            int actualValue = CountActualValue(node);
            if (actualValue == _targetBorder.LooseValue)
            {
                treeSearchContinuation.DontEnterChildren();
            }
            else
            {
                UpdateMaxValue(actualValue, treeSearchContinuation);
            }
        }

        private int CountActualValue(JumpNode node)
        {
            var lastMove = node.LastMove;
            _acutalMoves.Push(lastMove);
            lastMove.Perform(node.ActualGraph);
            return _valueOfGraph.GetValue(node.ActualGraph);
        }

        private void UpdateMaxValue(int actualValue, ITreeSearchContinuation treeSearchContinuation)
        {
            if (actualValue <= CurrentMaxValue)
            {
                return;
            }
            if (actualValue == _targetBorder.WinValue)
            {
                treeSearchContinuation.Stop();
            }
            ResultMove = new CompositeMove<IFieldsGraph>(_acutalMoves.ToArray().Reverse());
            CurrentMaxValue = actualValue;
        }


        public void OnLeave(JumpNode node, ITreeSearchContinuation treeSearchContinuation)
        {
            node.LastMove.Undo(node.ActualGraph);
            _acutalMoves.Pop();
        }
    }
}