using System;
using System.Collections.Generic;
using System.Linq;
using Caliburn.PresentationFramework;
using EndGames.Mapping;
using EndGames.Phutball;
using EndGames.Phutball.Events;
using EndGames.Shell.Extensions;
using EndGames.Shell.Utils;

namespace EndGames.Shell.Models
{
    public class PhutballBoardModel : PropertyChangedBase
    {
        private readonly IPhutballBoard _phutballBoard;
        private readonly IEventPublisher _eventPublisher;
        private readonly IPhutballOptions _phutballOptions;

        public PhutballBoardModel(IPhutballBoard phutballBoard, IEventPublisher eventPublisher, IPhutballOptions phutballOptions)
        {
            _phutballBoard = phutballBoard;
            _eventPublisher = eventPublisher;
            _phutballOptions = phutballOptions;
            _eventPublisher.Subscribe<PhutballBoardInitialized>(HandleGameInitialized);
            _eventPublisher.Subscribe<PhutballGameStarted>(HandleGameStart);
            _eventPublisher.Subscribe<PhutballGameEnded>(HandleGameEnded);
            _eventPublisher.Subscribe<ComputerStartedMoving>(m=> IsEnabled = false);
            _eventPublisher.Subscribe<ComputerStopedMoving>(m=> IsEnabled = true);
            _eventPublisher.Subscribe<PhutballGameFieldsChanged>(HandleGameFieldsChanged);
        }

        private void HandleGameFieldsChanged(PhutballGameFieldsChanged phutballGameFieldsChanged)
        {
            phutballGameFieldsChanged.ChangedFields.Each(HandleFieldStateChanged);
        }

        private BindableCollection<FieldModel> MapToViewModels(IEnumerable<Field> changedFields)
        {
            return changedFields.MapAllFromTo<Field, FieldModel>().ToBindableCollection();
        }

        private BindableCollection<FieldModel> _fields;
        private bool _isEnabled;

        public BindableCollection<FieldModel> Fields
        {
            get { return _fields; }
            set { _fields = value; 
                NotifyOfPropertyChange(()=> Fields);
            }
        }

        private double _width;
        public double Width
        {
            get { return _width; }
            set { _width = value; 
                NotifyOfPropertyChange(()=> Width);
            }
        }

        private double _height;
        public double Height
        {
            get { return _height; }
            set { _height = value; 
                NotifyOfPropertyChange(()=> Height);
            }
        }

        public void Initialize()
        {
            _phutballBoard.Initialize();
        }

        private void HandleGameInitialized(PhutballBoardInitialized boardInitialized)
        {
            Width = (_phutballOptions.ColumnCount ) * Consts.BoardElementSize;
            Height = (_phutballOptions.RowCount ) * Consts.BoardElementSize;
            Fields = MapToViewModels(_phutballBoard.GetCurrentFields());
        }

       
        private void HandleGameStart(PhutballGameStarted phutballGameStarted)
        {
            IsEnabled = true;
        }

        private void HandleGameEnded(PhutballGameEnded phutballGameStarted)
        {
            IsEnabled = false;
        }

        public bool IsEnabled
        {
            get {
                return _isEnabled;
            }
            set {
                _isEnabled = value;
                NotifyOfPropertyChange(()=> IsEnabled);
            }
        }

        private void HandleFieldStateChanged(Field field)
        {
            var fieldModel = GetField(field.Id);
            fieldModel.MapFrom(field);
        }

        private FieldModel GetField(int id)
        {
            return Fields.First(field => field.Id == id);
        }
    }
}