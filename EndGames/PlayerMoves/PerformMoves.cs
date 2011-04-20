using Phutball.Moves;

namespace Phutball.PlayerMoves
{
    public class PerformMoves : IPerformMoves
    {
        private readonly IFieldsUpdater _fieldsUpdater;
        private readonly IPlayersSwapper _playersState;
        private readonly IPerformMoves _callbackPerformer;
        
        public PerformMoves(IFieldsUpdater fieldsUpdater, IPlayersSwapper playersState)
        {
            _fieldsUpdater = fieldsUpdater;
            _playersState = playersState;
            _callbackPerformer = this;
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

        public static IPerformMoves DontCareAboutPlayerStateChange(IFieldsUpdater  actualGraph)
        {
            return new PerformMoves(actualGraph, new NulloPlayersSwapper());
        }
    }
}