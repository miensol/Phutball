using System.Collections;
using System.Collections.Generic;
using System.Linq;
using EndGames.Phutball.Moves;
using EndGames.Phutball.PlayerMoves;

namespace EndGames.Phutball.Search
{
    public interface IRevertibleMovesCollection : IEnumerable<IJumpNodeTreeWithFactory>
    {
        void RevertPrevious();
    }


    public class AllAlternatigJumpsTreeCollection : IRevertibleMovesCollection
    {
        private readonly IPerformMoves _performMoves;
        private RootedBySelectingWhiteFieldBoardJumpTree _current;
        private IPhutballMove _previousMove;
        private JumpNode _parentJumpNode;

        public AllAlternatigJumpsTreeCollection(IPerformMoves performMoves, IJumpNodeTreeWithFactory parent)
        {
            _performMoves = performMoves;
            Parent = parent;
            _parentJumpNode = Parent.Node;
            _current = new RootedBySelectingWhiteFieldBoardJumpTree(_parentJumpNode.ActualGraph);            
        }

        private IJumpNodeTreeWithFactory Parent { get; set; }

        public IEnumerator<IJumpNodeTreeWithFactory> GetEnumerator()
        {
            RevertPrevious();
            var currentMoves = _current.TraverseWithDfs().Skip(1);
            foreach (var currentMove in currentMoves)
            {
                RevertPrevious();
                var newMove = PerformNewMove(currentMove);
                var jumpNode = _parentJumpNode.FollowedBy(newMove);                
                yield return new AlternatingAllJumpsMovesTree(_performMoves, jumpNode, Parent.ChildFactory);                
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

        public void RevertPrevious()
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