using System.Collections;
using System.Collections.Generic;
using Phutball.Jumpers;
using Phutball.Moves;

namespace Phutball.Search
{
    public class PlayerJumpersFactory
    {
        private readonly IFieldsGraph _fieldsGraph;
        private readonly IPlayersState _playersState;
        private readonly IAlphaBetaOptions _alphaBetaOptions;
        private JumpersFactory _jumpersFactory;

        public PlayerJumpersFactory(IFieldsGraph fieldsGraph, IPlayersState playersState, IAlphaBetaOptions alphaBetaOptions)
        {
            _fieldsGraph = fieldsGraph;
            _playersState = playersState;
            _alphaBetaOptions = alphaBetaOptions;
            _jumpersFactory = new JumpersFactory(_fieldsGraph, _alphaBetaOptions.StoneRadius);
        }

        public IEnumerable<Field> PlacesAround(Field whiteField)
        {
            var currentPlayer = _playersState.CurrentPlayer;
            var targetBorder = currentPlayer.GetTargetBorder(_fieldsGraph);
            var places = targetBorder.PlacesForBlackStone();
            return _jumpersFactory.PlacesForBlack(places, whiteField);
        }
    }

    public class PlaceBlackStonesAroundWhite : IEnumerable<IJumpNodeTreeWithFactory>
    {
        private readonly IJumpNodeTreeWithFactory _parent;
        private readonly IPlayersState _playersState;
        private readonly IAlphaBetaOptions _alphaBetaOptions;
        private JumpNode _parentJumpNode;
        private PlayerJumpersFactory _placersFactory;

        public PlaceBlackStonesAroundWhite(IJumpNodeTreeWithFactory parent, IPlayersState playersState, IAlphaBetaOptions alphaBetaOptions)
        {
            _parent = parent;
            _playersState = playersState;
            _alphaBetaOptions = alphaBetaOptions;
            _parentJumpNode = _parent.Node;
            _placersFactory = new PlayerJumpersFactory(_parentJumpNode.ActualGraph, _playersState, _alphaBetaOptions);
        }

        public IEnumerator<IJumpNodeTreeWithFactory> GetEnumerator()
        {
            var whiteField = _parentJumpNode.ActualGraph.GetWhiteField();
            if(whiteField.IsWinningField(_parentJumpNode.ActualGraph.RowCount))
            {
                yield break;
            }

            foreach (var fieldToPlaceStoneAt in _placersFactory.PlacesAround(whiteField))
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