using ForTesting;
using NUnit.Framework;
using Phutball.Mapping;
using Phutball.Shell.Mapping;
using Rhino.Mocks;

namespace Phutball.Tests.Mapping
{
    public class when_mapping_object_to_target_type : observations_for_static_sut_with_ioc
    {
        private TestTargetClass _mapped;
        private object _source;
        private TestTargetClass _expectedMappedObject;

        protected override void Because()
        {
            _mapped = _source.MapFromTo<object,TestTargetClass>();
        }

        protected override void EstablishContext()
        {
            _expectedMappedObject = new TestTargetClass();
            _source = new object();
            Dependency<IDoMapper>().Stub(mapper => mapper.DoMap<object,TestTargetClass>(Arg.Is(_source)))
                .Return(_expectedMappedObject);
        }

        [Test]
        public void should_convert_source_object_to_target_type_using_automapper()
        {
            Dependency<IDoMapper>().AssertWasCalled(mapper=> mapper.DoMap<object,TestTargetClass>(Arg.Is(_source)));
        }

        [Test]
        public void should_return_object_mapped_using_mapper()
        {
            _mapped.ShouldEqual(_expectedMappedObject);
        }
        
    }

    public class TestTargetClass
    {
    }
}