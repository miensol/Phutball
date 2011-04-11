using System;
using EndGames.Phutball.Events;
using EndGames.Phutball.Moves;

namespace EndGames.Phutball.PlayerMoves
{
    public class PerformMovesUntilPlayerOnMoveChange : IPerformMoves
    {
        private readonly IEventPublisher _eventPublisher;
        private IPerformMoves _realPerformer;

        public PerformMovesUntilPlayerOnMoveChange(
            IEventPublisher eventPublisher, 
            IFieldsUpdater fieldsUpdater,
            IPlayersState playersState)
        {
            _eventPublisher = eventPublisher;
            _realPerformer = new PerformMoves(fieldsUpdater, playersState, this);
            _eventPublisher.Subscribe<PlayerOnTheMoveChanged>(e=> _realPerformer = new NullPerformMoves());
        }

        public void Perform(IPhutballMove moveToPerform)
        {
            _realPerformer.Perform(moveToPerform);
        }

        public void Undo(IPhutballMove moveToUndo)
        {
            _realPerformer.Undo(moveToUndo);
        }        
    }
}