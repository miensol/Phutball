using System;

namespace EndGames.Phutball.Search.BoardValues
{
    public class TargetBorder
    {
        private readonly Func<int> _rowAccessor;
        private readonly Func<int, int, int> _distaceCounter;

        public TargetBorder(Func<int> rowAccessor, Func<int,int,int> distaceCounter)
        {
            _rowAccessor = rowAccessor;
            _distaceCounter = distaceCounter;
        }

        private int _rowIndex;
        private bool _isInitilized;

        private int RowIndex
        {
            get
            {
                EnsureRowIndexIsInitilized();
                return _rowIndex;
            }
        }

        private void EnsureRowIndexIsInitilized()
        {
            if(false == _isInitilized)
            {
                _rowIndex = _rowAccessor();
            }
            _isInitilized = true;
        }

        public int GetDistanceFrom(Field whiteField)
        {
            var rawDistance = _distaceCounter(RowIndex, whiteField.RowIndex);
            return Math.Max(0, rawDistance);
        }
    }
}