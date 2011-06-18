using System.Windows;
using Caliburn.PresentationFramework.Screens;
using Phutball.Shell.Presenters.Interfaces;

namespace Phutball.Shell.Presenters
{
    public class HomePresenter : Screen, IHomePresenter
    {
        private readonly IShellPresenter _shellPresenter;

        public HomePresenter(IShellPresenter shellPresenter)
        {
            _shellPresenter = shellPresenter;
        }

        public void GoToPhutball()
        {
            _shellPresenter.Open<IPhutballPresenter>();
        }

        public void CloseApplication()
        {
            Application.Current.Shutdown();
        }

      
    }

   
}