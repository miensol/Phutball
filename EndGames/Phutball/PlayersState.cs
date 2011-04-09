using System;
using EndGames.Phutball.Events;

namespace EndGames.Phutball
{
    public class PlayersState : IPlayersState
    {
        private readonly IEventPublisher _eventPublisher;
        private Switch<PlayerOnBoardInfo> _switch;

        public PlayersState():this(EventPublisher.Empty())
        {
            
        }

        public PlayersState(IEventPublisher eventPublisher)
            : this(eventPublisher,PlayerEnum.First, PlayerEnum.Second)
        {
        }

        public PlayersState(Player first, Player second):this(EventPublisher.Empty(), first, second)
        {}

        public PlayersState(IEventPublisher eventPublisher, Player first, Player second)
        {
            _eventPublisher = eventPublisher;
            Initialize(first, second);
        }

        private void Initialize(Player first, Player second)
        {
            First = new PlayerOnBoardInfo(first);
            Second = new PlayerOnBoardInfo(second);
            _switch = new Switch<PlayerOnBoardInfo>(First, Second);
            first.IsOnTheMove = false;
            second.IsOnTheMove = false;
        }

        public Player CurrentPlayer
        {
            get { return _switch.Value.Player; }
        }

        public PlayerOnBoardInfo First { get; set; }

        public PlayerOnBoardInfo Second { get; set; }

        public Player NextPlayer
        {
            get { return _switch.Swap().Value.Player; }
        }

        public void SwapMovingPlayers()
        {
            if(AnyPlayerIsMoving())
            {
                _switch.Value.StopMoving();
                _switch = _switch.Swap();
                _switch.Value.StartMoving();   
                _eventPublisher.Publish(new PlayerOnTheMoveChanged());
            }
        }

        private bool AnyPlayerIsMoving()
        {
            return First.Player.IsOnTheMove || Second.Player.IsOnTheMove;
        }

        public void Start()
        {
            First.ClearTime();
            Second.ClearTime();
            First.StartMoving();
            Second.StopMoving();
        }

        public void Stop()
        {
            First.StopMoving();
            Second.StopMoving();
        }

        public IPlayersState TempCopy()
        {
            return new PlayersState(CurrentPlayer, NextPlayer);
        }

        public static IPlayersState SecondIsOnTheMove()
        {
            return new PlayersState(EventPublisher.Empty(), PlayerEnum.Second, PlayerEnum.First);
        }
    }
}