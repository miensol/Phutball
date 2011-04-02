using EndGames.Phutball.Events;

namespace EndGames.Phutball
{
    public class PhutballBoard : ReadOnlyPhutballBoard, IPhutballBoard
    {
        private readonly IEventPublisher _eventPublisher;

        public PhutballBoard(
            IFieldsGraph fieldsGraph, 
            IEventPublisher eventPublisher, 
            IPhutballOptions options) : base(fieldsGraph, options)
        {
            _eventPublisher = eventPublisher;
        }


        public void UpdateFields(params Field[] fields)
        {
            _eventPublisher.Publish(new PhutballGameFieldsChanged {ChangedFields = fields});
        }

        public void Initialize()
        {
            _fieldsGraph.Initialize();
            _eventPublisher.Publish(new PhutballBoardInitialized());
        }
    }
}