namespace EndGames.Phutball.Moves
{
    public class PlaceBlackStoneMove : IMove<IFieldsUpdater>
    {
        private readonly Field _field;

        public PlaceBlackStoneMove(Field field)
        {
            _field = field;
        }

        public void Perform(IFieldsUpdater board)
        {
            _field.PlaceBlackStone();
            board.UpdateFields(_field);
        }

        public void Undo(IFieldsUpdater board)
        {
            _field.RemoveStone();
            board.UpdateFields(_field);
        }
    }
}