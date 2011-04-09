using EndGames.Phutball.Moves;

namespace EndGames.Phutball.PlayerMoves
{
    public class PerformMoves : IPerformMoves
    {
        private readonly IFieldsUpdater _fieldsUpdater;
        private readonly IPlayersState _playersState;

        public PerformMoves(IFieldsUpdater fieldsUpdater, IPlayersState playersState)
        {
            _fieldsUpdater = fieldsUpdater;
            _playersState = playersState;
        }

        public void Perform(IPhutballMove moveToPerform)
        {
            moveToPerform.Perform(new PhutballMoveContext(this)
                                      {
                                          FieldsUpdater = _fieldsUpdater,
                                          SwitchPlayer = _playersState
                                      });
        }

        public void Undo(IPhutballMove moveToUndo)
        {
            moveToUndo.Undo(new PhutballMoveContext(this)
                                {
                                    FieldsUpdater = _fieldsUpdater,
                                    SwitchPlayer = _playersState
                                });
        }
    }
}