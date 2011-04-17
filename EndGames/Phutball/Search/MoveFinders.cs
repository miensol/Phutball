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

        public IMoveFindingStartegy DfsUnbounded()
        {
            return _rawMoveFinders.DfsUnbounded().EnsureMoveIsValid();
        }

        public IMoveFindingStartegy BfsUnbounded()
        {
            return _rawMoveFinders.BfsUnbounded().EnsureMoveIsValid();
        }

        public IMoveFindingStartegy DfsBounded()
        {
            return _rawMoveFinders.DfsBounded().EnsureMoveIsValid();
        }

        public IMoveFindingStartegy BfsBounded()
        {
            return _rawMoveFinders.BfsBounded().EnsureMoveIsValid();
        }

        public IMoveFindingStartegy AlphaBetaJumps()
        {
            return _rawMoveFinders.AlphaBetaJumps().EnsureMoveIsValid();
        }

        public IMoveFindingStartegy AlphaBeta()
        {
            return _rawMoveFinders.AlphaBeta().EnsureMoveIsValid();
        }

        public IMoveFindingStartegy AlphaBetaJumpsOrStay()
        {
            return _rawMoveFinders.AlphaBetaJumpsOrStay().EnsureMoveIsValid();
        }
    }
}