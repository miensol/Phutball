using Phutball.Moves;

namespace Phutball.Search
{
    public interface IMoveFindingStartegy
    {
        PhutballMoveScore Search(IFieldsGraph fieldsGraph);
    }

    public class PhutballMoveScore
    {
        public PhutballMoveScore(IPhutballMove move, int score)
        {
            Move = move;
            Score = score;
        }

        public IPhutballMove Move { get; set; }
        public int Score { get; set; }

        public static PhutballMoveScore Empty()
        {
            return new PhutballMoveScore(new EmptyPhutballMove(), 0);
        }

        public PhutballMoveScore FollowedBy(IPhutballMove phutballMove)
        {
            return new PhutballMoveScore(Move.FollowedBy(phutballMove), Score);
        }
    }
}