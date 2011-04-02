namespace EndGames.Phutball.Moves
{
    public interface IMove<in TWhere>
    {
        void Perform(TWhere board);
        void Undo(TWhere board);
    }

    public class EmptyMove<TWhere> : IMove<TWhere>
    {
        public void Perform(TWhere board)
        {
        }

        public void Undo(TWhere board)
        {
        }
    }
}