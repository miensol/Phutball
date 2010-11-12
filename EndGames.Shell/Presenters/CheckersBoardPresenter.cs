using System;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using Caliburn.PresentationFramework;
using Caliburn.PresentationFramework.Actions;
using Caliburn.PresentationFramework.Screens;
using EndGames.Games;
using EndGames.Shell.Models;
using EndGames.Shell.Presenters.Interfaces;
using EndGames.Shell.Utils;
using EndGames.Mapping;

namespace EndGames.Shell.Presenters
{
    public class CheckersBoardPresenter : Screen, ICheckersBoardPresenter
    {
        private readonly ICheckersGame _checkersGame;

        public CheckersBoardPresenter(ICheckersGame checkersGame)
        {
            _checkersGame = checkersGame;
        }


        private CheckersBoardModel _board;

        public CheckersBoardModel Board
        {
            get { return _board; }
            set { _board = value; NotifyOfPropertyChange(()=> Board);}
        }

        protected override void OnInitialize()
        {
            _checkersGame.BuildInitialGraphGame();
        }

        protected override void OnActivate()
        {
            Board = _checkersGame.MapFromTo<ICheckersGame,CheckersBoardModel>();
        }

        private void LoadBoarModel()
        {
            Board = new CheckersBoardModel();
        }

      

        public DataTemplateSelector TemplateSelector
        {
            get
            {
                return new ByTypeTemplateSelector();
            }
        }
    }
}