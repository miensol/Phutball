using Caliburn.PresentationFramework.Actions;
using Caliburn.PresentationFramework.Screens;
using EndGames.Phutball;
using EndGames.Phutball.Events;
using EndGames.Shell.Presenters.Interfaces;

namespace EndGames.Shell.Presenters
{
    public class CheatsPresenter : Screen, ICheatsPresenter
    {
        private readonly IEventPublisher _eventPublisher;
        private readonly BestMoveApplier _bestMoveApplier;

        public CheatsPresenter(
            IEventPublisher eventPublisher,
            BestMoveApplier bestMoveApplier
            )
        {
            _eventPublisher = eventPublisher;
            _bestMoveApplier = bestMoveApplier;
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
            _bestMoveApplier.PerformAndStore((f) => f.DfsBounded());
        }

        [AsyncAction(BlockInteraction = true)]
        public void MakeMoveBfs()
        {
            _bestMoveApplier.PerformAndStore((f) => f.BfsBounded());
        }

        [AsyncAction(BlockInteraction = true)]
        public void UnboundedMoveDfs()
        {
            _bestMoveApplier.PerformAndStore((f) => f.DfsUnbounded());            
        }
        
        [AsyncAction(BlockInteraction = true)]
        public void UnboundedMoveBfs()
        {
            _bestMoveApplier.PerformAndStore((f) => f.BfsUnbounded());            
        }

        [AsyncAction(BlockInteraction = true)]
        public void MakeMoveAlphaBetaJumps()
        {
            _bestMoveApplier.PerformAndStore((f) => f.AlphaBetaJumps());            
        }
        
        [AsyncAction(BlockInteraction = true)]
        public void MakeMoveAlphaBetaJumpsOrStay()
        {
            _bestMoveApplier.PerformAndStore((f) => f.AlphaBetaJumpsOrStay());            
        }
        
        [AsyncAction(BlockInteraction = true)]
        public void MakeMoveAlphaBeta()
        {
            _bestMoveApplier.PerformAndStore((f) => f.AlphaBeta());            
        }
        
        [AsyncAction(BlockInteraction = true)]
        public void MakeMoveLikeComputer()
        {
            _bestMoveApplier.ChoosePerformAndStore();            
        }
    }
}