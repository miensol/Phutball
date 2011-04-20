using Caliburn.PresentationFramework.Screens;

namespace Phutball.Shell.Presenters.Interfaces
{
    public interface IShellPresenter : IScreenCollection
    {
        void Open<TScreen>() where TScreen : IScreen;
    }
}