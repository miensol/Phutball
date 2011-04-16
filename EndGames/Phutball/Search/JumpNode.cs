using EndGames.Phutball.Moves;

namespace EndGames.Phutball.Search
{
    public class JumpNode
    {
        public JumpNode(IFieldsGraph sourceGraph, IPhutballMove moveToApply)
        {
            ActualGraph = sourceGraph;
            LastMove = moveToApply;
            MovesFromRoot = new EmptyPhutballMove();
        }

        public IFieldsGraph ActualGraph { get; private set; }
        public IPhutballMove LastMove { get; private set; }
        public IPhutballMove MovesFromRoot { get; set; }

        public override string ToString()
        {
            return "FromRoot : {0}, LastMove: {1}".ToFormat(MovesFromRoot.ToString(), LastMove.ToString());
        }

        public static JumpNode Empty(IFieldsGraph graph)
        {
            return new JumpNode(graph, new EmptyPhutballMove());
        }

        public JumpNode FollowedBy(IPhutballMove newMove)
        {
            return new JumpNode(ActualGraph, newMove)
                       {
                           MovesFromRoot = MovesFromRoot.FollowedBy(newMove)
                       };
        }
    }
}