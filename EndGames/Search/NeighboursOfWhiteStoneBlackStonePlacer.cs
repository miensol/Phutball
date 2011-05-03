using System;
using System.Collections.Generic;
using System.Linq;
using Phutball.Jumpers;

namespace Phutball.Search
{
    public class NeighboursOfWhiteStoneBlackStonePlacer : IPlaceBlackStone {
        private readonly Tuple<int, int> _firstPosition;
        private readonly IEnumerable<Tuple<int, int>> _whiteCoords;
        private IEnumerable<Tuple<int, int>> _allCoords;

        public NeighboursOfWhiteStoneBlackStonePlacer(Tuple<int, int> firstPosition, IEnumerable<Tuple<int, int>> whiteCoords)
        {
            _firstPosition = firstPosition;
            _whiteCoords = whiteCoords;
            _allCoords = new[] {_firstPosition}.Concat(_whiteCoords);
        }

        public IEnumerable<Tuple<int, int>> UpperIsTarget(IFieldsGraph fieldsGraph)
        {
            return Direction.AllUpper.Take(5).Select(dir => _firstPosition.Add(dir))
                .Concat(new[]{_firstPosition})
                .Concat(_whiteCoords)
                .Concat(Direction.AllUpper.Take(3).SelectMany(
                    dir => _whiteCoords.Select(coord => coord.Add(dir))));

        }

        public IEnumerable<Tuple<int, int>> BottomIsTarget(IFieldsGraph fieldsGraph)
        {
            return Direction.AllBottom.Take(5).Select(dir => _firstPosition.Add(dir))
                .Concat(new[] { _firstPosition })
                .Concat(_whiteCoords)
                .Concat(Direction.AllBottom.Take(3).SelectMany(
                    dir => _whiteCoords.Select(coord => coord.Add(dir))));
        }
    }
}