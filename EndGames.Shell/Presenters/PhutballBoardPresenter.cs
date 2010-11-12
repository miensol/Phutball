﻿using System;
using Caliburn.PresentationFramework.Screens;
using EndGames.Phutball;
using EndGames.Shell.Models;
using EndGames.Shell.Presenters.Interfaces;

namespace EndGames.Shell.Presenters
{
    public class PhutballBoardPresenter : Screen, IPhutbalBoardPresenter
    {
         private readonly PhutballGameState _phutballGameState;
        private readonly Func<PhutballBoardModel> _boardCreator;
        private PhutballBoardModel _board;

        public PhutballBoardPresenter(PhutballGameState phutballGameState, Func<PhutballBoardModel> boardCreator)
        {
            _phutballGameState = phutballGameState;
            _boardCreator = boardCreator;
         
        }

        protected override void OnInitialize()
        {
            Board = _boardCreator();
            base.OnInitialize();
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