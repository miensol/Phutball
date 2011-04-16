using System;
using EndGames.Phutball.Moves;
using EndGames.Phutball.PlayerMoves;
using EndGames.Phutball.Search.BoardValues;

namespace EndGames.Phutball.Search
{
    public class AlphaBetaMoveFindingStrategy : IMoveFindingStartegy
    {
        private readonly IPlayersState _playersState;
        private readonly int _alphaBetaSearchDepth;
        private readonly Func<IFieldsGraph, IJumpNodeTree> _movesFactory;
        private AndOrSearch<JumpNode> _andOrSearch;

        public AlphaBetaMoveFindingStrategy(IPlayersState playersState, 
            int alphaBetaSearchDepth,
            Func< IFieldsGraph,IJumpNodeTree> movesFactory
            )
        {
            _playersState = playersState.CopyRestarted();            
            _alphaBetaSearchDepth = alphaBetaSearchDepth;
            _movesFactory = movesFactory;
        }

        public IPhutballMove Search(IFieldsGraph fieldsGraph)
        {
            var actualGraph = (IFieldsGraph)fieldsGraph.Clone();
            var performMoves = new PerformMoves(actualGraph, _playersState);
            _andOrSearch = new AndOrSearch<JumpNode>(
                new WhiteStoneToCurrentPlayerBorderDistance(_playersState),
                _alphaBetaSearchDepth,
                new PerformMovesNodeVisitor(performMoves)
            );

            var movesTree = _movesFactory(actualGraph);
            _andOrSearch.Run(movesTree);
            var result = new CompositeMove();
            _andOrSearch.BestMove.Move.MovesFromRoot.CollectToPlayerSwitch(result);
            return result;
        }
    }
}