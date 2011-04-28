using System.Collections.Generic;
using System.Linq;
using Phutball.Search.BoardValues;

namespace Phutball.Search
{
    public class PlayerBlackStonePlacer
    {
        private readonly IFieldsGraph _fieldsGraph;
        private readonly IPlayersState _playersState;

        public PlayerBlackStonePlacer(IFieldsGraph fieldsGraph, IPlayersState playersState)
        {
            _fieldsGraph = fieldsGraph;
            _playersState = playersState;
        }

        public IEnumerable<Field> CurrentPlayerPlaces(IPlaceBlackStone blackStonePlacer)
        {
            var currentPlayer = _playersState.CurrentPlayer;
            var targetBorder = currentPlayer.GetTargetBorder(_fieldsGraph);
            return targetBorder.Select(() => blackStonePlacer.UpperIsTarget(_fieldsGraph),
                                       () => blackStonePlacer.BottomIsTarget(_fieldsGraph))
                .Distinct()
                .Where(_fieldsGraph.CanPlaceBlackStone)
                .Select(_fieldsGraph.GetField);
        }
    }
}