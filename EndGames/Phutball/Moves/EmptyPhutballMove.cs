namespace EndGames.Phutball.Moves
{
    public class EmptyPhutballMove : IPhutballMove
    {
        public void Perform(PhutballMoveContext context)
        {
        }

        public void Undo(PhutballMoveContext context)
        {
        }

        public override string ToString()
        {
            return "";
        }
    }
}