using System.Collections.Generic;

namespace EndGames.Phutball.Moves
{
    public static class MoveExtnensions
    {
        public static IPhutballMove ToComposite(this IEnumerable<IPhutballMove> sequence)
        {
            return new CompositeMove(sequence);
        }

        public static IPhutballMove FollowedBy(this IPhutballMove left, IPhutballMove right)
        {
            return new CompositeMove(left, right);
        }
    }
}