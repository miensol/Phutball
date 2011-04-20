namespace Phutball.Search.BoardValues
{
    public class AddValues<T> : IValueOf<T>
    {
        private readonly IValueOf<T> _left;
        private readonly IValueOf<T> _right;

        public AddValues(IValueOf<T> left, IValueOf<T> right)
        {
            _left = left;
            _right = right;
        }

        public int GetValue(T valueSubject)
        {
            var leftValue = _left.GetValue(valueSubject);
            if(TargetBorder.IsWinOrLooseValue(leftValue))
            {
                return leftValue;
            }
            var rightValue = _right.GetValue(valueSubject);
            if(rightValue == TargetBorder.WinValueConst)
            {
                return rightValue;
            }
            return leftValue + rightValue;
        }
    }
}