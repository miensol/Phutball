using Caliburn.PresentationFramework.Screens;
using ForTesting;
using NUnit.Framework;
using Phutball.Shell.Presenters;
using Phutball.Shell.Presenters.Interfaces;
using Rhino.Mocks;

namespace Phutball.Shell.Tests.Presenters
{
    public class when_shell_opens_screen : observations_for_auto_created_sut_of_type<ShellPresenter>
    {
        private ITestScreen _screenToOpen;
        private IHomePresenter _defaultScreen;

        protected override void Because()
        {
            Sut.Open<ITestScreen>();
        }

        protected override void AfterSutCreation()
        {
            base.AfterSutCreation();
            Sut.Activate();
        }

        protected override void EstablishContext()
        {
            _defaultScreen = GenerateStub<IHomePresenter>();
            _defaultScreen.Stub(home => home.CanShutdown()).Return(true);
            ProvideImplementationOf(_defaultScreen);
            _screenToOpen = GenerateStub<ITestScreen>();
            ProvideImplementationOf(_screenToOpen);
        }

        [Test]
        public void should_set_its_active_screen()
        {
            Sut.ActiveScreen.ShouldEqual(_screenToOpen);
        }
    }

    

    public interface ITestScreen : IScreen
    {
    }
}