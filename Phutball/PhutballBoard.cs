using Phutball.Events;

namespace Phutball
{
    public class PhutballBoard : ReadOnlyPhutballBoard, IPhutballBoard
    {
        private readonly IPlayersState _playerState;
        private readonly IEventPublisher _eventPublisher;

        public PhutballBoard(
            IPlayersState playerState,
            IFieldsGraph fieldsGraph, 
            IEventPublisher eventPublisher, 
            IPhutballOptions options) : base(fieldsGraph, options)
        {
            _playerState = playerState;
            _eventPublisher = eventPublisher;
        }


        public void UpdateFields(params Field[] fields)
        {
            _fieldsGraph.UpdateFields(fields);
            _eventPublisher.Publish(new PhutballGameFieldsChanged {ChangedFields = fields});
            if(IsEndingConfiguration())
            {
                NotifyPlayerWon();
            }
        }

        private void NotifyPlayerWon()
        {
            var currentPlayer = _playerState.CurrentPlayer;
            var targetBorder = currentPlayer.GetTargetBorder(_fieldsGraph);                
            var whiteField = _fieldsGraph.GetWhiteField().RowIndex;
            if(targetBorder.IsWinning(whiteField))
            {
                _playerState.PlayerWon(currentPlayer);
            }else
            {
                _playerState.PlayerWon(_playerState.Next);
            }
        }

        public Field GetWhiteField()
        {
            return _fieldsGraph.GetWhiteField();
        }

        public void Initialize()
        {
            _fieldsGraph.Initialize();
            _eventPublisher.Publish(new PhutballBoardInitialized());
        }
    }
}