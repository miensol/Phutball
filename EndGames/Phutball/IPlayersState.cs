namespace EndGames.Phutball
{
    public interface IPlayersState : IPlayersSwapper
    {
        Player CurrentPlayer { get; }
        PlayerOnBoardInfo First { get; }
        PlayerOnBoardInfo Second { get; }
        Player NextPlayer { get; }
        void Start();
        void Stop();
        IPlayersState CopyRestarted();
    }
}