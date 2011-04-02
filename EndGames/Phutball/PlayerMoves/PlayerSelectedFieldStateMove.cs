using System.Collections.Generic;
using EndGames.Phutball.Jumpers;
using EndGames.Phutball.Moves;

namespace EndGames.Phutball.PlayerMoves
{
    public class PlayerSelectedFieldStateMove : PlayerMoveStateBase
    {
        private readonly IPhutballBoard _phutballBoard;
        private readonly IPlayersState _playersState;
        private readonly IPerformMoves _performMoves;
        private readonly Field _selectedField;

        public PlayerSelectedFieldStateMove(
            IPhutballBoard phutballBoard, 
            IPlayersState playersState,
            Field selectedField)
        {
            _phutballBoard = phutballBoard;
            _playersState = playersState;
            _performMoves = new PerformMoves(phutballBoard, _playersState);
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
            _performMoves.Perform(new JumpWhiteStoneMove(_selectedField, jumpedFields, newSelectedField));
            NextState = new PlayerSelectedFieldStateMove(_phutballBoard, _playersState ,newSelectedField);
        }

        private void DeselectSelectedField(Field field)
        {
            _performMoves.Perform(new DeselectWhiteFieldMove(field));
            NextState = new WaitingForPlayerMoveState(_phutballBoard, _playersState);
        }

        private bool ClickedSelectedField(Field field)
        {
            return _selectedField == field;
        }
    }
}