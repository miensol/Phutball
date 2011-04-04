using EndGames.Phutball.Moves;

namespace EndGames.Phutball.Search
{
    public class EnsureMoveIsValid : IMoveFindingStartegy
    {
        private readonly IMoveFindingStartegy _realStrategy;

        public EnsureMoveIsValid(IMoveFindingStartegy realStrategy)
        {
            _realStrategy = realStrategy;
        }

        public IPhutballMove Search(IFieldsGraph fieldsGraph)
        {
            var result = _realStrategy.Search(fieldsGraph);
            if(result == null)
            {
                return new EmptyPhutballMove();
            }
            return new CompositeMove(new[] {result, new DeselectWhiteFieldIfSelectedMove()});
        }
    }
}