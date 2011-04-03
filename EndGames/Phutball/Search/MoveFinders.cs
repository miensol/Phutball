namespace EndGames.Phutball.Search
{
    public interface IMoveFinders
    {
        IMoveFindingStartegy DfsUnbounded(IPlayersState playersState);
        IMoveFindingStartegy BfsUnbounded(IPlayersState playersState);
        IMoveFindingStartegy DfsBounded(IPlayersState playersState, int maxDepth);
    }

    public class MoveFinders : IMoveFinders
    {
        private MovesFactory _movesFactory;

        public MoveFinders(MovesFactory movesFactory)
        {
            _movesFactory = movesFactory;
        }

        public IMoveFindingStartegy DfsUnbounded(IPlayersState playersState)
        {
            return new BruteForceMoveFindingStartegy(new EmptyNodeVisitor<JumpNode>(), 
                (visitor)=> new DfsSearch<JumpNode>(visitor), playersState,
                _movesFactory);
        }

        public IMoveFindingStartegy BfsUnbounded(IPlayersState playersState)
        {
            return new BruteForceMoveFindingStartegy(new EmptyNodeVisitor<JumpNode>(),
                                                     (vistor) => new BfsSearch<JumpNode>(vistor),
                                                     playersState,
                                                     _movesFactory);
        }

        public IMoveFindingStartegy DfsBounded(IPlayersState playersState, int maxDepth)
        {
            return new BoundedDepthMoveFindingStrategy(playersState, maxDepth, _movesFactory);
        }
    }
}