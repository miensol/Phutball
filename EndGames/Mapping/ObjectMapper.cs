using AutoMapper;

namespace EndGames.Mapping
{
    public class DoMapper : IDoMapper
    {
        #region IDoMapper Members

        public TDestination DoMap<TSource, TDestination>(TSource source)
        {
            return Mapper.Map<TSource, TDestination>(source);
        }

        public void DoMap<TSource, TDestination>(TSource from, TDestination to)
        {
            Mapper.Map(from, to);
        }

        #endregion
    }
}