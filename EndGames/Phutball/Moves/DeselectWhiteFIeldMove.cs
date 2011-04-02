namespace EndGames.Phutball.Moves
{
    public class DeselectWhiteFieldMove : IPhutballMove
    {
        private readonly Field _field;

        public DeselectWhiteFieldMove(Field field)
        {
            _field = field;
        }

        public void Perform(PhutballMoveContext context)
        {
            var board = context.FieldsUpdater;
            _field.DeSelect();
            board.UpdateFields(_field);
            context.SwitchPlayer.Next();
        }

        public void Undo(PhutballMoveContext context)
        {
            var board = context.FieldsUpdater;
            _field.Select();
            board.UpdateFields(_field);
            context.SwitchPlayer.Next();
        }
    }
}