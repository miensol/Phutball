using System.Diagnostics;
using EndGames.Phutball.PlayerMoves;

namespace EndGames.Phutball.Moves
{
    public class PhutballMoveContext
    {
        public IPerformMoves PerformMoves { get; set; }

        [DebuggerStepThrough]
        public PhutballMoveContext(IPerformMoves performMoves)
        {
            PerformMoves = performMoves;
        }

        public IPlayersSwapper SwitchPlayer { get; set; }
        public IFieldsUpdater FieldsUpdater { get; set; }
    }
}