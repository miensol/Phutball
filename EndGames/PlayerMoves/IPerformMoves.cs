using Phutball.Moves;

namespace Phutball.PlayerMoves
{
    public interface IPerformMoves
    {
        void Perform(IPhutballMove moveToPerform);
        void Undo(IPhutballMove moveToUndo);
    }
}