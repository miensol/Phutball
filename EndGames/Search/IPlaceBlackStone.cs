using System;
using System.Collections.Generic;

namespace Phutball.Search
{
    public interface IPlaceBlackStone
    {
        IEnumerable<Tuple<int, int>> UpperIsTarget(IFieldsGraph fieldsGraph);
        IEnumerable<Tuple<int, int>> BottomIsTarget(IFieldsGraph fieldsGraph);
    }
}