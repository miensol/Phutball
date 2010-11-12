using System.Collections.Generic;
using Microsoft.Practices.ServiceLocation;

namespace EndGames.Mapping
{
    public static class AutoMapperExtensions
    {
        public static TDestination MapFromTo<TSource, TDestination>(this TSource source)
        {
            IDoMapper mapper = GetMapper();
            return mapper.DoMap<TSource, TDestination>(source);
        }

        private static IDoMapper GetMapper()
        {
            return ServiceLocator.Current.GetInstance<IDoMapper>();
        }

        public static IEnumerable<TDestination> MapAllFromTo<TSource, TDestination>(this IEnumerable<TSource> sources)
        {
            return MapFromTo<IEnumerable<TSource>, IEnumerable<TDestination>>(sources);
        }

        public static void MapFrom<TSource, TDestination>(this TDestination to, TSource from)
        {
            GetMapper().DoMap(from, to);
        }
    }
}