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
            if(WhiteFieldIsOnLoosingRow(valueSubject, rawDistance))
            {
                return 0;
            }
            return int.MaxValue - rawDistance;
        }

        private bool WhiteFieldIsOnLoosingRow(IFieldsGraph valueSubject, int rawDistance)
        {
            return rawDistance > valueSubject.RowCount - 4;
        }
    }
}