using System;
using System.Collections.Generic;
using System.Linq;
using Phutball.Jumpers;

namespace Phutball.Search
{
    public class TowardsTargetBorderStonePlacer : IPlaceBlackStone
    {
        private readonly IAlphaBetaOptions _alphaBetaOptions;

        public TowardsTargetBorderStonePlacer(IAlphaBetaOptions alphaBetaOptions)
        {
            _alphaBetaOptions = alphaBetaOptions;
        }

        public IEnumerable<Tuple<int, int>> UpperIsTarget(IFieldsGraph fieldsGraph)
        {
            var coord = fieldsGraph.GetWhiteFieldCoords();

            return Direction.AllUpper.Take(5).SelectMany(MultiplyByRadius)
                .Select(r=> coord.Add(r));
        }

        private IEnumerable<Tuple<int, int>> MultiplyByRadius(Tuple<int, int> tuple)
        {
            return _alphaBetaOptions.StoneRadius.Times((i) => tuple.Multiply(i * 2 + 1));
        }

        public IEnumerable<Tuple<int, int>> BottomIsTarget(IFieldsGraph fieldsGraph)
        {
            var coord = fieldsGraph.GetWhiteFieldCoords();

            return Direction.AllBottom.Take(5).SelectMany(MultiplyByRadius)
                .Select(r => coord.Add(r));
        }
    }
}