namespace EndGames.Phutball.Search.BoardValues
{
    public class DistanceToUpperBorderCounter : IDistanceCounter
    {
        public int Distance(Field to)
        {
            var upper = TargetBorderEnum.Upper;
            if(to.RowIndex <= upper.RowIndex)
            {
                return 0;
            }
            if(to.RowIndex >= upper.Oposite.RowIndex)
            {
                return upper.Oposite.RowIndex - upper.RowIndex;
            }
            return (to.RowIndex - upper.RowIndex);
        }
    }
}