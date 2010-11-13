using System;

namespace EndGames.Phutball.Search.BoardValues
{
    public class WhiteStoneToBorderDistanceValue : IValueOfGraph
    {
        private readonly TargetBorder _winingBorder;

        public WhiteStoneToBorderDistanceValue(TargetBorder winingBorder)
        {
            _winingBorder = winingBorder;
        }

        public int GetValue(IFieldsGraph valueSubject)
        {
            var whiteField = valueSubject.GetWhiteField();
            var rawDistance = _winingBorder.GetDistanceFrom(whiteField);
            if(rawDistance == 0)
            {
                return _winingBorder.WinValue;
            }
            var distanceBetweenBorders = DistanceBetweenBorders();
            if(rawDistance >= distanceBetweenBorders)
            {
                return _winingBorder.LooseValue;
            }
            return distanceBetweenBorders - rawDistance;                        
        }

        private int DistanceBetweenBorders()
        {
            return Math.Abs(_winingBorder.RowIndex - _winingBorder.Oposite.RowIndex);
        }
    }
}