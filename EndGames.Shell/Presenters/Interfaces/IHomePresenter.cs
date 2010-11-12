using Caliburn.PresentationFramework.Screens;

namespace EndGames.Shell.Presenters.Interfaces
{
    public interface IHomePresenter : IScreen
    {
        void GoToPhutball();
        void CloseApplication();
    }
}