using System;
using Phutball.Moves;
using Phutball.PlayerMoves;
using Phutball.Search.BoardValues;
using Phutball.Search.Visitors;

namespace Phutball.Search.Strategies
{
    public class AlphaBetaMoveFindingStrategy : IMoveFindingStartegy
    {
        private readonly IPlayersState _playersState;
        private readonly IAlphaBetaOptions _alphaBetaSearchDepth;
        private readonly Func<IFieldsGraph, IJumpNodeTree> _movesFactory;
        private AlphaBetaSearch<JumpNode> _alphaBetaSearch;

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
            if(_alphaBetaSearchDepth.SearchDepth == 0)
            {
                return PhutballMoveScore.Empty();
            }
            var actualGraph = (IFieldsGraph)fieldsGraph.Clone();
            var performMoves = new PerformMoves(actualGraph, _playersState);
            var visitedNodes = new VisitedNodesCounter<JumpNode>();
            _alphaBetaSearch = new AlphaBetaSearch<JumpNode>(
                new WhiteStoneToCurrentPlayerBorderDistance(_playersState, actualGraph, _alphaBetaSearchDepth.DistanceToBorderWeight)
                .Add(new BlackStoneToTargetBorderCount(_playersState, actualGraph, _alphaBetaSearchDepth.BlackStonesToBorderWeight))
                ,
                _alphaBetaSearchDepth,
                new PerformMovesNodeVisitor(performMoves).FollowedBy(visitedNodes)
            );

            var movesTree = _movesFactory(actualGraph);
            _alphaBetaSearch.Run(movesTree);
            var result = new CompositeMove();
            _alphaBetaSearch.BestMove.Move.MovesFromRoot.CollectToPlayerSwitch(result);
            return new PhutballMoveScore(result, _alphaBetaSearch.BestMove.Score)
                       {
                           CuttoffsCount = _alphaBetaSearch.CuttoffsCount,
                           VisitedNodesCount = visitedNodes.Count
                       };
        }
    }
}