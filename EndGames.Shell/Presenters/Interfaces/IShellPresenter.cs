using Caliburn.PresentationFramework.Screens;

namespace EndGames.Shell.Presenters.Interfaces
{
    public interface IShellPresenter : IScreenCollection
    {
        void Open<TScreen>() where TScreen : IScreen;
    }
}