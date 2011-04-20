using System.Collections.Generic;
using AutoMapper;
using Caliburn.PresentationFramework;

namespace Phutball.Shell.Mapping
{
    public static class AutoMapperExtensions
    {
        public static IMappingExpression<TSource, TDestination> CreateWpfCollectionMapping<TSource, TDestination>(this IMappingExpression<TSource, TDestination> mappingExpression)
        {
            Mapper.CreateMap<IEnumerable<TSource>, BindableCollection<TDestination>>();
            return mappingExpression;
        }
    }
}