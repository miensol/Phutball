using System.Collections.Generic;
using System.Linq;
using Phutball.Moves;
using Phutball.PlayerMoves;
using Phutball.Search.BoardValues;

namespace Phutball.Search
{
    public class PickBestValueNodeVisitor : ISearchNodeVisitor<JumpNode>
    {
        private readonly Stack<IPhutballMove> _acutalMoves = new Stack<IPhutballMove>();
        private readonly TargetBorder _targetBorder;
        private readonly IValueOfGraph _valueOfGraph;
        private readonly IFieldsGraph _graphCopy;
        private readonly IPerformMoves _performMoves;

        public IPhutballMove ResultMove { get; private set; }

        public int CurrentMaxValue { get; set; }

        public PickBestValueNodeVisitor(TargetBorder targetBorder, IFieldsGraph graphCopy, IPerformMoves performMoves)
        {
            _targetBorder = targetBorder;
            _valueOfGraph = new WhiteStoneToBorderDistanceValue(targetBorder);
            _graphCopy = graphCopy;
            _performMoves = performMoves;
            CurrentMaxValue = _valueOfGraph.GetValue(_graphCopy);
        }

        public void OnEnter(ITree<JumpNode> node, ITreeSearchContinuation treeSearchContinuation)
        {
            int actualValue = CountActualValue(node.Node);
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
            _performMoves.Perform(lastMove);
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
            ResultMove = new CompositeMove(_acutalMoves.ToArray().Reverse());
            CurrentMaxValue = actualValue;
        }


        public void OnLeave(ITree<JumpNode> node, ITreeSearchContinuation treeSearchContinuation)
        {
            _performMoves.Undo(node.Node.LastMove);
            _acutalMoves.Pop();
        }
    }
}