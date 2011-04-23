using System;
using System.Linq;
using Phutball.Moves;
using Phutball.Search.BoardValues;

namespace Phutball.Search
{
    public class RawMoveFinders : IMoveFinders
    {
        private readonly MovesFactory _movesFactory;
        private readonly Func<IPlayersState> _playersStateCopy;
        private readonly IPhutballOptions _phutballOptions;

        public RawMoveFinders(MovesFactory movesFactory, IPlayersState playersState, IPhutballOptions phutballOptions)
        {
            _movesFactory = movesFactory;
            _playersStateCopy  = ()=>playersState.CopyRestarted();
            _phutballOptions = phutballOptions;
        }

        public IMoveFindingStartegy DfsUnbounded()
        {
            return new BruteForceMoveFindingStartegy(
                new EmptyNodeVisitor<JumpNode>(),
                (visitor) => new DfsSearch<JumpNode>(visitor), _playersStateCopy(),
                _movesFactory);
        }

        public IMoveFindingStartegy BfsUnbounded()
        {
            return new BruteForceMoveFindingStartegy(
                new EmptyNodeVisitor<JumpNode>(),
                (vistor) => new BfsSearch<JumpNode>(vistor),
                _playersStateCopy(),
                _movesFactory);
        }

        public IMoveFindingStartegy DfsBounded()
        {
            return new BoundedDepthMoveFindingStrategy(
                _playersStateCopy(), 
                _phutballOptions.DfsSearchDepth, 
                _movesFactory, (vistor)=> new DfsSearch<JumpNode>(vistor));
        }

        public IMoveFindingStartegy DfsCuttoff()
        {
            return new CuttofsMoveFindingStrategy(
                new EmptyNodeVisitor<JumpNode>(),
                (vistors) => new DfsSearch<JumpNode>(vistors),
                _playersStateCopy(),
                _movesFactory
                )
                       {
                           CuttoffToTarget = true,
                           MaxVisitedNodes = _phutballOptions.DfsMaxVistedNodes
                       };
        }

        public IMoveFindingStartegy DfsCuttoffToWhite()
        {
            return new CuttofsMoveFindingStrategy(
                    new EmptyNodeVisitor<JumpNode>(),
                    (vistors)=> new DfsSearch<JumpNode>(vistors),
                    _playersStateCopy(),
                    _movesFactory
                )
                       {
                           MaxVisitedNodes = _phutballOptions.DfsMaxVistedNodes
                       };
        }

        public IMoveFindingStartegy DfsNodesBounded()
        {
            return new BoundedVistedNodesCountStrategy(
                    _playersStateCopy(), 
                    _phutballOptions.DfsMaxVistedNodes,
                    _movesFactory, (vistors)=> new DfsSearch<JumpNode>(vistors)
                );
        }

        public IMoveFindingStartegy BfsBounded()
        {
            return new BoundedDepthMoveFindingStrategy(_playersStateCopy(), _phutballOptions.BfsSearchDepth, _movesFactory,
                                                       (vistor) => new BfsSearch<JumpNode>(vistor));
        }

        public IMoveFindingStartegy AlphaBetaJumps()
        {
            var playersStateCopy = _playersStateCopy();
            return new AlphaBetaMoveFindingStrategy(playersStateCopy, _phutballOptions.AlphaBeta,
                    (graph)=> new AlternatingJumpsMovesTree(new JumpNode(graph, new EmptyPhutballMove()),
                        (parent) => new AllAlternatigJumpsTreeCollection(parent, _phutballOptions.AlphaBeta))
                );
        }

        public IMoveFindingStartegy AlphaBetaJumpsOrStay()
        {
            var playersStateCopy = _playersStateCopy();
            var options = _phutballOptions.AlphaBeta.AllowNoMoveToBeTaken();
            return new AlphaBetaMoveFindingStrategy(playersStateCopy, options,
                    (graph) => new AlternatingJumpsMovesTree(new JumpNode(graph, new EmptyPhutballMove()),
                        (parent) => new AllAlternatigJumpsTreeCollection(parent, options))
                );
        }

        public IMoveFindingStartegy AlphaBeta()
        {
            var playersStateCopy = _playersStateCopy();
            return new AlphaBetaMoveFindingStrategy(
                playersStateCopy, _phutballOptions.AlphaBeta,
                (graph) => new AlternatingJumpsMovesTree( JumpNode.Empty(graph),
                                    (parent) => new AllAlternatigJumpsTreeCollection(parent, _phutballOptions.AlphaBeta)
                                            .Concat(new PlaceBlackStonesAroundWhite(parent, playersStateCopy, _phutballOptions.AlphaBeta))
                                    )
            );
        }

        public IMoveFindingStartegy BfsNodesBounded()
        {
            return new BoundedVistedNodesCountStrategy(_playersStateCopy(), _phutballOptions.BfsMaxVisitedNodes,
                                                       _movesFactory,
                                                       (vistors) => new BfsSearch<JumpNode>(vistors));
        }

        public IMoveFindingStartegy OrderByNodesValues()
        {
            return new BoundedVistedNodesCountStrategy(
                _playersStateCopy(),
                _phutballOptions.BfsMaxVisitedNodes,
                _movesFactory,
                (visotors, performer, target) => new BfsSearch<JumpNode>(visotors,
                new BestValueAddRemoveCollection(performer,new WhiteStoneToBorderDistanceValue(target)))
            );
        }
    }
}