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
        private IPhutballMove _previousMove = new EmptyPhutballMove();
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
            var currentMoves = _current.TraverseWithDfs();
            foreach (var currentMove in currentMoves)
            {
                RevertPrevious();

                var newMove = PerformNewMove(currentMove);

                yield return new AlternatingAllJumpsMovesTree(_performMoves)
                                 {
                                     Node = new JumpNode(_parentJumpNode.ActualGraph, _parentJumpNode.LastMove.FollowedBy(newMove))
                                 };
                                
            }
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
            _performMoves.Undo(_previousMove);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }        
    }
}