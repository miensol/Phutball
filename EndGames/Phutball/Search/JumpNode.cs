using EndGames.Phutball.Moves;

namespace EndGames.Phutball.Search
{
    public class JumpNode
    {
        public JumpNode(IFieldsGraph sourceGraph, IMove<IFieldsGraph> moveToApply)
        {
            ActualGraph = sourceGraph;
            LastMove = moveToApply;
        }

        public IFieldsGraph ActualGraph { get; private set; }
        public IMove<IFieldsGraph> LastMove { get; private set; }
    }
}