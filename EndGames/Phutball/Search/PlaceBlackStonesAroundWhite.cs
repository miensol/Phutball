using System;
using System.Collections;
using System.Collections.Generic;
using EndGames.Phutball.Jumpers;
using EndGames.Phutball.Moves;
using EndGames.Phutball.PlayerMoves;

namespace EndGames.Phutball.Search
{
    public class PlaceBlackStonesAroundWhite : IRevertibleMovesCollection
    {
        private readonly IPerformMoves _perform;
        private readonly IJumpNodeTreeWithFactory _parent;
        private IPhutballMove _previous;
        private JumpNode _parentJumpNode;
        private DirectedJumpersFactory _placersFactory;

        public PlaceBlackStonesAroundWhite(IPerformMoves perform, IJumpNodeTreeWithFactory parent)
        {
            _perform = perform;
            _parent = parent;
            _parentJumpNode = _parent.Node;
            _previous = new EmptyPhutballMove();
            _placersFactory = new DirectedJumpersFactory(_parentJumpNode.ActualGraph);
        }

        public IEnumerator<IJumpNodeTreeWithFactory> GetEnumerator()
        {
            RevertPrevious();
            var whiteField = _parentJumpNode.ActualGraph.GetWhiteField();
            if(whiteField.IsWinningField(_parentJumpNode.ActualGraph.RowCount))
            {
                yield break;
            }

            foreach (var fieldToPlaceStoneAt in _placersFactory.AllPlaces(whiteField))
            {
                RevertPrevious();
                var newMove = PerformNewMove(fieldToPlaceStoneAt);
                var jumpNode = _parentJumpNode.FollowedBy(newMove);
                yield return new AlternatingAllJumpsMovesTree(_perform, jumpNode, _parent.ChildFactory);                
            }
            
            RevertPrevious();
        }

        private IPhutballMove PerformNewMove(Field fieldToPlaceStoneAt)
        {
            var newMove = new PlaceBlackStoneMove(fieldToPlaceStoneAt);
            _perform.Perform(newMove);
            _previous = newMove;
            return newMove;
        }

        public void RevertPrevious()
        {
            _perform.Undo(_previous);
            _previous = new EmptyPhutballMove();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}