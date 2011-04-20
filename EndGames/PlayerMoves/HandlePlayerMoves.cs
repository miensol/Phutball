namespace Phutball.PlayerMoves
{
    public class HandlePlayerMoves : IHandlePlayerMoves
    {
        private IPlayerMoveState _currentMoveState;
        private WaitingForPlayerMoveState _waitForPlayerMoveState;
        private readonly IPlayersState _playersState;

        public HandlePlayerMoves(WaitingForPlayerMoveState waitingForPlayerMoveState, IPlayersState playersState)
        {
            _waitForPlayerMoveState = waitingForPlayerMoveState;
            _playersState = playersState;
            _currentMoveState = _waitForPlayerMoveState;
        }


        public void PlayerClickedField(Field field)
        {
            if(_playersState.CurrentPlayer.IsAHuman)
            {
                _currentMoveState.PlayerClickedField(field);
                _currentMoveState = _currentMoveState.GetNextState();   
            }
        }

        public void WaitForPlayerMove()
        {
            _currentMoveState = _waitForPlayerMoveState;
        }
    }
}