namespace EndGames.Phutball.Search.BoardValues
{
    public class DistanceToBottomBorderCounter : IDistanceCounter
    {
        private readonly IFieldsGraph _fieldsGraph;

        public DistanceToBottomBorderCounter(IFieldsGraph fieldsGraph)
        {
            _fieldsGraph = fieldsGraph;
        }

        public int Distance(Field to)
        {
            var bottom = _fieldsGraph.Borders.Bottom;
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