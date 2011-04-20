namespace Phutball.Moves
{
    public class EmptyPhutballMove : IPhutballMove
    {
        public void Perform(PhutballMoveContext context)
        {
        }

        public void Undo(PhutballMoveContext context)
        {
        }

        public bool CollectToPlayerSwitch(CompositeMove resultMove)
        {
            return false;
        }

        public override string ToString()
        {
            return "";
        }
    }
}