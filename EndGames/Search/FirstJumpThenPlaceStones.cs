using System.Collections;
using System.Collections.Generic;

namespace Phutball.Search
{
    public class FirstJumpThenPlaceStones : IEnumerable<IJumpNodeTreeWithFactory>
    {
        private readonly IJumpNodeTreeWithFactory _parent;
        private readonly IAlphaBetaOptions _options;
        private readonly IPlayersState _playerState;

        public FirstJumpThenPlaceStones(IJumpNodeTreeWithFactory parent, IAlphaBetaOptions options, IPlayersState playerState)
        {
            _parent = parent;
            _options = options;
            _playerState = playerState;
        }

        public IEnumerator<IJumpNodeTreeWithFactory> GetEnumerator()
        {
            foreach (var subMove in new AllAlternatigJumpsTreeCollection(_parent, _options))
            {
                yield return subMove;
            }
            foreach (var subMove in new PlaceBlackStonesForPlayer(_parent, _playerState, () => new TowardsTargetBorderStonePlacer(_options)))
            {
                yield return subMove;
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}