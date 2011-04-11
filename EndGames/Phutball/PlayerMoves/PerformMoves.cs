using EndGames.Phutball.Moves;

namespace EndGames.Phutball.PlayerMoves
{
    public class PerformMoves : IPerformMoves
    {
        private readonly IFieldsUpdater _fieldsUpdater;
        private readonly IPlayersState _playersState;
        private readonly IPerformMoves _callbackPerformer;

        public PerformMoves(IFieldsUpdater fieldsUpdater, IPlayersState playersState)
        {
            _fieldsUpdater = fieldsUpdater;
            _playersState = playersState;
            _callbackPerformer = this;
        }

        public PerformMoves(IFieldsUpdater fieldsUpdater, IPlayersState playersState, IPerformMoves callbackPerformer)
            :this(fieldsUpdater, playersState)
        {
            _callbackPerformer = callbackPerformer;
        }


        public void Perform(IPhutballMove moveToPerform)
        {
            moveToPerform.Perform(new PhutballMoveContext(_callbackPerformer)
                                      {
                                          FieldsUpdater = _fieldsUpdater,
                                          SwitchPlayer = _playersState
                                      });
        }

        public void Undo(IPhutballMove moveToUndo)
        {
            moveToUndo.Undo(new PhutballMoveContext(_callbackPerformer)
                                {
                                    FieldsUpdater = _fieldsUpdater,
                                    SwitchPlayer = _playersState
                                });
        }
    }
}