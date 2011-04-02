using EndGames.Phutball.Moves;

namespace EndGames.Phutball.PlayerMoves
{
    public interface IPerformMoves
    {
        void Perform(IPhutballMove moveToPerform);
        void Undo(IPhutballMove moveToUndo);
    }
}