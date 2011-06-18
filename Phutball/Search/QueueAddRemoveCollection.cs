using System.Collections;
using System.Collections.Generic;

namespace Phutball.Search
{
    public class QueueAddRemoveCollection<TItem> : IAddRemoveCollection<TItem>
    {
        private Queue<TItem> _innerCol;

        public QueueAddRemoveCollection()
        {
            _innerCol = new Queue<TItem>();
        }

        public TItem Pull()
        {
            return _innerCol.Dequeue();
        }

        public void Put(TItem item)
        {
            _innerCol.Enqueue(item);
        }

        public IEnumerator<TItem> GetEnumerator()
        {
            return _innerCol.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}