namespace EndGames.Mapping
{
    public interface IDoMapper
    {
        TDestination DoMap<TSource, TDestination>(TSource source);
        void DoMap<TSource, TDestination>(TSource from, TDestination to);
    }
}