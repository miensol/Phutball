using EndGames.Phutball.Moves;

namespace EndGames.Phutball.PlayerMoves
{
    public class WaitingForPlayerMoveState : PlayerMoveStateBase
    {
        private readonly IPhutballBoard _phutballBoard;

        public WaitingForPlayerMoveState(IPhutballBoard phutballBoard)
        {
            _phutballBoard = phutballBoard;
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
            new SelectWhiteFieldMove(field).Perform(_phutballBoard);
            NextState = new PlayerSelectedFieldStateMove(_phutballBoard, field);
        }


        private void PlaceBlackStoneOnField(Field field)
        {
            new PlaceBlackStoneMove(field).Perform(_phutballBoard);
            NextState = this;
        }

        private bool CanPlaceBlackStone(Field field)
        {
            return _phutballBoard.CanPlaceBlackStone(field);
        }

      
    }
}