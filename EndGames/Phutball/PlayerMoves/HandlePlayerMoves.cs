namespace EndGames.Phutball.PlayerMoves
{
    public class HandlePlayerMoves : IHandlePlayerMoves
    {
        private IPlayerMoveState _currentMoveState;
        private WaitingForPlayerMoveState _waitForPlayerMoveState;

        public HandlePlayerMoves(WaitingForPlayerMoveState waitingForPlayerMoveState)
        {
            _waitForPlayerMoveState = waitingForPlayerMoveState;
            _currentMoveState = waitingForPlayerMoveState;
        }


        public void PlayerClickedField(Field field)
        {
            _currentMoveState.PlayerClickedField(field);
            _currentMoveState = _currentMoveState.GetNextState();
        }

        public void WaitForPlayerMove()
        {
            _currentMoveState = _waitForPlayerMoveState;
        }
    }
}