namespace Phutball.Search
{
    public class MoveScore<T,TScore>
    {
        public T Move { get; set; }

        public TScore Score { get; set; }
        public int Depth { get; set; }

        public override string ToString()
        {
            return "Score: {0}, Move: {1}".ToFormat(Score, Move);
        }
    }
}