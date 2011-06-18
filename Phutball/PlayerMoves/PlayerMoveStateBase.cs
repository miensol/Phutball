namespace Phutball.PlayerMoves
{
    public abstract class PlayerMoveStateBase : IPlayerMoveState
    {
        protected IPlayerMoveState NextState;


        public abstract void PlayerClickedField(Field field);

        public IPlayerMoveState GetNextState()
        {
            return NextState;
        }

    }
}