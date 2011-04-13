using Caliburn.PresentationFramework.Filters;
using Caliburn.PresentationFramework.Screens;
using EndGames.Phutball.Events;
using EndGames.Phutball.Moves;

namespace EndGames.Shell.Presenters
{
    public class MovesHistoryPresenter : Screen 
    {
        private readonly MovesHistory _movesHistory;
        private readonly IEventPublisher _eventPublisher;
        private bool _undoEnabled;

        public MovesHistoryPresenter(MovesHistory movesHistory, IEventPublisher eventPublisher)
        {
            _movesHistory = movesHistory;
            _eventPublisher = eventPublisher;
            _eventPublisher.Subscribe<MovesHistoryChanged>(OnMovesHistoryChanged);
        }

        private void OnMovesHistoryChanged(MovesHistoryChanged obj)
        {
            NotifyOfPropertyChange(()=> CanRedo);
            NotifyOfPropertyChange(()=> CanUndo);
        }

        [Preview("CanUndo", AffectsTriggers = true)]
        public void Undo()
        {
            _movesHistory.Undo();
        }
        
        public bool CanUndo
        {
            get {return _movesHistory.CanUndo;}
        }
    
        [Preview("CanRedo", AffectsTriggers = true)]
        public void Redo()
        {
            _movesHistory.Redo();
        }

        public bool CanRedo
        {
            get{return _movesHistory.CanRedo;}
        }

       
    }
}