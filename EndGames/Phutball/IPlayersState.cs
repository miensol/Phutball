namespace EndGames.Phutball
{
    public interface IPlayersState
    {
        Player CurrentPlayer { get; }
        PlayerOnBoardInfo First { get; }
        PlayerOnBoardInfo Second { get; }
        void Next();
        void Initialize();
        void Start();
        void Stop();
    }
}