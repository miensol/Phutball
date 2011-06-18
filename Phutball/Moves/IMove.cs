namespace Phutball.Moves
{
    public interface IMove<in TWhere>
    {
        void Perform(TWhere context);
        void Undo(TWhere context);
    }    
}