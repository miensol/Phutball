using System.Collections;
using System.Collections.Generic;
using System.Linq;
using EndGames.Phutball.Moves;
using EndGames.Phutball.PlayerMoves;

namespace EndGames.Phutball.Search
{
    public class AllAlternatigJumpsTreeCollection<T> : IEnumerable<ITree<JumpNode>>
    {
        private readonly IPerformMoves _performMoves;
        private RootedBySelectingWhiteFieldBoardJumpTree _current;
        private IPhutballMove _previousMove;
        private JumpNode _parentJumpNode;

        public AllAlternatigJumpsTreeCollection(IPerformMoves performMoves, ITree<JumpNode> parent)
        {
            _performMoves = performMoves;
            Parent = parent;
            _parentJumpNode = Parent.Node;
            _current = new RootedBySelectingWhiteFieldBoardJumpTree(_parentJumpNode.ActualGraph);            
        }

        public ITree<JumpNode> Parent { get; private set; }

        public IEnumerator<ITree<JumpNode>> GetEnumerator()
        {
            RevertPrevious();
            var currentMoves = _current.TraverseWithDfs();
            foreach (var currentMove in currentMoves)
            {
                RevertPrevious();
                var newMove = PerformNewMove(currentMove);
                var jumpNode = new JumpNode(_parentJumpNode.ActualGraph, _parentJumpNode.LastMove.FollowedBy(newMove));
                yield return new AlternatingAllJumpsMovesTree(_performMoves, jumpNode);                
            }
            RevertPrevious();
        }

        private IPhutballMove PerformNewMove(ITree<JumpNode> currentMove)
        {
            var newMove = currentMove.PathFromRoot()
                .Select(t=> t.Node.LastMove)
                .Concat(new[]{ new DeselectWhiteFieldIfSelectedMove()}).ToComposite();

            _previousMove = newMove;

            _performMoves.Perform(newMove);
            return newMove;
        }

        private void RevertPrevious()
        {                
            if(_previousMove != null)
            {
                _performMoves.Undo(_previousMove);
                _previousMove = null;
            }            
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }        
    }
}