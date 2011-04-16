using System;
using EndGames.Phutball.Moves;
using EndGames.Phutball.PlayerMoves;
using EndGames.Phutball.Search;

namespace EndGames.Phutball
{
    public class BestMoveApplier
    {
        private readonly IMoveFinders _moveFinders;
        private readonly MovesHistory _movesHistory;
        private readonly IFieldsGraph _fieldsGraph;
        private readonly IPerformMoves _performMoves;

        public BestMoveApplier(IMoveFinders moveFinders,  MovesHistory movesHistory, IFieldsGraph fieldsGraph, IPerformMoves performMoves)
        {
            _moveFinders = moveFinders;
            _performMoves = performMoves;
            _movesHistory = movesHistory;
            _fieldsGraph = fieldsGraph;
        }

        public void PerformAndStore(Func<IMoveFinders, IMoveFindingStartegy> chooseStrategy)
        {
            var strategy = chooseStrategy(_moveFinders);
            var bestMove = strategy.Search(_fieldsGraph);
            _movesHistory.PerformAndStore(()=> _performMoves, bestMove);
        }

        public void ChoosePerformAndStore()
        {
            PerformAndStore(f=> f.AlphaBeta());
        }
    }
}