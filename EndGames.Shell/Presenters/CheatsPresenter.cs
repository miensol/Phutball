using Caliburn.PresentationFramework.Actions;
using Caliburn.PresentationFramework.Screens;
using EndGames.Phutball;
using EndGames.Phutball.Events;
using EndGames.Phutball.PlayerMoves;
using EndGames.Phutball.Search;
using EndGames.Shell.Presenters.Interfaces;

namespace EndGames.Shell.Presenters
{
    public class CheatsPresenter : Screen, ICheatsPresenter
    {
        private readonly IMoveFinders _moveFinders;
        private readonly IPlayersState _playersState;
        private readonly IPhutballOptions _phutballOptions;
        private readonly IEventPublisher _eventPublisher;
        private readonly IFieldsGraph _fieldsGraph;
        private readonly IPhutballBoard _phutballBoard;
        private readonly IPerformMoves _performMoves;

        public CheatsPresenter(
            IMoveFinders moveFinders,
            IPlayersState playersState,
            IPhutballOptions phutballOptions,
            IEventPublisher eventPublisher,
            IFieldsGraph fieldsGraph,
            IPhutballBoard phutballBoard,
            IPerformMoves performMoves)
        {
            _moveFinders = moveFinders;
            _playersState = playersState;
            _phutballOptions = phutballOptions;
            _eventPublisher = eventPublisher;
            _fieldsGraph = fieldsGraph;            
            _phutballBoard = phutballBoard;
            _performMoves = performMoves;
            _eventPublisher.Subscribe<PhutballGameStarted>((e) => IsEnabled = true);
            _eventPublisher.Subscribe<PhutballGameEnded>((e) => IsEnabled = false);
        }

        private bool _isEnabled;
        public bool IsEnabled
        {
            get { return _isEnabled; }
            set { _isEnabled = value; 
                NotifyOfPropertyChange(()=> IsEnabled);
            }
        }

        [AsyncAction(BlockInteraction = true)]
        public void MakeMoveDfs()
        {
            PerformBestMove(_moveFinders.DfsBounded(_playersState, _phutballOptions.DfsSearchDepth));
        }

        [AsyncAction(BlockInteraction = true)]
        public void MakeMoveBfs()
        {
            PerformBestMove(_moveFinders.BfsBounded(_playersState, _phutballOptions.BfsSearchDepth));            
        }

        [AsyncAction(BlockInteraction = true)]
        public void UnboundedMoveDfs()
        {
            PerformBestMove(_moveFinders.DfsUnbounded(_playersState));            
        }
        
        [AsyncAction(BlockInteraction = true)]
        public void UnboundedMoveBfs()
        {
            PerformBestMove(_moveFinders.BfsUnbounded(_playersState));            
        }

        [AsyncAction(BlockInteraction = true)]
        public void MakeMoveAlphaBeta()
        {
            var startegy = _moveFinders.AlphaBeta(_playersState, _phutballOptions.AlphaBetaSearchDepth);
            var bestMove = startegy.Search(_fieldsGraph);
            new PerformMovesUntilPlayerOnMoveChange(_eventPublisher, _phutballBoard, _playersState).Perform(bestMove);           
        }

        private void PerformBestMove(IMoveFindingStartegy findingStrategy)
        {
            var bestMove = findingStrategy.Search(_fieldsGraph);
            _performMoves.Perform(bestMove);
        }
    }
}