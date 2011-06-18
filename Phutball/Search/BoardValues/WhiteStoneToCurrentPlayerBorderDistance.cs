namespace Phutball.Search.BoardValues
{
    public class WhiteStoneToCurrentPlayerBorderDistance : IValueOf<JumpNode>
    {
        private readonly int _distanceToBorderWeight;
        private WhiteStoneToBorderDistanceValue _whiteStoneToBorderDistance;
        private TargetBorder _targetBorder;

        public WhiteStoneToCurrentPlayerBorderDistance(IPlayersState playersState, IFieldsGraph fieldsGraph, int distanceToBorderWeight)
        {
            _distanceToBorderWeight = distanceToBorderWeight;
            var currentPlayer = playersState.CurrentPlayer;
            _targetBorder = currentPlayer.GetTargetBorder(fieldsGraph);
            _whiteStoneToBorderDistance = new WhiteStoneToBorderDistanceValue(_targetBorder);
        }

        public int GetValue(JumpNode valueSubject)
        {
            var rawValue = _whiteStoneToBorderDistance.GetValue(valueSubject.ActualGraph);
            if(rawValue == _targetBorder.WinValue || rawValue == _targetBorder.LooseValue)
            {
                return rawValue;
            }
            return rawValue * _distanceToBorderWeight;
        }
    }
}