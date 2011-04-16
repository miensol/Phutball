using System.Linq;
using EndGames.Phutball.Moves;

namespace EndGames.Phutball.Search
{
    public class RawMoveFinders : IMoveFinders
    {
        private readonly MovesFactory _movesFactory;
        private readonly IPlayersState _playersState;
        private readonly IPhutballOptions _phutballOptions;

        public RawMoveFinders(MovesFactory movesFactory, IPlayersState playersState, IPhutballOptions phutballOptions)
        {
            _movesFactory = movesFactory;
            _playersState = playersState;
            _phutballOptions = phutballOptions;
        }

        public IMoveFindingStartegy DfsUnbounded()
        {
            return new BruteForceMoveFindingStartegy(
                new EmptyNodeVisitor<JumpNode>(), 
                (visitor)=> new DfsSearch<JumpNode>(visitor), _playersState,
                _movesFactory);
        }

        public IMoveFindingStartegy BfsUnbounded()
        {
            return new BruteForceMoveFindingStartegy(
                new EmptyNodeVisitor<JumpNode>(),
                (vistor) => new BfsSearch<JumpNode>(vistor),
                _playersState,
                _movesFactory);
        }

        public IMoveFindingStartegy DfsBounded()
        {
            return new BoundedDepthMoveFindingStrategy(
                _playersState, 
                _phutballOptions.DfsSearchDepth, 
                _movesFactory, (vistor)=> new DfsSearch<JumpNode>(vistor));
        }

        public IMoveFindingStartegy BfsBounded()
        {
            return new BoundedDepthMoveFindingStrategy(_playersState, _phutballOptions.BfsSearchDepth, _movesFactory,
                                                       (vistor) => new BfsSearch<JumpNode>(vistor));
        }

        public IMoveFindingStartegy AlphaBetaJumps()
        {
            return new AlphaBetaMoveFindingStrategy(_playersState, _phutballOptions.AlphaBeta,
                    (graph)=> new AlternatingJumpsMovesTree(new JumpNode(graph, new EmptyPhutballMove()),
                        (parent) => new AllAlternatigJumpsTreeCollection(parent, _phutballOptions.AlphaBeta))
                );
        }

        public IMoveFindingStartegy AlphaBeta()
        {
            return new AlphaBetaMoveFindingStrategy(
                _playersState, _phutballOptions.AlphaBeta,
                (graph) => new AlternatingJumpsMovesTree( JumpNode.Empty(graph),
                                    (parent) => new AllAlternatigJumpsTreeCollection(parent, _phutballOptions.AlphaBeta)
                                                    .Concat(new PlaceBlackStonesAroundWhite(parent))
                                    )
            );
        }
    }
}