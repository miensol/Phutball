using System;
using Caliburn.PresentationFramework.Screens;
using EndGames.Phutball;
using EndGames.Shell.Models;
using EndGames.Shell.Presenters.Interfaces;

namespace EndGames.Shell.Presenters
{
    public class PhutballBoardPresenter : Screen, IPhutbalBoardPresenter
    {
        private readonly PhutballGameState _phutballGameState;
        private readonly IEventPublisher _eventPublisher;
        private readonly Func<PhutballBoardModel> _boardCreator;
        private PhutballBoardModel _board;

        public PhutballBoardPresenter(PhutballGameState phutballGameState, 
            IEventPublisher eventPublisher,
            Func<PhutballBoardModel> boardCreator)
        {
            _phutballGameState = phutballGameState;
            _eventPublisher = eventPublisher;
            _boardCreator = boardCreator;
        }

        protected override void OnInitialize()
        {
            Board = _boardCreator();
            _eventPublisher.Subscribe < GameOptionsChanged>(OnGameOptionsChanged);
            base.OnInitialize();
        }

        private void OnGameOptionsChanged(GameOptionsChanged @event)
        {
            Board.Initialize();
        }

        protected override void OnActivate()
        {
            Board.Initialize();
            base.OnActivate();
        }

        public PhutballBoardModel Board
        {
            get { return _board; }
            set { _board = value; 
                NotifyOfPropertyChange(()=>Board);
            }
        }

        public void FieldClicked(FieldModel fieldModel)
        {
            _phutballGameState.CurrentPlayerClickedField(fieldModel.Id);
        }
    }
}