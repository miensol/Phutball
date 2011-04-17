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

        public PhutballMoveScore Search(IFieldsGraph fieldsGraph)
        {
            var result = _realStrategy.Search(fieldsGraph);
            if(result == null || result.Move == null)
            {
                return PhutballMoveScore.Empty();
            }
            return result.FollowedBy(new DeselectWhiteFieldIfSelectedMove());
        }
    }
}