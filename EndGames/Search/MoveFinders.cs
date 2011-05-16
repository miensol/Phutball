namespace Phutball.Search
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

        public IMoveFindingStartegy DfsCuttoff()
        {
            return _rawMoveFinders.DfsCuttoff().EnsureMoveIsValid();
        }

        public IMoveFindingStartegy DfsCuttoffToWhite()
        {
            return _rawMoveFinders.DfsCuttoffToWhite().EnsureMoveIsValid();
        }

        public IMoveFindingStartegy OrderByNodesValuesWithCuttofsToWhite()
        {
            return _rawMoveFinders.OrderByNodesValuesWithCuttofsToWhite().EnsureMoveIsValid();
        }

        public IMoveFindingStartegy BfsCuttoffToWhite()
        {
            return _rawMoveFinders.BfsCuttoffToWhite().EnsureMoveIsValid();
        }

        public IMoveFindingStartegy DfsNodesBounded()
        {
            return _rawMoveFinders.DfsNodesBounded().EnsureMoveIsValid();
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

        public IMoveFindingStartegy SmartAlphaBeta()
        {
            return _rawMoveFinders.SmartAlphaBeta().EnsureMoveIsValid();
        }

        public IMoveFindingStartegy BfsNodesBounded()
        {
            return _rawMoveFinders.BfsNodesBounded().EnsureMoveIsValid();
        }

        public IMoveFindingStartegy OrderByNodesValues()
        {
            return _rawMoveFinders.OrderByNodesValues().EnsureMoveIsValid();
        }

        public IMoveFindingStartegy SmartAlphaBetaJumpOrStay()
        {
            return _rawMoveFinders.SmartAlphaBetaJumpOrStay().EnsureMoveIsValid();
        }

        public IMoveFindingStartegy AlphaBetaJumpsOrStay()
        {
            return _rawMoveFinders.AlphaBetaJumpsOrStay().EnsureMoveIsValid();
        }
    }
}