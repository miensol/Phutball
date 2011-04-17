using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using EndGames.Phutball.Events;
using EndGames.Phutball.Moves;
using EndGames.Phutball.PlayerMoves;
using EndGames.Phutball.Search;
using System.Linq;

namespace EndGames.Phutball
{
    public class LongRunningProcess
    {
        private static CancellationTokenSource _cancelationTokenSurce;

        public static CancellationTokenSource StartNew()
        {
            if(_cancelationTokenSurce != null)
            {
                throw new InvalidOperationException("must clear long running process first");
            }
            _cancelationTokenSurce = new CancellationTokenSource();
            return _cancelationTokenSurce;
        }

        public static CancellationToken Current
        {
            get
            {
                if(_cancelationTokenSurce == null)
                {
                    return CancellationToken.None;
                }
                return _cancelationTokenSurce.Token;
            }
        }

        public static void Clear()
        {
            _cancelationTokenSurce = null;
        }
    }

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
            get { return f => new[] {f.AlphaBetaJumpsOrStay(), f.AlphaBeta()}; }
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