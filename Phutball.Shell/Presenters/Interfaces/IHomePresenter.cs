using Caliburn.PresentationFramework.Screens;

namespace Phutball.Shell.Presenters.Interfaces
{
    public interface IHomePresenter : IScreen
    {
        void GoToPhutball();
        void CloseApplication();
    }
}