using System;
using System.Collections.Generic;
using System.Linq;
using Caliburn.PresentationFramework;
using EndGames.Mapping;
using EndGames.Phutball;
using EndGames.Phutball.Events;
using EndGames.Shell.Extensions;

namespace EndGames.Shell.Models
{
    public class PhutballBoardModel : PropertyChangedBase
    {
        private readonly IPhutballBoard _phutballBoard;
        private readonly IEventPublisher _eventPublisher;

        public PhutballBoardModel(IPhutballBoard phutballBoard, IEventPublisher eventPublisher)
        {
            _phutballBoard = phutballBoard;
            _eventPublisher = eventPublisher;
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

        public void Initialize()
        {
            _eventPublisher.GetEvent<PhutballBoardInitialized>().Subscribe(HandleGameInitialized);
            _eventPublisher.GetEvent<PhutballGameStarted>().Subscribe(HandleGameStart);
            _eventPublisher.GetEvent<PhutballGameFieldsChanged>().Subscribe(HandleGameFieldsChanged);
            _phutballBoard.Initialize();
        }

        private void HandleGameInitialized(PhutballBoardInitialized boardInitialized)
        {
            Fields = MapToViewModels(_phutballBoard.GetCurrentFields());
        }

       
        private void HandleGameStart(PhutballGameStarted phutballGameStarted)
        {
            IsEnabled = true;
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