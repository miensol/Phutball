using EndGames.Phutball.Moves;
namespace EndGames.Phutball.Search
{
    public class BoundedDepthMoveFindingStrategy : IMoveFindingStartegy
    {
        private readonly BruteForceMoveFindingStartegy _bruteForceSearch;

        public BoundedDepthMoveFindingStrategy(IPlayersState playersState, int maxDepth, MovesFactory movesFactory)
        {
            _bruteForceSearch = new BruteForceMoveFindingStartegy(new StopOnDepthNodeVisitor<JumpNode>(maxDepth), 
                (vistor)=> new DfsSearch<JumpNode>(vistor),
                playersState,
                movesFactory);
        }

        public IPhutballMove Search(IFieldsGraph fieldsGraph)
        {
            return _bruteForceSearch.Search(fieldsGraph);
        }
    }
}