using System.Collections;
using System.Collections.Generic;
using EndGames.Phutball.Jumpers;
using EndGames.Phutball.Moves;

namespace EndGames.Phutball.Search
{
    public class PlaceBlackStonesAroundWhite : IEnumerable<IJumpNodeTreeWithFactory>
    {
        private readonly IJumpNodeTreeWithFactory _parent;
        private JumpNode _parentJumpNode;
        private DirectedJumpersFactory _placersFactory;

        public PlaceBlackStonesAroundWhite(IJumpNodeTreeWithFactory parent)
        {
            _parent = parent;
            _parentJumpNode = _parent.Node;
            _placersFactory = new DirectedJumpersFactory(_parentJumpNode.ActualGraph);
        }

        public IEnumerator<IJumpNodeTreeWithFactory> GetEnumerator()
        {
            var whiteField = _parentJumpNode.ActualGraph.GetWhiteField();
            if(whiteField.IsWinningField(_parentJumpNode.ActualGraph.RowCount))
            {
                yield break;
            }

            foreach (var fieldToPlaceStoneAt in _placersFactory.AllPlaces(whiteField))
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