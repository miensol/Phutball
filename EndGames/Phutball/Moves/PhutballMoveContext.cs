using EndGames.Phutball.PlayerMoves;

namespace EndGames.Phutball.Moves
{
    public class PhutballMoveContext
    {
        public IPerformMoves PerformMoves { get; set; }

        public PhutballMoveContext(IPerformMoves performMoves)
        {
            PerformMoves = performMoves;
        }

        public IPlayersState SwitchPlayer { get; set; }
        public IFieldsUpdater FieldsUpdater { get; set; }
    }
}