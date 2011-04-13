using System;

namespace EndGames.Phutball.Moves
{
    public class PlaceBlackStoneMove : IPhutballMove
    {
        private readonly Field _field;

        public PlaceBlackStoneMove(Field field)
        {
            _field = field;
        }

        public void Perform(PhutballMoveContext context)
        {
            var board = context.FieldsUpdater;
            _field.PlaceBlackStone();
            board.UpdateFields(_field);
            context.SwitchPlayer.SwapMovingPlayers();
        }

        public void Undo(PhutballMoveContext context)
        {
            var board = context.FieldsUpdater;
            _field.RemoveStone();
            board.UpdateFields(_field);
            context.SwitchPlayer.SwapMovingPlayers();
        }

        public bool CollectToPlayerSwitch(CompositeMove resultMove)
        {
            resultMove.Add(this);
            return true;
        }
    }
}