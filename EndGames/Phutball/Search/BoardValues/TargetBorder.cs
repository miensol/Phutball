using System;
using System.Collections.Generic;
using EndGames.Phutball.Jumpers;

namespace EndGames.Phutball.Search.BoardValues
{
    public class TargetBorder
    {
        public static readonly int WinValueConst = int.MaxValue;
        public static readonly int LooseValueConst = 0;
        public string Name { get; set; }
        private readonly Func<int> _rowAccessor;

        public TargetBorder(Func<int> rowAccessor, string name)
        {
            Name = name;
            WinValue = WinValueConst;
            LooseValue = LooseValueConst;
            _rowAccessor = rowAccessor;            
        }

        public TargetBorder ChoosePlacesForBlackStoneUsing(Func<IEnumerable<Tuple<int,int>>,IEnumerable<Tuple<int,int>>> blackChooser)
        {
            _blackPlaces = blackChooser(JumpersFactory.Directions());
            return this;
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
        private IEnumerable<Tuple<int, int>> _blackPlaces;

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

        public override string ToString()
        {
            return Name;
        }

        public IEnumerable<Tuple<int,int>> PlacesForBlackStone()
        {
            return _blackPlaces;
        }

        public static bool IsWinOrLooseValue(int leftValue)
        {
            return leftValue == WinValueConst || leftValue == LooseValueConst;
        }
    }
}