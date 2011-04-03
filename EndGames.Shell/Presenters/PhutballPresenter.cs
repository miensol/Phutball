using Caliburn.PresentationFramework.Screens;
using EndGames.Shell.Presenters.Interfaces;

namespace EndGames.Shell.Presenters
{
    public class PhutballPresenter : Screen, IPhutballPresenter
    {
        private GameOptionsPresenter _gameOptions;
        private GameStatePresenter _gameState;

        public GameStatePresenter GameState
        {
            get { return _gameState; }
            set
            {
                _gameState = value;
                NotifyOfPropertyChange(() => GameState);
            }
        }

        private PhutballBoardPresenter _board;

        public PhutballBoardPresenter Board
        {
            get { return _board; }
            set
            {
                _board = value;
                NotifyOfPropertyChange(() => Board);
            }
        }

        private CheatsPresenter _cheats;
        public CheatsPresenter Cheats
        {
            get { return _cheats; }
            set { _cheats = value; 
                NotifyOfPropertyChange(()=> Cheats);
            }
        }

        public GameOptionsPresenter GameOptions
        {
            get { return _gameOptions; }
            set
            {
                _gameOptions = value;
                NotifyOfPropertyChange(() => GameOptions);
            }
        }

        public PhutballPresenter(GameStatePresenter gameStatePresenter,
                                 GameOptionsPresenter gameOptions,
                                 PhutballBoardPresenter boardPresenter,
                                 CheatsPresenter cheatsPresenter
            )
        {
            GameOptions = gameOptions;
            GameState = gameStatePresenter;
            Board = boardPresenter;
            Cheats = cheatsPresenter;
        }

        protected override void OnInitialize()
        {
            GameState.Initialize();
            Board.Initialize();
            GameOptions.Initialize();
            Cheats.Initialize();
            base.OnInitialize();
        }

        protected override void OnActivate()
        {
            GameState.Activate();
            Board.Activate();
            GameOptions.Activate();
            Cheats.Activate();
            base.OnActivate();
        }
    }
}