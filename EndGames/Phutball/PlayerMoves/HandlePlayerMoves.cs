namespace EndGames.Phutball.PlayerMoves
{
    public class HandlePlayerMoves : IHandlePlayerMoves
    {
        private IPlayerMoveState _currentMoveState;

        public HandlePlayerMoves(WaitingForPlayerMoveState waitingForPlayerMoveState)
        {
            _currentMoveState = waitingForPlayerMoveState;
        }


        public void PlayerClickedField(Field field)
        {
            _currentMoveState.PlayerClickedField(field);
            _currentMoveState = _currentMoveState.GetNextState();
        }

    }
}