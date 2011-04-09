namespace EndGames.Phutball
{
    public interface IPlayersState 
    {
        Player CurrentPlayer { get; }
        PlayerOnBoardInfo First { get; }
        PlayerOnBoardInfo Second { get; }
        Player NextPlayer { get; }
        void SwapMovingPlayers();
        void Start();
        void Stop();
        IPlayersState TempCopy();
    }
}