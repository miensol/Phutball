using System;
using System.Collections;
using System.Collections.Generic;

namespace Phutball.Search
{
    public class JumpCollectWhiteStonePlacesThenPutBlack : IEnumerable<IJumpNodeTreeWithFactory>
    {
        private readonly IJumpNodeTreeWithFactory _parent;
        private readonly IAlphaBetaOptions _options;
        private readonly IPlayersState _playerState;

        public JumpCollectWhiteStonePlacesThenPutBlack(IJumpNodeTreeWithFactory parent, IAlphaBetaOptions options, IPlayersState playerState)
        {
            _parent = parent;
            _options = options;
            _playerState = playerState;
        }



        public IEnumerator<IJumpNodeTreeWithFactory> GetEnumerator()
        {
            var whitePositions = new CollectWhiteFieldPositinosVisitor();
            foreach (var subMove in new AllAlternatigJumpsTreeCollection(_parent, _options, whitePositions))
            {
                yield return subMove;
            }
            foreach (var subMove in new PlaceBlackStonesForPlayer(_parent, _playerState, () => new NeighboursOfWhiteStoneBlackStonePlacer(whitePositions.FirstPosition, whitePositions.WhiteCoords)))
            {
                yield return subMove;
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }

    public class CollectWhiteFieldPositinosVisitor : ISearchNodeVisitor<JumpNode>
    {
        public HashSet<Tuple<int, int>> _whiteCords = new HashSet<Tuple<int, int>>();
        public Tuple<int, int> FirstPosition { get; set; }

        public IEnumerable<Tuple<int,int>> WhiteCoords
        {
            get {
                return _whiteCords;
            }
        }

        public void OnEnter(ITree<JumpNode> node, ITreeSearchContinuation treeSearchContinuation)
        {
            var whiteCoord = node.Node.ActualGraph.GetWhiteFieldCoords();            
            if(FirstPosition == null)
            {
                FirstPosition = whiteCoord;
            }else
            {
                _whiteCords.Add(whiteCoord);    
            }                        
        }

        public void OnLeave(ITree<JumpNode> node, ITreeSearchContinuation treeSearchContinuation)
        {
        }
    }


    public class FirstJumpThenPlaceStones : IEnumerable<IJumpNodeTreeWithFactory>
    {
        private readonly IJumpNodeTreeWithFactory _parent;
        private readonly IAlphaBetaOptions _options;
        private readonly IPlayersState _playerState;

        public FirstJumpThenPlaceStones(IJumpNodeTreeWithFactory parent, IAlphaBetaOptions options, IPlayersState playerState)
        {
            _parent = parent;
            _options = options;
            _playerState = playerState;
        }

        public IEnumerator<IJumpNodeTreeWithFactory> GetEnumerator()
        {
            foreach (var subMove in new AllAlternatigJumpsTreeCollection(_parent, _options))
            {
                yield return subMove;
            }
            foreach (var subMove in new PlaceBlackStonesForPlayer(_parent, _playerState, () => new TowardsTargetBorderStonePlacer(_options)))
            {
                yield return subMove;
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}