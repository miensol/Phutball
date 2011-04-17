namespace EndGames.Phutball.Search
{
    public static class SearchExtensions
    {
        public static ISearchNodeVisitor<TNode> FollowedBy<TNode>(this ISearchNodeVisitor<TNode> left, ISearchNodeVisitor<TNode> right)
        {
            return new CompisiteSearchNodeVisitor<TNode>(left, right);
        }

        public static MoveScore<T, int> Max<T>(this MoveScore<T, int> arg1, MoveScore<T, int> arg2)
        {
            if(arg1.Score > arg2.Score)
            {
                return arg1;
            }
            if(arg1.Score == arg2.Score)
            {
                return arg1.Depth <= arg2.Depth ? arg1 : arg2;
            }
            return arg2;
        }

        public static MoveScore<T,int> Min<T>(this MoveScore<T,int> arg1, MoveScore<T,int> arg2)
        {
            if (arg1.Score < arg2.Score)
            {
                return arg1;
            }
            if (arg1.Score == arg2.Score)
            {
                return arg1.Depth <= arg2.Depth ? arg1 : arg2;
            }
            return arg2;
        }
    }
}