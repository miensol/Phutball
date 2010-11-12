using System.Collections.Generic;
using EndGames.Phutball.Jumpers;
using EndGames.Phutball.Moves;

namespace EndGames.Phutball.PlayerMoves
{
    public class PlayerSelectedFieldStateMove : PlayerMoveStateBase
    {
        private readonly IPhutballBoard _phutballBoard;
        private readonly Field _selectedField;

        public PlayerSelectedFieldStateMove(IPhutballBoard phutballBoard, Field selectedField)
        {
            _phutballBoard = phutballBoard;
            _selectedField = selectedField;
        }

        public override void PlayerClickedField(Field field)
        {
            if (ClickedSelectedField(field))
            {
                DeselectSelectedField(field);
            }
            else
            {
                TryToPerformJump(_selectedField, field);
            }
        }

        private void TryToPerformJump(Field fromField, Field tofield)
        {
            IStoneJumper stoneJumper = _phutballBoard.GetStoneJumper(fromField, tofield);
            IJump validJump = stoneJumper.FindValidJump();
            if (validJump != null)
            {
                PerformJump(tofield, validJump.GetJumpedFields());
            }
            else
            {
                NextState = this;
            }
        }

        private void PerformJump(Field newSelectedField, IEnumerable<Field> jumpedFields)
        {
            new JumpWhiteStoneMove(_selectedField, jumpedFields, newSelectedField).Perform(_phutballBoard);
            NextState = new PlayerSelectedFieldStateMove(_phutballBoard, newSelectedField);
        }

        private void DeselectSelectedField(Field field)
        {
            new DeselectWhiteFieldMove(field).Perform(_phutballBoard);
            NextState = new WaitingForPlayerMoveState(_phutballBoard);
        }

        private bool ClickedSelectedField(Field field)
        {
            return _selectedField == field;
        }
    }
}