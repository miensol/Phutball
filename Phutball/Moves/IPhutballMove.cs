namespace Phutball.Moves
{
    public interface IPhutballMove
    {
        bool CollectToPlayerSwitch(CompositeMove resultMove);
        void Perform(PhutballMoveContext context);
        void Undo(PhutballMoveContext context);
    }
}