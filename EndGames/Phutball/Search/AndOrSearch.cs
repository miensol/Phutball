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

        public AndOrSearch(IValueOf<T> valuer, int maxDepth)
        {
            _valuer = valuer;
            _maxDepth = maxDepth;
            _depthCounter = new DepthCounterNodeVisitor<T>();
        }


        public void Run<TTree>(TTree tree) where TTree : ITree<T>
        {
            var alpha = new MoveScore<int>{Score = int.MinValue};
            var beta = new MoveScore<int>{Score = int.MaxValue};
            var currentPlayer = new Switch<int>(MIN_PLAYER, MAX_PLAYER);
            BestMove =  AlphaBeta(tree, alpha, beta, currentPlayer.Swap());
        }

        public MoveScore<int> BestMove { get; set; }

        private MoveScore<int> AlphaBeta(ITree<T> tree, MoveScore<int> alpha, MoveScore<int> beta, Switch<int> player)
        {
            Enter(tree);
            MoveScore<int> result;
            if(_depthCounter.CurrentDepth == _maxDepth || tree.IsLeaf)
            {
                result = new MoveScore<int> {Score = _valuer.GetValue(tree.Node), Move = tree.Node, Tree = tree};
            } else
            {
                if (player.Is(MAX_PLAYER))
                {
                    foreach (var child in tree.Children)
                    {
                        var graph = child.Node;
                        alpha = Max(alpha, AlphaBeta(child, alpha, beta, player.Swap()));
                        if (beta.Score <= alpha.Score)
                        {
                            break;
                        }
                    }
                    result = alpha;
                }
                else
                {
                    foreach (var child in tree.Children)
                    {
                        var graph = child.Node;
                        beta = Min(beta, AlphaBeta(child, alpha, beta, player.Swap()));
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

        private static MoveScore<int> Min(MoveScore<int> arg1, MoveScore<int> arg2)
        {
            return arg1.Score < arg2.Score ? arg1 : arg2;
        }

        private static MoveScore<int> Max(MoveScore<int> arg1, MoveScore<int> arg2)
        {
            return arg1.Score > arg2.Score ? arg1 : arg2;
        }

        public class MoveScore<TScore>
        {
            public T Move { get; set; }
            public TScore Score { get; set; }

            public ITree<T> Tree { get; set; }

            public override string ToString()
            {
                return "Score: {0}, Move: {1}".ToFormat(Score, Move);
            }
        }

        private void Leave(ITree<T> tree)
        {
            _depthCounter.OnLeave(tree,null);
        }

        private void Enter(ITree<T> tree)
        {
            _depthCounter.OnEnter(tree, null);
        }
    }
}