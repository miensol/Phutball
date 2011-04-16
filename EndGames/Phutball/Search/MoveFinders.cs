using System;

namespace EndGames.Phutball.Search
{
    public class MoveFinders : IMoveFinders
    {
        private readonly RawMoveFinders _rawMoveFinders;

        public MoveFinders(RawMoveFinders rawMoveFinders)
        {
            _rawMoveFinders = rawMoveFinders;
        }

        public IMoveFindingStartegy DfsUnbounded(IPlayersState playersState)
        {
            return _rawMoveFinders.DfsUnbounded(playersState).EnsureMoveIsValid();
        }

        public IMoveFindingStartegy BfsUnbounded(IPlayersState playersState)
        {
            return _rawMoveFinders.BfsUnbounded(playersState).EnsureMoveIsValid();
        }

        public IMoveFindingStartegy DfsBounded(IPlayersState playersState, int maxDepth)
        {
            return _rawMoveFinders.DfsBounded(playersState, maxDepth).EnsureMoveIsValid();
        }

        public IMoveFindingStartegy BfsBounded(IPlayersState playersState, int bfsSearchDepth)
        {
            return _rawMoveFinders.BfsBounded(playersState, bfsSearchDepth).EnsureMoveIsValid();
        }

        public IMoveFindingStartegy AlphaBetaJumps(IPlayersState playersState, IAlphaBetaOptions alphaBetaSearchDepth)
        {
            return _rawMoveFinders.AlphaBetaJumps(playersState, alphaBetaSearchDepth).EnsureMoveIsValid();
        }

        public IMoveFindingStartegy AlphaBeta(IPlayersState playersState, IAlphaBetaOptions alphaBetaSearchDepth)
        {
            return _rawMoveFinders.AlphaBeta(playersState, alphaBetaSearchDepth).EnsureMoveIsValid();
        }
    }
}