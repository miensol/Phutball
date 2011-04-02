using EndGames.Phutball.Moves;

namespace EndGames.Phutball.Search
{
    public class JumpNode
    {
        public JumpNode(IFieldsGraph sourceGraph, IPhutballMove moveToApply)
        {
            ActualGraph = sourceGraph;
            LastMove = moveToApply;
        }

        public IFieldsGraph ActualGraph { get; private set; }
        public IPhutballMove LastMove { get; private set; }
    }
}