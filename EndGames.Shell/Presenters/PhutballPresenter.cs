using Caliburn.PresentationFramework.Screens;
using EndGames.Shell.Presenters.Interfaces;

namespace EndGames.Shell.Presenters
{
    public class PhutballPresenter : Screen, IPhutballPresenter
    {
        private GameStatePresenter _gameState;
        public GameStatePresenter GameState
        {
            get { return _gameState; }
            set { _gameState = value; 
                NotifyOfPropertyChange(()=> GameState);
            }
        }

        private PhutballBoardPresenter _board;
        public PhutballBoardPresenter Board
        {
            get { return _board; }
            set { _board = value; 
                NotifyOfPropertyChange(() => Board);
            }
        }

        public PhutballPresenter(GameStatePresenter gameStatePresenter, PhutballBoardPresenter boardPresenter)
        {
            GameState = gameStatePresenter;
            Board = boardPresenter;
        }

        protected override void OnInitialize()
        {
            GameState.Initialize();
            Board.Initialize();
            base.OnInitialize();
        }

        protected override void OnActivate()
        {
            GameState.Activate();
            Board.Activate();
            base.OnActivate();
        }
    }
}