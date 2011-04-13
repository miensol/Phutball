using System;
using EndGames.Phutball.PlayerMoves;

namespace EndGames.Phutball.Moves
{
    public class MoveHistoryItem
    {
        public IPhutballMove Move { get; set; }

        public Func<IPerformMoves> Performer { get; set; }

        public void Undo()
        {
            Performer().Undo(Move);
        }

        public void Perform()
        {
            Performer().Perform(Move);
        }

        public static MoveHistoryItem Item(Func<IPerformMoves> movePerfomer, IPhutballMove move)
        {
            return new MoveHistoryItem
                       {
                           Performer = movePerfomer,
                           Move = move
                       };
        }
    }
}