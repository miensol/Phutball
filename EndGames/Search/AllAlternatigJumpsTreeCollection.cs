using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Phutball.Moves;
using Phutball.PlayerMoves;
using Phutball.Search.Visitors;

namespace Phutball.Search
{
    public class AllAlternatigJumpsTreeCollection : IEnumerable<IJumpNodeTreeWithFactory>
    {
        private readonly IAlphaBetaOptions _alphaBetaOptions;
        private readonly JumpNode _parentJumpNode;
        private ISearchNodeVisitor<JumpNode> _afterMoveVisitor;

        public AllAlternatigJumpsTreeCollection(IJumpNodeTreeWithFactory parent, IAlphaBetaOptions alphaBetaOptions)
            :this(parent, alphaBetaOptions, new EmptyNodeVisitor<JumpNode>())
        {
        
        }

        public AllAlternatigJumpsTreeCollection(IJumpNodeTreeWithFactory parent, IAlphaBetaOptions alphaBetaOptions, ISearchNodeVisitor<JumpNode> afterMoveVisitor)
        {
            _alphaBetaOptions = alphaBetaOptions;
            _afterMoveVisitor = afterMoveVisitor;
            Parent = parent;
            _parentJumpNode = Parent.Node;            
        }

        private IJumpNodeTreeWithFactory Parent { get; set; }

        public IEnumerator<IJumpNodeTreeWithFactory> GetEnumerator()
        {            
            var actualGraph = (IFieldsGraph) _parentJumpNode.ActualGraph.Clone();
            var localMovePerformer = PerformMoves.DontCareAboutPlayerStateChange(actualGraph);
            var current = new RootedBySelectingWhiteFieldBoardJumpTree(actualGraph);
            var visitor = new PerformMovesNodeVisitor(localMovePerformer).FollowedBy(_afterMoveVisitor);
            var currentMoves = current.TraverseDfs(visitor, _alphaBetaOptions.JumpsMaxDepth)
                .Skip(_alphaBetaOptions.SkipShortMoves);
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