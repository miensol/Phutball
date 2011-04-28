using System;
using System.Collections.Generic;
using System.Linq;
using Phutball.Jumpers;

namespace Phutball.Search
{
    public class TowardsTargetBorderStonePlacer : IPlaceBlackStone
    {
        private readonly IAlphaBetaOptions _alphaBetaOptions;
        private readonly IEnumerable<Tuple<int, int>> _upper;
        private readonly IEnumerable<Tuple<int, int>> _bottom;

        public TowardsTargetBorderStonePlacer(IAlphaBetaOptions alphaBetaOptions)
        {
            _alphaBetaOptions = alphaBetaOptions;
            _upper = Direction.All.Take(5).ToList();
            _bottom = Direction.All.Reverse().Take(5).ToList();
        }

        public IEnumerable<Tuple<int, int>> UpperIsTarget(IFieldsGraph fieldsGraph)
        {
            var coord = fieldsGraph.GetWhiteFieldCoords();

            return _upper.SelectMany(MultiplyByRadius).Reverse()
                .Select(r=> coord.Add(r));
        }

        private IEnumerable<Tuple<int, int>> MultiplyByRadius(Tuple<int, int> tuple)
        {
            return _alphaBetaOptions.StoneRadius.Times((i) => tuple.Multiply(i * 2 + 1));
        }

        public IEnumerable<Tuple<int, int>> BottomIsTarget(IFieldsGraph fieldsGraph)
        {
            var coord = fieldsGraph.GetWhiteFieldCoords();

            return _bottom.SelectMany(MultiplyByRadius).Reverse()
                .Select(r => coord.Add(r));
        }
    }
}