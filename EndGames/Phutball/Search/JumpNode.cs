using System;
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

        public override string ToString()
        {
            return LastMove.ToString();
        }

        public static JumpNode Empty(IFieldsGraph graph)
        {
            return new JumpNode(graph, new EmptyPhutballMove());
        }

        public JumpNode FollowedBy(IPhutballMove newMove)
        {
            return new JumpNode(ActualGraph, LastMove.FollowedBy(newMove));
        }
    }
}