using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;
using Phutball.Events;
using Phutball.Moves;
using Phutball.PlayerMoves;
using Phutball.Search;

namespace Phutball
{
    public class BestMoveApplier
    {
        private readonly IMoveFinders _moveFinders;
        private readonly MovesHistory _movesHistory;
        private readonly IFieldsGraph _fieldsGraph;
        private readonly IPerformMoves _performMoves;
        private IEventPublisher _eventPublisher;

        public BestMoveApplier(IMoveFinders moveFinders,  MovesHistory movesHistory, IFieldsGraph fieldsGraph, IPerformMoves performMoves, IEventPublisher eventPublisher)
        {
            _moveFinders = moveFinders;
            _eventPublisher = eventPublisher;
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
            ChooseBestMoveAndApply(CancellationToken.None,
                                   (bestMove) => _movesHistory.PerformAndStore(() => _performMoves, bestMove));
        }

        private Func<IMoveFinders, IEnumerable<IMoveFindingStartegy>> ComputerStrategies
        {
            get { return f => new[] {f.SmartAlphaBeta(), f.SmartAlphaBetaJumpOrStay()}; }
        }

        public void ChooseAndPerform(CancellationToken cancelComputerMove)
        {
            ChooseBestMoveAndApply(cancelComputerMove, (resultMOve)=> _performMoves.Perform(resultMOve));
        }

        private void ChooseBestMoveAndApply(CancellationToken cancelComputerMove, Action<IPhutballMove> jobToDo)
        {
            _eventPublisher.Publish(new ComputerStartedMoving());
            _movesHistory.Clear();
            var strategies = ComputerStrategies(_moveFinders);
            var searchTasks = strategies.Select(s => Task.Factory.StartNew(() => s.Search(_fieldsGraph), cancelComputerMove)).ToArray();            
            try
            {                
                Task.WaitAll(searchTasks, cancelComputerMove);
                if (cancelComputerMove.IsCancellationRequested == false)
                {
                    var result = searchTasks.Select(t => t.Result).ToList();
                    jobToDo(result.MaxBy(r => r.Score).Move);
                    _eventPublisher.Publish(new ComputerStopedMoving());
                }                
            }
            catch (OperationCanceledException)
            {
                Task.WaitAll(searchTasks);                
            }            
        }
    }
}