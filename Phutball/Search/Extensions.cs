namespace Phutball.Search
{
    public static class Extensions
    {
        public static IMoveFindingStartegy EnsureMoveIsValid(this IMoveFindingStartegy moveFindingStartegy)
        {
            return new EnsureMoveIsValid(moveFindingStartegy);
        }
    }
}