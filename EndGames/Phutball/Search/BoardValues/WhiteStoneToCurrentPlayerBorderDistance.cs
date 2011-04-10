namespace EndGames.Phutball.Search.BoardValues
{
    public class WhiteStoneToCurrentPlayerBorderDistance : IValueOf<JumpNode>
    {
        private readonly Player _currentPlayer;

        public WhiteStoneToCurrentPlayerBorderDistance(IPlayersState playersState)
        {
            _currentPlayer = playersState.CurrentPlayer;
        }

        public int GetValue(JumpNode valueSubject)
        {
            var targetBorder = _currentPlayer.GetTargetBorder(valueSubject.ActualGraph);
            var whiteStoneToBorderDistance = new WhiteStoneToBorderDistanceValue(targetBorder);
            return whiteStoneToBorderDistance.GetValue(valueSubject.ActualGraph);
        }
    }
}