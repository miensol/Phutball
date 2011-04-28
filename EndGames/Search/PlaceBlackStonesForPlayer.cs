using System;
using System.Collections;
using System.Collections.Generic;
using Phutball.Moves;

namespace Phutball.Search
{
    public class PlaceBlackStonesForPlayer : IEnumerable<IJumpNodeTreeWithFactory>
    {
        private readonly IJumpNodeTreeWithFactory _parent;
        private readonly IPlayersState _playersState;
        private readonly Func<IPlaceBlackStone> _blackStonePlacer;
        private readonly JumpNode _parentJumpNode;
        private readonly PlayerBlackStonePlacer _placersFactory;

        public PlaceBlackStonesForPlayer(
            IJumpNodeTreeWithFactory parent, 
            IPlayersState playersState, 
            Func<IPlaceBlackStone> blackStonePlacer)
        {
            _parent = parent;
            _playersState = playersState;
            _blackStonePlacer = blackStonePlacer;
            _parentJumpNode = _parent.Node;
            _placersFactory = new PlayerBlackStonePlacer(_parentJumpNode.ActualGraph, _playersState);
        }

        public IEnumerator<IJumpNodeTreeWithFactory> GetEnumerator()
        {
            var whiteField = _parentJumpNode.ActualGraph.GetWhiteField();
            if(whiteField.IsWinningField(_parentJumpNode.ActualGraph.RowCount))
            {
                yield break;
            }

            foreach (var fieldToPlaceStoneAt in _placersFactory.CurrentPlayerPlaces(_blackStonePlacer()))
            {
                var newMove = new PlaceBlackStoneMove(fieldToPlaceStoneAt);
                var jumpNode = _parentJumpNode.FollowedBy(newMove);
                yield return new AlternatingJumpsMovesTree(jumpNode, _parent.ChildFactory);                
            }            
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}