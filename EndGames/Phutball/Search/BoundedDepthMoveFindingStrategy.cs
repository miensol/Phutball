using EndGames.Phutball.Moves;
using EndGames.Phutball.Search.BoardValues;

namespace EndGames.Phutball.Search
{
    public class BoundedDepthMoveFindingStrategy : IMoveFindingStartegy
    {
        private readonly BruteForceMoveFindingStartegy _bruteForceSearch;

        public BoundedDepthMoveFindingStrategy(TargetBorder targetBorder, int maxDepth)
        {
            _bruteForceSearch = new BruteForceMoveFindingStartegy(targetBorder,
                                                                  new StopOnDepthNodeVisitor<JumpNode>(maxDepth));
        }

        public IMove<IFieldsGraph> Search(IFieldsGraph fieldsGraph)
        {
            return _bruteForceSearch.Search(fieldsGraph);
        }
    }
}