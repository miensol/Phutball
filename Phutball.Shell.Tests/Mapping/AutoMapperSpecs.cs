using AutoMapper;
using ForTesting;
using NUnit.Framework;
using Phutball.Mapping;

namespace Phutball.Tests.Mapping
{
    public class when_mapping_test_view_model_to_target_class : observations_for_auto_created_sut_of_type<DoMapper>
    {
        private TestViewModel _mapped;
        private TestSourceViewModel _source;

        protected override void Because()
        {
            _mapped = Sut.DoMap<TestSourceViewModel,TestViewModel>(_source);
        }

        protected override void EstablishContext()
        {
            _source = new TestSourceViewModel { SomeProperty = New.String() };
            Mapper.CreateMap<TestSourceViewModel, TestViewModel>();
        }

        protected override void AfterEachObservation()
        {
            base.AfterEachObservation();
            Mapper.Reset();
        }

        [Test]
        public void should_convert_source_object_into_mapped_target_type()
        {
            _mapped.SomeProperty.ShouldEqual(_source.SomeProperty);
        }

    }

    public class TestViewModel
    {
        public string SomeProperty { get; set; }
    }

    public class TestSourceViewModel
    {
        public string SomeProperty {
            get;
            set;}
    }
}