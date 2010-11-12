using System.Windows;
using Caliburn.PresentationFramework.Screens;
using EndGames.Shell.Presenters.Interfaces;
using Microsoft.Practices.ServiceLocation;
using EndGames.Shell.Extensions;
namespace EndGames.Shell.Presenters
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