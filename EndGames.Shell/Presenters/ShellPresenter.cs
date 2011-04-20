using Caliburn.PresentationFramework.Screens;
using Microsoft.Practices.ServiceLocation;
using Phutball.Shell.Extensions;
using Phutball.Shell.Presenters.Interfaces;

namespace Phutball.Shell.Presenters
{
    public class ShellPresenter : Navigator<IScreen>, IShellPresenter   
    {
        private readonly IServiceLocator _serviceLocator;

        public ShellPresenter(IServiceLocator serviceLocator )
        {
            _serviceLocator = serviceLocator;
        }

        public void Open<TScreen>() where TScreen : IScreen
        {
            this.OpenScreen<TScreen>();
        }

        protected override void OnActivate()
        {
            base.OnActivate();

            Open<IHomePresenter>();
        }

        

    }
}