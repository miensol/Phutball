using Caliburn.PresentationFramework.Filters;
using Caliburn.PresentationFramework.Screens;
using EndGames.Phutball;
using EndGames.Shell.Presenters.Interfaces;
using System;

namespace EndGames.Shell.Presenters
{

    public class GameOptionsPresenter : Screen, IGameOptionsPresenter
    {
        private const int BORDER_FIELDS_COUNT = 4;
        private readonly IEventPublisher _eventPublisher;
        private readonly IPhutballOptions _phutballOptions;

        public GameOptionsPresenter(IEventPublisher eventPublisher, IPhutballOptions phutballOptions)
        {
            _eventPublisher = eventPublisher;
            _phutballOptions = phutballOptions;
            Height = _phutballOptions.RowCount - BORDER_FIELDS_COUNT;
            Width = _phutballOptions.ColumnCount - BORDER_FIELDS_COUNT;

        }

        public decimal Width { get; set; }
        public decimal Height { get; set; }
        public decimal DfsDepth { get; set; }

        public void UpdateBoardSize()
        {
            _phutballOptions.RowCount = (int)Height + BORDER_FIELDS_COUNT;
            _phutballOptions.ColumnCount = (int)Width + BORDER_FIELDS_COUNT;
            _eventPublisher.Publish(new GameOptionsChanged());
        }

    }

    public class GameOptionsChanged
    {
    }
}