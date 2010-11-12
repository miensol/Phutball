namespace EndGames.Phutball.PlayerMoves
{
    public interface IPlayerMoveState
    {
        void PlayerClickedField(Field field);
        IPlayerMoveState GetNextState();
    }
}