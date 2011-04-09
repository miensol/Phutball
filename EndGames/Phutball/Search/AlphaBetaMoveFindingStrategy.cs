using EndGames.Phutball.Moves;
using EndGames.Phutball.PlayerMoves;
using EndGames.Phutball.Search.BoardValues;

namespace EndGames.Phutball.Search
{
    public class AlphaBetaMoveFindingStrategy : IMoveFindingStartegy
    {
        private readonly IPlayersState _playersState;
        private readonly int _alphaBetaSearchDepth;

        public AlphaBetaMoveFindingStrategy(IPlayersState playersState, int alphaBetaSearchDepth)
        {
            _playersState = playersState;            
            _alphaBetaSearchDepth = alphaBetaSearchDepth;
        }

        public IPhutballMove Search(IFieldsGraph fieldsGraph)
        {
            var andOrSearch = new AndOrSearch<JumpNode>(
                new WhiteStoneToPlayersBorderDistance(_playersState), _alphaBetaSearchDepth
            );
            var actualGraph = (IFieldsGraph)fieldsGraph.Clone();
            var performMoves = new PerformMoves(actualGraph, _playersState);
            var movesTree = new AlternatingAllJumpsMovesTree(performMoves)
                                                   {
                                                       Node = new JumpNode(actualGraph, new EmptyPhutballMove())
                                                   };
            andOrSearch.Run(movesTree);
            return null;
        }
    }
}