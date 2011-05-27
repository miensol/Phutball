using Phutball.Events;

namespace Phutball
{
    public class PlayersState : IPlayersState
    {
        private readonly IEventPublisher _eventPublisher;
        private readonly IPhutballOptions _options;
        private Switch<PlayerOnBoardInfo> _switch;

        public PlayersState(IEventPublisher eventPublisher, IPhutballOptions phutballOptions)
            : this(eventPublisher,phutballOptions, PlayerEnum.First(), PlayerEnum.Second())
        {
        }

        private PlayersState(Player first, Player second):this(EventPublisher.Empty(), new PhutballOptions(), first, second)
        {}

        private PlayersState(IEventPublisher eventPublisher, IPhutballOptions options, Player first, Player second)
        {
            _eventPublisher = eventPublisher;
            _options = options;
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
                _eventPublisher.Publish(new PlayersStateChanged());
                _eventPublisher.Publish(new PlayerOnTheMoveChanged());
            }
        }

        private bool AnyPlayerIsMoving()
        {
            return First.Player.IsOnTheMove || Second.Player.IsOnTheMove;
        }

        private void Start()
        {
            First.ClearTime();
            Second.ClearTime();
            if(_options.SecondStartsGame)
            {
                First.StopMoving();
                Second.StartMoving();                
            }else
            {
                First.StartMoving();
                Second.StopMoving();                
            }            
        }

        public void Stop()
        {
            First.StopMoving();
            Second.StopMoving();
        }

        public IPlayersState CopyRestarted()
        {
            var copyRestarted = new PlayersState(CurrentPlayer, NextPlayer);
            copyRestarted.Start();
            return copyRestarted;
        }

        public void StartVsComputer()
        {
            Initialize(PlayerEnum.First(), PlayerEnum.Computer());
            Start();
        }

        public void StartVsHuman()
        {
            Initialize(PlayerEnum.First(), PlayerEnum.Second());
            Start();
        }

        public static IPlayersState SecondIsOnTheMove()
        {
            return new PlayersState(EventPublisher.Empty(), new PhutballOptions(), PlayerEnum.Second(), PlayerEnum.First());
        }
        
        public static IPlayersState FirstIsOnTheMove()
        {
            return new PlayersState(EventPublisher.Empty(), new PhutballOptions(), PlayerEnum.First(), PlayerEnum.Second());
        }
    }
}