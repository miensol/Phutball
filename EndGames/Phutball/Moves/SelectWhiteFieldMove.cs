namespace EndGames.Phutball.Moves
{
    public class SelectWhiteFieldMove : IMove<IFieldsUpdater>
    {
        private readonly Field _whiteField;

        public SelectWhiteFieldMove(Field whiteField)
        {
            _whiteField = whiteField;
        }

        public void Perform(IFieldsUpdater board)
        {
            _whiteField.Select();
            board.UpdateFields(_whiteField);
        }

        public void Undo(IFieldsUpdater board)
        {
            _whiteField.DeSelect();
            board.UpdateFields(_whiteField);
        }

        public override string ToString()
        {
            return "Select field {0}".ToFormat(_whiteField);
        }
    }
}