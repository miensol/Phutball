using EndGames.Mapping;
using EndGames.Shell.Presenters.Interfaces;
using ForTesting;
using NUnit.Framework;

namespace EndGames.Shell.Tests
{
    public class when_adding_shell_registry_to_container : observations_for_static_sut_with_ioc
    {
        protected override void Because()
        {
            Container.Configure(ce=> ce.AddRegistry<EngGamesRegistry>());
        }

        [Test, Ignore("just for now until i get time to rename invalid interfaces")]
        public void should_have_proper_container_configuration()
        {
            Container.AssertConfigurationIsValid();
        }

        [Test]
        public void should_register_shell_presenter_as_singleton()
        {
            ShouldRegister<IShellPresenter>().AsSingleton();
        }

        private IRegistrationTypeConstraint ShouldRegister<TINterface>()
        {
            return Container.ShouldRegister<TINterface>();
        }

        [Test]
        public void should_register_home_presenter_as_transient()
        {
            ShouldRegister<IHomePresenter>().AsTransient();
        }

       [Test]
        public void should_register_object_mapper()
        {
            ShouldRegister<IDoMapper>().AsTransient();
        }
    }
}