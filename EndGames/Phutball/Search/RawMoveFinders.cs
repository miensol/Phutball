﻿using EndGames.Phutball.PlayerMoves;

namespace EndGames.Phutball.Search
{
    public class RawMoveFinders : IMoveFinders
    {
        private readonly MovesFactory _movesFactory;

        public RawMoveFinders(MovesFactory movesFactory)
        {
            _movesFactory = movesFactory;
        }

        public IMoveFindingStartegy DfsUnbounded(IPlayersState playersState)
        {
            return new BruteForceMoveFindingStartegy(new EmptyNodeVisitor<JumpNode>(), 
                (visitor)=> new DfsSearch<JumpNode>(visitor), playersState,
                _movesFactory);
        }

        public IMoveFindingStartegy BfsUnbounded(IPlayersState playersState)
        {
            return new BruteForceMoveFindingStartegy(new EmptyNodeVisitor<JumpNode>(),
                                                     (vistor) => new BfsSearch<JumpNode>(vistor),
                                                     playersState,
                                                     _movesFactory);
        }

        public IMoveFindingStartegy DfsBounded(IPlayersState playersState, int maxDepth)
        {
            return new BoundedDepthMoveFindingStrategy(playersState, maxDepth, _movesFactory, (vistor)=> new DfsSearch<JumpNode>(vistor));
        }

        public IMoveFindingStartegy BfsBounded(IPlayersState playersState, int bfsSearchDepth)
        {
            return new BoundedDepthMoveFindingStrategy(playersState, bfsSearchDepth, _movesFactory,
                                                       (vistor) => new BfsSearch<JumpNode>(vistor));
        }

        public IMoveFindingStartegy AlphaBeta(IPlayersState playersState, int alphaBetaSearchDepth)
        {
            return new AlphaBetaMoveFindingStrategy(playersState, alphaBetaSearchDepth);
        }
    }
}