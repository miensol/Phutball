using System;
using System.Collections.Generic;
using System.Linq;
using Phutball.Jumpers;

namespace Phutball.Search
{
    public class TowardsTargetBorderStonePlacer : IPlaceBlackStone
    {
        private readonly IAlphaBetaOptions _alphaBetaOptions;
        private readonly IEnumerable<Tuple<int, int>> _allUpper;
        private readonly IEnumerable<Tuple<int, int>> _allBottom;

        public TowardsTargetBorderStonePlacer(IAlphaBetaOptions alphaBetaOptions)
        {
            _alphaBetaOptions = alphaBetaOptions;
            _allUpper = Direction.AllUpper.Take(5)
                .Concat(new[]{ Direction.N.Multiply(2).Add(Direction.W), Direction.N.Multiply(2).Add(Direction.E) })
                .Shuffle();
            _allBottom = Direction.AllBottom.Take(5)
                .Concat(new[] { Direction.S.Multiply(2).Add(Direction.W), Direction.S.Multiply(2).Add(Direction.E) })
                .Shuffle();

        }

        public IEnumerable<Tuple<int, int>> UpperIsTarget(IFieldsGraph fieldsGraph)
        {
            var coord = fieldsGraph.GetWhiteFieldCoords();

            return _allUpper.SelectMany(MultiplyByRadius)
                .Select(r=> coord.Add(r));
        }

        private IEnumerable<Tuple<int, int>> MultiplyByRadius(Tuple<int, int> tuple)
        {
            return _alphaBetaOptions.StoneRadius.Times((i) => tuple.Multiply(i * 2 + 1));
        }

        public IEnumerable<Tuple<int, int>> BottomIsTarget(IFieldsGraph fieldsGraph)
        {
            var coord = fieldsGraph.GetWhiteFieldCoords();

            return _allBottom.SelectMany(MultiplyByRadius)
                .Select(r => coord.Add(r));
        }
    }
}