using System;
using StructureMap;
using StructureMap.Query;

namespace ForTesting
{
    public static class StructureMapContainerSpecifications
    {
        public static IRegistrationTypeConstraint ShouldRegister<TWhat>(this IContainer container)
        {
            return new RegistrationTypeConstraint<TWhat>(container).ShouldRegister();
        }
    }

    public interface IRegistrationTypeConstraint
    {
        IRegistrationTypeConstraint AsSingleton();
        IRegistrationTypeConstraint AsTransient();
    }

    public class RegistrationTypeConstraint<TPluginType> : IRegistrationTypeConstraint
    {
        private readonly IContainer _container;
        private IModel _model;

        public RegistrationTypeConstraint(IContainer container)
        {
            _container = container;
            _model = _container.Model;
        }

        public IRegistrationTypeConstraint ShouldRegister()
        {
            _model.HasImplementationsFor<TPluginType>().ShouldBeTrue();
            return this;
        }

        public IRegistrationTypeConstraint AsSingleton()
        {
            Configuration().Lifecycle.ShouldEqual(InstanceScope.Singleton.ToString());
            return this;
        }

        private IPluginTypeConfiguration Configuration()
        {
            return _model.For<TPluginType>();
        }

        public IRegistrationTypeConstraint AsTransient()
        {
            Configuration().Lifecycle.ShouldEqual(InstanceScope.Transient.ToString());
            return this;
        }
    }
}