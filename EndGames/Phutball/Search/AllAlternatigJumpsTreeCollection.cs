using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using EndGames.Phutball.Moves;
using EndGames.Phutball.PlayerMoves;

namespace EndGames.Phutball.Search
{
    public class AllAlternatigJumpsTreeCollection : IEnumerable<IJumpNodeTreeWithFactory>, ISearchNodeVisitor<JumpNode>
    {
        private RootedBySelectingWhiteFieldBoardJumpTree _current;
        private JumpNode _parentJumpNode;
        private IPerformMoves _localMovePerformer;

        public AllAlternatigJumpsTreeCollection(IJumpNodeTreeWithFactory parent)
        {
            Parent = parent;
            _parentJumpNode = Parent.Node;            
        }

        private IJumpNodeTreeWithFactory Parent { get; set; }

        public IEnumerator<IJumpNodeTreeWithFactory> GetEnumerator()
        {            
            var actualGraph = (IFieldsGraph) _parentJumpNode.ActualGraph.Clone();
            _localMovePerformer = PerformMoves.DontCareAboutPlayerStateChange(actualGraph);
            _current = new RootedBySelectingWhiteFieldBoardJumpTree(actualGraph);            
            var currentMoves = _current.TraverseDfs(this).Skip(1);
            foreach (var currentMove in currentMoves)
            {
                var newMove = CreateNewMove(currentMove);
                var jumpNode = _parentJumpNode.FollowedBy(newMove);                
                yield return new AlternatingAllJumpsMovesTree(jumpNode, Parent.ChildFactory);                
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

        public void OnEnter(ITree<JumpNode> node, ITreeSearchContinuation treeSearchContinuation)
        {
            _localMovePerformer.Perform(node.Node.LastMove);
        }

        public void OnLeave(ITree<JumpNode> node, ITreeSearchContinuation treeSearchContinuation)
        {
            _localMovePerformer.Undo(node.Node.LastMove);
        }
    }
}