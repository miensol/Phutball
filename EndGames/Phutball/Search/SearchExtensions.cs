namespace EndGames.Phutball.Search
{
    public static class SearchExtensions
    {
        public static ISearchNodeVisitor<TNode> FollowedBy<TNode>(this ISearchNodeVisitor<TNode> left, ISearchNodeVisitor<TNode> right)
        {
            return new CompisiteSearchNodeVisitor<TNode>(left, right);
        }
    }
}