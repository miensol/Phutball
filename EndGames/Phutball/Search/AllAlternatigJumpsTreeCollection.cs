using System.Collections;
using System.Collections.Generic;
using System.Linq;
using EndGames.Phutball.Moves;
using EndGames.Phutball.PlayerMoves;

namespace EndGames.Phutball.Search
{
    public class AllAlternatigJumpsTreeCollection : IEnumerable<IJumpNodeTreeWithFactory>
    {
        private JumpNode _parentJumpNode;

        public AllAlternatigJumpsTreeCollection(IJumpNodeTreeWithFactory parent)
        {
            Parent = parent;
            _parentJumpNode = Parent.Node;            
        }

        private IJumpNodeTreeWithFactory Parent { get; set; }

        public IEnumerator<IJumpNodeTreeWithFactory> GetEnumerator()
        {            
            var actualGraph = (IFieldsGraph) _parentJumpNode.ActualGraph.Clone();
            var localMovePerformer = PerformMoves.DontCareAboutPlayerStateChange(actualGraph);
            var current = new RootedBySelectingWhiteFieldBoardJumpTree(actualGraph);
            var currentMoves = current.TraverseDfs(new PerformMovesNodeVisitor(localMovePerformer)).Skip(1);
            foreach (var currentMove in currentMoves)
            {
                var newMove = CreateNewMove(currentMove);
                var jumpNode = _parentJumpNode.FollowedBy(newMove);                
                yield return new AlternatingJumpsMovesTree(jumpNode, Parent.ChildFactory);                
            }
        }

        private IPhutballMove CreateNewMove(ITree<JumpNode> currentMove)
        {
            var newMove = currentMove.PathFromRoot()
                .Select(t=> t.Node.LastMove)
                .Concat(new[]{ new DeselectWhiteFieldIfSelectedMove()}).ToComposite();
            return newMove;
        }
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

    }
}