using Phutball.Moves;

namespace Phutball.Search
{
    public class PhutballMoveScore
    {
        public PhutballMoveScore(IPhutballMove move, int score)
        {
            Move = move;
            Score = score;
        }

        public IPhutballMove Move { get; set; }
        public int Score { get; set; }

        public int VisitedNodesCount { get; set; }

        public int MaxDepth { get; set; }

        public int CuttoffsCount { get; set; }

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