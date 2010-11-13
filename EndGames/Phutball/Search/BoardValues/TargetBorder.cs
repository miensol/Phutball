using System;

namespace EndGames.Phutball.Search.BoardValues
{
    public class TargetBorder
    {
        private readonly Func<int> _rowAccessor;

        public TargetBorder(Func<int> rowAccessor)
        {
            WinValue = int.MaxValue;
            LooseValue = 0;
            _rowAccessor = rowAccessor;            
        }

        
        public TargetBorder OppositeIs(Func<TargetBorder> opositeBorder)
        {
            _opositeBorder = opositeBorder;
            return this;
        }

        public TargetBorder CountDistanceUsing(IDistanceCounter distanceCounter)
        {
            _distanceCounter = distanceCounter;
            return this;
        }

        public TargetBorder Oposite
        {
            get { return _opositeBorder(); }
        }

        private int _rowIndex;
        private bool _isInitilized;
        private Func<TargetBorder> _opositeBorder;
        private IDistanceCounter _distanceCounter;

        public int RowIndex
        {
            get
            {
                EnsureRowIndexIsInitilized();
                return _rowIndex;
            }
        }

        public int WinValue { get; private set; }

        public int LooseValue { get; private set; }

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
            return _distanceCounter.Distance(whiteField);
        }
    }
}