using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Phutball.Events;
using Phutball.PlayerMoves;

namespace Phutball
{
    public class PhutballGameState
    {
        private PhutballGameStateEnum _currentState;
        private readonly IEventPublisher _eventPublisher;
        private readonly IPhutballBoard _phutballBoard;
        private readonly IPlayersState _playersState;
        private readonly BestMoveApplier _bestMoveApplier;
        private IHandlePlayerMoves _handlePlayerMoves;
        private Func<IHandlePlayerMoves> _handlePlayerMovesFactory;
        private CancellationTokenSource _cancenTokenSource ;
        private Task _moveTask;

        public PhutballGameState(
            IEventPublisher eventPublisher, 
            IPhutballBoard phutballBoard, 
            IPlayersState playersState,
            BestMoveApplier bestMoveApplier,
            Func<IHandlePlayerMoves> handlePlayerMovesFactory)
        {
            _currentState = PhutballGameStateEnum.NotStarted;
            _eventPublisher = eventPublisher;
            _phutballBoard = phutballBoard;
            _playersState = playersState;
            _bestMoveApplier = bestMoveApplier;
            _handlePlayerMovesFactory = handlePlayerMovesFactory;
            _handlePlayerMoves = handlePlayerMovesFactory();
            _eventPublisher.Subscribe<CurrentPlayerWonEvent>((e)=> CurrentPlayerWon());
            _eventPublisher.Subscribe<PlayerOnTheMoveChanged>(OnPlayerOnTheMoveChanged);
            _eventPublisher.Subscribe<ComputerStartedMoving>((e)=> LongRunningProcess.Clear());
        }

        private void OnPlayerOnTheMoveChanged(PlayerOnTheMoveChanged change)
        {
            _handlePlayerMoves = _handlePlayerMovesFactory();          
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

        public void StartWithComputer()
        {
            _playersState.StartVsComputer();
            StartBoardAndPlayers();
        }

        public void StartVsHuman()
        {
            _playersState.StartVsHuman();
            StartBoardAndPlayers();
        }

        private void StartBoardAndPlayers()
        {
            _phutballBoard.Initialize();
            _handlePlayerMoves.WaitForPlayerMove();
            _currentState = PhutballGameStateEnum.Started;
            _eventPublisher.Publish(new PhutballGameStarted());
            _eventPublisher.Publish(new PlayersStateChanged());
            PerformMoveAsComputerIfNeeded();
        }

        public void CurrentPlayerClickedField(int fieldId)
        {
            var field = _phutballBoard.GetField(fieldId);
            _handlePlayerMoves.PlayerClickedField(field);
            _eventPublisher.Publish(new PlayersStateChanged());
            PerformMoveAsComputerIfNeeded();
        }

        private void PerformMoveAsComputerIfNeeded()
        {
            if (_playersState.CurrentPlayer.IsAComputer)
            {
                _cancenTokenSource = LongRunningProcess.StartNew();
                var token = _cancenTokenSource.Token;
                _moveTask = Task.Factory.StartNew(() => _bestMoveApplier.ChooseAndPerform(token), token);
            }
        }

        public void Restart()
        {
            CancelComputerMove();
            _currentState = PhutballGameStateEnum.NotStarted;
            _playersState.Stop();
            _eventPublisher.Publish(new PlayersStateChanged());
            _eventPublisher.Publish(new PhutballGameEnded());
        }

        private void CancelComputerMove()
        {
            if(_moveTask != null)
            {
                _cancenTokenSource.Cancel();
                try
                {
                    _moveTask.Wait(_cancenTokenSource.Token);
                }
                catch(OperationCanceledException)
                {
                    _moveTask.Wait();
                }
                _moveTask = null;
                LongRunningProcess.Clear();
                _eventPublisher.Publish(new ComputerStartedMoving());
            }
        }
    }
}