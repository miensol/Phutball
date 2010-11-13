namespace EndGames.Phutball.Search.BoardValues
{
    public class DistanceToBottomBorderCounter : IDistanceCounter
    {
        public int Distance(Field to)
        {
            var bottom = TargetBorderEnum.Bottom;
            if(to.RowIndex <= bottom.Oposite.RowIndex)
            {
                return bottom.RowIndex - bottom.Oposite.RowIndex;
            }
            if(to.RowIndex >= bottom.RowIndex)
            {
                return 0;
            }
            return bottom.RowIndex - to.RowIndex;
        }
    }
}