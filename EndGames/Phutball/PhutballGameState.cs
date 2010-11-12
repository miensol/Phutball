using EndGames.Phutball.Events;
using EndGames.Phutball.Player;
using EndGames.Phutball.PlayerMoves;

namespace EndGames.Phutball
{
    public class PhutballGameState
    {
        private PhutballGameStateEnum _currentState;
        private readonly IPlayer _currentPlayer;
        private readonly IEventPublisher _eventPublisher;
        private readonly IPhutballBoard _phutballBoard;
        private readonly IHandlePlayerMoves _handlePlayerMoves;

        public PhutballGameState(IEventPublisher eventPublisher, IPhutballBoard phutballBoard, IHandlePlayerMoves handlePlayerMoves)
        {
            _currentState = PhutballGameStateEnum.NotStarted;
            _currentPlayer = PlayerEnum.First;
            _eventPublisher = eventPublisher;
            _phutballBoard = phutballBoard;
            _handlePlayerMoves = handlePlayerMoves;
        }


        public PhutballGameStateEnum CurrentState
        {
            get { return _currentState; }
        }

        public void CurrentPlayerWon()
        {
            _eventPublisher.Publish(new CurrentPlayerWonEvent{Player = _currentPlayer});
        }

        public void Start()
        {
            _currentState = PhutballGameStateEnum.Started;
            _eventPublisher.Publish(new PhutballGameStarted());
        }

        public void CurrentPlayerClickedField(int fieldId)
        {
            var field = _phutballBoard.GetField(fieldId);
            _handlePlayerMoves.PlayerClickedField(field);
            if(_phutballBoard.IsEndingConfiguration())
            {
                _currentState = PhutballGameStateEnum.CurrentPlayerWon;
                CurrentPlayerWon();
            }
        }
    }
}