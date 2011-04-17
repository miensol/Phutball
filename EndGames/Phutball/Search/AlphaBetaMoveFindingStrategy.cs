using System;
using EndGames.Phutball.Moves;
using EndGames.Phutball.PlayerMoves;
using EndGames.Phutball.Search.BoardValues;

namespace EndGames.Phutball.Search
{
    public class AlphaBetaMoveFindingStrategy : IMoveFindingStartegy
    {
        private readonly IPlayersState _playersState;
        private readonly IAlphaBetaOptions _alphaBetaSearchDepth;
        private readonly Func<IFieldsGraph, IJumpNodeTree> _movesFactory;
        private AndOrSearch<JumpNode> _andOrSearch;

        public AlphaBetaMoveFindingStrategy(IPlayersState playersState, 
            IAlphaBetaOptions alphaBetaSearchDepth,
            Func< IFieldsGraph,IJumpNodeTree> movesFactory
            )
        {
            _playersState = playersState;            
            _alphaBetaSearchDepth = alphaBetaSearchDepth;
            _movesFactory = movesFactory;
        }

        public PhutballMoveScore Search(IFieldsGraph fieldsGraph)
        {
            var actualGraph = (IFieldsGraph)fieldsGraph.Clone();
            var performMoves = new PerformMoves(actualGraph, _playersState);
            _andOrSearch = new AndOrSearch<JumpNode>(
                new WhiteStoneToCurrentPlayerBorderDistance(_playersState, actualGraph, _alphaBetaSearchDepth.DistanceToBorderWeight)
                .Add(new AverageBlackStoneToTargetBorderDistance(_playersState, actualGraph, _alphaBetaSearchDepth.BlackStonesToBorderWeight))
                ,
                _alphaBetaSearchDepth,
                new PerformMovesNodeVisitor(performMoves)
            );

            var movesTree = _movesFactory(actualGraph);
            _andOrSearch.Run(movesTree);
            var result = new CompositeMove();
            _andOrSearch.BestMove.Move.MovesFromRoot.CollectToPlayerSwitch(result);
            return new PhutballMoveScore(result, _andOrSearch.BestMove.Score);
        }
    }
}