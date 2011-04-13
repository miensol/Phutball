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
        private readonly MovesHistory _movesHistory;

        public PlayerSelectedFieldStateMove(
            IPhutballBoard phutballBoard, 
            IPlayersState playersState,
            Field selectedField, 
            MovesHistory movesHistory)
        {
            _phutballBoard = phutballBoard;
            _movesHistory = movesHistory;
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
            _movesHistory.PerformAndStore(() => _performMoves, new JumpWhiteStoneMove(_selectedField, jumpedFields, newSelectedField));
            NextState = new PlayerSelectedFieldStateMove(_phutballBoard, _playersState , newSelectedField, _movesHistory);
        }

        private void DeselectSelectedField(Field field)
        {
            _movesHistory.PerformAndStore(() => _performMoves, new DeselectWhiteFieldMove(field));
            NextState = new WaitingForPlayerMoveState(_phutballBoard, _movesHistory, _playersState);
        }

        private bool ClickedSelectedField(Field field)
        {
            return _selectedField == field;
        }
    }
}