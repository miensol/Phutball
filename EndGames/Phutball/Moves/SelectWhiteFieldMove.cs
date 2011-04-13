﻿namespace EndGames.Phutball.Moves
{
    public class SelectWhiteFieldMove : IPhutballMove
    {
        private readonly Field _whiteField;

        public SelectWhiteFieldMove(Field whiteField)
        {
            _whiteField = whiteField;
        }

        public void Perform(PhutballMoveContext context)
        {
            var board = context.FieldsUpdater;
            _whiteField.Select();
            board.UpdateFields(_whiteField);   
        }

        public void Undo(PhutballMoveContext context)
        {
            var board = context.FieldsUpdater;
            _whiteField.DeSelect();
            board.UpdateFields(_whiteField);
            context.SwitchPlayer.SwapMovingPlayers();
        }

        public override string ToString()
        {
            return "Select field {0}".ToFormat(_whiteField);
        }
    }
}