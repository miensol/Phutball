using System;
using System.Collections.Generic;

namespace Phutball.Search
{
    public interface IAddRemoveCollection<TItem> : IEnumerable<TItem>
    {
        TItem Pull();
        void Put(TItem item);
    }
}