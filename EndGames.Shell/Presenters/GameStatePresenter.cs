using Caliburn.PresentationFramework.Filters;
using Caliburn.PresentationFramework.Screens;
using EndGames.Phutball;
using EndGames.Shell.Presenters.Interfaces;

namespace EndGames.Shell.Presenters
{

    public class GameStatePresenter : Screen, IGameStatePresenter
    {
        private readonly PhutballGameState _gameState;

        public GameStatePresenter(PhutballGameState gameState)
        {
            _gameState = gameState;
        }

        [Preview("CanStartGame", AffectsTriggers = true)]
        public void StartGame()
        {
            _gameState.Start();
        }

        public bool CanStartGame()
        {
            return _gameState.CurrentState != PhutballGameStateEnum.Started;
        }
    }
}