namespace EndGames.Phutball
{
    public class PlayersState : IPlayersState
    {
        private Switch<PlayerOnBoardInfo> _switch;

        public PlayersState()
        {
            Initialize();
        }

        public PlayersState(Player first, Player second)
        {
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

        public void Next()
        {
            _switch.Value.StopMoving();
            _switch = _switch.Swap();
            _switch.Value.StartMoving();

        }

        public void Initialize()
        {
            Initialize(PlayerEnum.First, PlayerEnum.Second);
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

        public static IPlayersState SecondIsOnTheMove()
        {
            return new PlayersState(PlayerEnum.Second, PlayerEnum.First);
        }
    }
}