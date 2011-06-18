using System;
using System.Collections.Generic;
using System.Linq;

namespace Phutball.Search
{
    public class CompositeBlackStonePlacer : IPlaceBlackStone
    {
        private readonly IPlaceBlackStone _first;
        private readonly IPlaceBlackStone _right;

        public CompositeBlackStonePlacer(IPlaceBlackStone first, IPlaceBlackStone right)
        {
            _first = first;
            _right = right;
        }

        public IEnumerable<Tuple<int, int>> UpperIsTarget(IFieldsGraph fieldsGraph)
        {
            return _first.UpperIsTarget(fieldsGraph).Concat(_right.UpperIsTarget(fieldsGraph));
        }

        public IEnumerable<Tuple<int, int>> BottomIsTarget(IFieldsGraph fieldsGraph)
        {
            return _first.BottomIsTarget(fieldsGraph).Concat(_right.BottomIsTarget(fieldsGraph));
        }
    }
}