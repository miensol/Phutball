using System;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using EndGames.Phutball.Events;
using EndGames.Phutball.PlayerMoves;

namespace EndGames.Phutball.Moves
{            
    public class MovesHistory
    {
        private ObservableCollection<MoveHistoryItem> _done = new ObservableCollection<MoveHistoryItem>();
        private readonly IEventPublisher _eventPublisher;
        private ObservableCollection<MoveHistoryItem> _undone = new ObservableCollection<MoveHistoryItem>();

        public MovesHistory(IEventPublisher eventPublisher)
        {
            _eventPublisher = eventPublisher;
            _eventPublisher.Subscribe<PhutballGameStarted>(OnGameStart);            
            _eventPublisher.Subscribe<PhutballGameEnded>(OnGameEnd);
            _done.CollectionChanged += RaiseHistoryChanged;
            _undone.CollectionChanged += RaiseHistoryChanged;
        }

        public bool CanUndo
        {
            get { return _done.Any(); }            
        }

        public bool CanRedo
        {
            get { return _undone.Any(); }
        }

        private void RaiseHistoryChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            _eventPublisher.Publish(new MovesHistoryChanged());
        }

        private void OnGameEnd(PhutballGameEnded obj)
        {
            ClearHistory();
        }

        private void ClearHistory()
        {
            _done.Clear();
            _undone.Clear();
        }

        private void OnGameStart(PhutballGameStarted obj)
        {
            ClearHistory();
        }

        public void PerformAndStore(Func<IPerformMoves> movePerfomer, IPhutballMove move)
        {
            var item = MoveHistoryItem.Item(movePerfomer, move);
            item.Perform();
            _done.Add(item);
            _undone.Clear();
        }

        public void Undo()
        {
            var moveToUndo = _done.Last();
            _done.Remove(moveToUndo);
            moveToUndo.Undo();
            _undone.Add(moveToUndo);
        }

        public void Redo()
        {
            var moveToDo = _undone.Last();
            _undone.Remove(moveToDo);
            moveToDo.Perform();
            _done.Add(moveToDo);
        }
    }
}