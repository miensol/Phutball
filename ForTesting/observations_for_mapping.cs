using System;
using AutoMapper;

namespace ForTesting
{
    public abstract class observations_for_mapping<TSource,TDestination> : observations_for_static_sut_with_ioc
    {
        protected TSource Source;
        protected Exception ThrownException;
        protected TDestination Mapped;

        protected override void Because()
        {
            Source = GetSource();
            ThrownException = null;
            Mapped = default(TDestination);
            ThrownException = SpecificationExtensions.CatchException(()=>Mapped = Mapper.Map<TSource, TDestination>(Source));
        }

        protected abstract TSource GetSource();

        protected void assert_mapper_configuration_is_valid()
        {
            Mapper.AssertConfigurationIsValid();
        }

        protected void assert_no_exception_was_thrown_during_matching()
        {
            ThrownException.ShouldBeNull();
        }

        protected override void AfterEachObservation()
        {
            Mapper.Reset();
            base.AfterEachObservation();
        }
    }
}