using System;

namespace EndGames.Phutball.Moves
{
    public class DeselectWhiteFieldIfSelectedMove : IPhutballMove
    {
        private Field _deselectedField;

        public void Perform(PhutballMoveContext context)
        {
            var board = context.FieldsUpdater;
            var whiteField = board.GetWhiteField();
            if(whiteField.Selected)
            {
                whiteField.DeSelect();
                _deselectedField = whiteField;
                board.UpdateFields(whiteField);
                context.SwitchPlayer.SwapMovingPlayers();
            }
        }

        public void Undo(PhutballMoveContext context)
        {
            if(_deselectedField != null)
            {
                _deselectedField.Select();
                context.FieldsUpdater.UpdateFields(_deselectedField);                
            }
        }

        public bool CollectToPlayerSwitch(CompositeMove resultMove)
        {
            resultMove.Add(this);
            return true;
        }

        public override string ToString()
        {
            return "Deslect White Field";
        }
    }
}