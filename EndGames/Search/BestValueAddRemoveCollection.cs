using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Phutball.PlayerMoves;
using Phutball.Search.BoardValues;

namespace Phutball.Search
{
    public class BestValueAddRemoveCollection :IAddRemoveCollection<ITree<JumpNode>>
    {
        private readonly IPerformMoves _performMoves;
        private readonly IValueOfGraph _valueOfGraph;
        private BinaryHeap<int, ITree<JumpNode>> _inner;

        public BestValueAddRemoveCollection(IPerformMoves performMoves, IValueOfGraph valueOfGraph)
        {
            _performMoves = performMoves;
            _valueOfGraph = valueOfGraph;
            _inner = new BinaryHeap<int, ITree<JumpNode>>((left,right)=> right - left);
        }

        public IEnumerator<ITree<JumpNode>> GetEnumerator()
        {
            return _inner.Select(kv=> kv.Value).GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public ITree<JumpNode> Pull()
        {
            return _inner.RemoveMinimum().Value;
        }

        public void Put(ITree<JumpNode> item)
        {
            var jumpNode = item.Node;
            _performMoves.Perform(jumpNode.LastMove);
            var value = _valueOfGraph.GetValue(jumpNode.ActualGraph);
            _inner.Add(value, item);
            _performMoves.Undo(jumpNode.LastMove);
        }
    }
}