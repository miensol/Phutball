namespace EndGames.Phutball.PlayerMoves
{
    public interface IHandlePlayerMoves
    {
        void PlayerClickedField(Field field);
        void WaitForPlayerMove();
    }
}