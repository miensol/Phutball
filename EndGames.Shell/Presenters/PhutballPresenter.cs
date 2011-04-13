using System.Collections.Generic;
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

        private MovesHistoryPresenter _movesHistory;
        private IEnumerable<Screen> _screenCollection;

        public MovesHistoryPresenter MovesHistory
        {
            get { return _movesHistory; }
            set { _movesHistory = value; 
                NotifyOfPropertyChange(()=> MovesHistory);
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
                                 CheatsPresenter cheatsPresenter,
                                 MovesHistoryPresenter movesHistory
            )
        {
            GameOptions = gameOptions;
            GameState = gameStatePresenter;
            Board = boardPresenter;
            Cheats = cheatsPresenter;
            MovesHistory = movesHistory;
            _screenCollection = new Screen[] {GameOptions, GameState, Board, Cheats, MovesHistory};
        }

        protected override void OnInitialize()
        {
            _screenCollection.Each(s => s.Initialize());
            base.OnInitialize();
        }

        protected override void OnActivate()
        {
            _screenCollection.Each(s => s.Activate());
            base.OnActivate();
        }
    }
}