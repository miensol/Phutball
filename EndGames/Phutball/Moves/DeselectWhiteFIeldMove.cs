namespace EndGames.Phutball.Moves
{
    public class DeselectWhiteFieldMove : IMove<IFieldsUpdater>
    {
        private readonly Field _field;

        public DeselectWhiteFieldMove(Field field)
        {
            _field = field;
        }

        public void Perform(IFieldsUpdater board)
        {
            _field.DeSelect();
            board.UpdateFields(_field);
        }

        public void Undo(IFieldsUpdater board)
        {
            _field.Select();
            board.UpdateFields(_field);
        }
    }
}