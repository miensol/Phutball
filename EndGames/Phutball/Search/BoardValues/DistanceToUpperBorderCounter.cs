namespace EndGames.Phutball.Search.BoardValues
{
    public class DistanceToUpperBorderCounter : IDistanceCounter
    {
        private readonly IFieldsGraph _fieldsGraph;

        public DistanceToUpperBorderCounter(IFieldsGraph fieldsGraph)
        {
            _fieldsGraph = fieldsGraph;
        }

        public int Distance(Field to)
        {
            var upper = _fieldsGraph.Borders().Upper;
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