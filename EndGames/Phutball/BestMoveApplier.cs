using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using EndGames.Phutball.Moves;
using EndGames.Phutball.PlayerMoves;
using EndGames.Phutball.Search;
using System.Linq;

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
            _movesHistory.PerformAndStore(()=> _performMoves, bestMove.Move);
        }

        public void ChoosePerformAndStore()
        {
            PerformAndStore(f => new[] { f.AlphaBetaJumpsOrStay(), f.AlphaBeta()});
        }

        public void PerformAndStore(Func<IMoveFinders,IEnumerable<IMoveFindingStartegy>> strategiesChooser)
        {
            var strategies = strategiesChooser(_moveFinders);
            var searchTasks = strategies.Select(s => Task.Factory.StartNew(() => s.Search(_fieldsGraph))).ToArray();
            Task.WaitAll(searchTasks);
            var result = searchTasks.Select(t => t.Result).ToList();
            _movesHistory.PerformAndStore(()=>_performMoves , result.MaxBy(r=> r.Score).Move);
        }
    }
}