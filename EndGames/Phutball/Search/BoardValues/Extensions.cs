namespace EndGames.Phutball.Search.BoardValues
{
    public static class Extensions
    {
        public static IValueOf<TSubject> Add<TSubject>(this IValueOf<TSubject> left, IValueOf<TSubject> right)
        {
            return new AddValues<TSubject>(left, right);
        }
    }
}