using System.Collections.Generic;
using AutoMapper;
using Caliburn.PresentationFramework;
using Microsoft.Practices.ServiceLocation;
using Phutball.Mapping;

namespace Phutball.Shell.Mapping
{
    public static class AutoMapperExtensions
    {
        public static IMappingExpression<TSource, TDestination> CreateWpfCollectionMapping<TSource, TDestination>(this IMappingExpression<TSource, TDestination> mappingExpression)
        {
            Mapper.CreateMap<IEnumerable<TSource>, BindableCollection<TDestination>>();
            return mappingExpression;
        }
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

        public static MappingExpression<TSource> Map<TSource>(this TSource tsource)
        {
            return new MappingExpression<TSource>(GetMapper(), tsource);
        }

    }

    public class MappingExpression<T>
    {
        private readonly IDoMapper _getMapper;
        private readonly T _tsource;

        public MappingExpression(IDoMapper getMapper, T tsource)
        {
            _getMapper = getMapper;
            _tsource = tsource;
        }

        public TDest To<TDest>()
        {
            return _getMapper.DoMap<T, TDest>(_tsource);
        }
    }
}