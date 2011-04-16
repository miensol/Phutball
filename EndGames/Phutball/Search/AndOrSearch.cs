using System.Linq;
using EndGames.Phutball.Search.BoardValues;

namespace EndGames.Phutball.Search
{
    public class AndOrSearch<T> : ITreeSearch<T>
    {
        private const int MAX_PLAYER = 0;
        private const int MIN_PLAYER = 1;

        private readonly IValueOf<T> _valuer;
        private readonly int _maxDepth;
        private DepthCounterNodeVisitor<T> _depthCounter;
        private ISearchNodeVisitor<T> _nodeVisitor;

        public AndOrSearch(IValueOf<T> valuer, IAlphaBetaOptions maxDepth, ISearchNodeVisitor<T> nodeVisitor)
        {
            _valuer = valuer;
            _maxDepth = maxDepth.SearchDepth;
            _depthCounter = new DepthCounterNodeVisitor<T>();
            _nodeVisitor = _depthCounter.FollowedBy(nodeVisitor);
        }


        public void Run<TTree>(TTree tree) where TTree : ITree<T>
        {
            var alpha = new MoveScore<T,int>{Score = int.MinValue, Depth=int.MaxValue};
            var beta = new MoveScore<T,int>{Score = int.MaxValue, Depth=int.MaxValue};
            var currentPlayer = new Switch<int>(MIN_PLAYER, MAX_PLAYER);
            BestMove =  AlphaBeta(tree, alpha, beta, currentPlayer.Swap());
        }

        public MoveScore<T,int> BestMove { get; set; }

        private MoveScore<T,int> AlphaBeta(ITree<T> tree, MoveScore<T,int> alpha, MoveScore<T,int> beta, Switch<int> player)
        {
            Enter(tree);
            MoveScore<T,int> result;
            if(_depthCounter.CurrentDepth == _maxDepth || tree.IsLeaf)
            {
                result = new MoveScore<T,int>
                             {
                                 Score = _valuer.GetValue(tree.Node), 
                                 Move = tree.Node, 
                                 Depth = _depthCounter.CurrentDepth
                             };
            } else
            {
                if (player.Is(MAX_PLAYER))
                {
                    var children = tree.Children;
                    foreach (var child in children)
                    {
                     //   var graph = child.Node;
                        alpha = alpha.Max( AlphaBeta(child, alpha, beta, player.Swap()) );
                        if (beta.Score <= alpha.Score)
                        {
                            break;
                        }
                    }
                    result = alpha;
                }
                else
                {
                    var children = tree.Children;
                    foreach (var child in children)
                    {
                       // var graph = child.Node;
                        beta = beta.Min(AlphaBeta(child, alpha, beta, player.Swap()));
                        if (beta.Score <= alpha.Score)
                        {
                            break;
                        }
                    }
                    result = beta;
                }
            }
            Leave(tree);
            return result;
        }


        private void Leave(ITree<T> tree)
        {
            _nodeVisitor.OnLeave(tree, null);
        }

        private void Enter(ITree<T> tree)
        {
            _nodeVisitor.OnEnter(tree, null);
        }
    }
}