namespace EndGames.Phutball.Search.BoardValues
{
    public class WhiteStoneToPlayersBorderDistance : IValueOf<JumpNode>
    {
        private readonly IPlayersState _playersState;

        public WhiteStoneToPlayersBorderDistance(IPlayersState playersState)
        {
            _playersState = playersState;
        }

        public int GetValue(JumpNode valueSubject)
        {
            var targetBorder = _playersState.CurrentPlayer.GetTargetBorder(valueSubject.ActualGraph);
            var whiteStoneToBorderDistance = new WhiteStoneToBorderDistanceValue(targetBorder);
            return whiteStoneToBorderDistance.GetValue(valueSubject.ActualGraph);
        }
    }
}