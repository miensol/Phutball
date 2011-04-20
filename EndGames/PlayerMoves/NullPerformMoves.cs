using Phutball.Moves;

namespace Phutball.PlayerMoves
{
    public class NullPerformMoves : IPerformMoves
    {
        public void Perform(IPhutballMove moveToPerform)
        {            
        }

        public void Undo(IPhutballMove moveToUndo)
        {
        }
    }
}