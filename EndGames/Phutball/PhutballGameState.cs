using System;
using System.Collections.Generic;
using EndGames.Phutball.Events;
using EndGames.Phutball.PlayerMoves;

namespace EndGames.Phutball
{
    public class PhutballGameState
    {
        private PhutballGameStateEnum _currentState;
        private readonly IEventPublisher _eventPublisher;
        private readonly IPhutballBoard _phutballBoard;
        private readonly IPlayersState _playersState;
        private IHandlePlayerMoves _handlePlayerMoves;

        public PhutballGameState(
            IEventPublisher eventPublisher, 
            IPhutballBoard phutballBoard, 
            IPlayersState playersState,
            Func<IHandlePlayerMoves> handlePlayerMoves)
        {
            _currentState = PhutballGameStateEnum.NotStarted;
            _eventPublisher = eventPublisher;
            _phutballBoard = phutballBoard;
            _playersState = playersState;
            _handlePlayerMoves = handlePlayerMoves();
            _eventPublisher.Subscribe<CurrentPlayerWonEvent>((e)=> CurrentPlayerWon());
            _eventPublisher.Subscribe<PlayerOnTheMoveChanged>(change => _handlePlayerMoves = handlePlayerMoves());
        }


        public PhutballGameStateEnum CurrentState
        {
            get { return _currentState; }
        }

        public IEnumerable<PlayerOnBoardInfo> Players
        {
            get { return new[]{ _playersState.First, _playersState.Second }; }
        }

        public void CurrentPlayerWon()
        {
            _currentState = PhutballGameStateEnum.CurrentPlayerWon;
            _playersState.Stop();
            _eventPublisher.Publish(new PhutballGameEnded());
            _eventPublisher.Publish(new PlayersStateChanged());
        }

        public void Start()
        {            
            _phutballBoard.Initialize();
            _playersState.Start();
            _handlePlayerMoves.WaitForPlayerMove();
            _currentState = PhutballGameStateEnum.Started;
            _eventPublisher.Publish(new PhutballGameStarted());
            _eventPublisher.Publish(new PlayersStateChanged());
        }

        public void CurrentPlayerClickedField(int fieldId)
        {
            var field = _phutballBoard.GetField(fieldId);
            _handlePlayerMoves.PlayerClickedField(field);
            _eventPublisher.Publish(new PlayersStateChanged());            
        }

        public void Restart()
        {
            _currentState = PhutballGameStateEnum.NotStarted;
            _playersState.Stop();
            _eventPublisher.Publish(new PlayersStateChanged());
            _eventPublisher.Publish(new PhutballGameEnded());
        }
    }
}