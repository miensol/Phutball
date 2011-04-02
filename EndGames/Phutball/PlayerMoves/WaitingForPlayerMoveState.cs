using EndGames.Phutball.Moves;

namespace EndGames.Phutball.PlayerMoves
{
    public class WaitingForPlayerMoveState : PlayerMoveStateBase
    {
        private readonly IPhutballBoard _phutballBoard;
        private readonly IPlayersState _playersState;
        private readonly IPerformMoves _performMoves;

        public WaitingForPlayerMoveState(IPhutballBoard phutballBoard, IPlayersState playersState)
        {
            _phutballBoard = phutballBoard;
            _playersState = playersState;
            _performMoves = new PerformMoves(phutballBoard, playersState);
        }

        public override void PlayerClickedField(Field field)
        {
            if (CanPlaceBlackStone(field))
            {
                PlaceBlackStoneOnField(field);
            }
            else
            {
                TryToSelectField(field);
            }
        }

        private void TryToSelectField(Field field)
        {
            if (field.CanSelect)
            {
                SelectField(field);
            }
            else
            {
                NextState = this;
            }
        }

        private void SelectField(Field field)
        {
            _performMoves.Perform(new SelectWhiteFieldMove(field));
            NextState = new PlayerSelectedFieldStateMove(_phutballBoard, _playersState, field);
        }


        private void PlaceBlackStoneOnField(Field field)
        {
            _performMoves.Perform(new PlaceBlackStoneMove(field));
            NextState = this;
        }

        private bool CanPlaceBlackStone(Field field)
        {
            return _phutballBoard.CanPlaceBlackStone(field);
        }

      
    }
}