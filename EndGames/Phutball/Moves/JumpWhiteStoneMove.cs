using System;
using System.Collections.Generic;
using System.Linq;

namespace EndGames.Phutball.Moves
{
    public class JumpWhiteStoneMove : IMove<IFieldsUpdater>
    {
        private Field _selectedField;
        private readonly IEnumerable<Field> _jumpedFields;
        private readonly Field _newSelectedField;

        public JumpWhiteStoneMove(Field selectedField, IEnumerable<Field> jumpedFields, Field newSelectedField)
        {
            _selectedField = selectedField;
            _jumpedFields = jumpedFields;
            _newSelectedField = newSelectedField;
        }

        public void Perform(IFieldsUpdater board)
        {
            _selectedField.DeSelect();
            _jumpedFields.Each(field => field.RemoveStone());
            _newSelectedField.Select();
            _selectedField.RemoveStone();
            _newSelectedField.PlaceWhiteStone();
            NotifyOfFieldsStateChange(board);
        }

        private void NotifyOfFieldsStateChange(IFieldsUpdater board)
        {
            IEnumerable<Field> changedFields = _jumpedFields.Union(new[] { _selectedField, _newSelectedField });
            board.UpdateFields(changedFields.ToArray());
        }

        public void Undo(IFieldsUpdater board)
        {
            _selectedField.Select();
            _jumpedFields.Each(field => field.PlaceBlackStone());
            _newSelectedField.DeSelect();
            _selectedField.PlaceWhiteStone();
            _newSelectedField.RemoveStone();
            NotifyOfFieldsStateChange(board);
        }
    }
}