using System.Diagnostics;
using Phutball.PlayerMoves;

namespace Phutball.Moves
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